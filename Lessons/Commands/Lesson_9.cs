using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_9 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection choices = uidoc.Selection;

            Reference hasPickOne = choices.PickObject(ObjectType.Face);
            if (hasPickOne != null)
            {
                FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_GenericModel);
                FamilySymbol symbol = collector.ToElements().Cast<FamilySymbol>().FirstOrDefault(x => x.FamilyName == "M_Trim-Window-Interior-Flat");
                symbol.Activate();

                Element element = doc.GetElement(hasPickOne);
                PlanarFace selectedFace = element.GetGeometryObjectFromReference(hasPickOne) as PlanarFace;

                Options geomOptions = new Options();
                geomOptions.ComputeReferences = true;
                GeometryElement geometryElement = element.get_Geometry(geomOptions);

                Face face = null;
                foreach (GeometryObject geomObj in geometryElement)
                {
                    Solid geomSolid = geomObj as Solid;
                    if (geomSolid != null)
                    {
                        foreach (Face geomFace in geomSolid.Faces)
                        {
                            if (geomFace is PlanarFace planarFace &&
                                selectedFace.Area == planarFace.Area &&
                                selectedFace.FaceNormal.X == planarFace.FaceNormal.X &&
                                selectedFace.FaceNormal.Y == planarFace.FaceNormal.Y &&
                                selectedFace.FaceNormal.Z == planarFace.FaceNormal.Z) 
                            { 
                                face = planarFace; 
                                break;
                            }
                        }
                        break;
                    }
                }

                if (face is null) return Result.Failed;

                BoundingBoxUV bboxUV = selectedFace.GetBoundingBox();
                UV center = (bboxUV.Max + bboxUV.Min) / 2.0;
                XYZ location = selectedFace.Evaluate(center);
                XYZ normal = selectedFace.ComputeNormal(center);
                XYZ refDir = normal.CrossProduct(XYZ.BasisZ);

                using (Transaction tr = new Transaction(doc, "Создание элемента на поверхности"))
                {
                    tr.Start();

                    doc.Create.NewFamilyInstance(face, location, refDir, symbol);

                    tr.Commit();
                }
            }
            return Result.Succeeded;
        }
    }
}
