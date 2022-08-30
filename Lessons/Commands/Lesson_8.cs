using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_8 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {   
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection choices = uidoc.Selection;

            Reference hasPickOne = choices.PickObject(ObjectType.Element);
            if (hasPickOne != null)
            {
                Element selectedElement = doc.GetElement(hasPickOne.ElementId);
                if(selectedElement is Wall selectedWall)
                {
                    Level level = doc.GetElement(selectedWall.LevelId) as Level;

                    FilteredElementCollector collector = new FilteredElementCollector(doc);
                    ICollection<Element> collection = collector.OfClass(typeof(FamilySymbol))
                                                               .OfCategory(BuiltInCategory.OST_Windows)
                                                               .ToElements();
                    FamilySymbol windowType = collection.FirstOrDefault() as FamilySymbol;
                    windowType.Activate();

                    LocationCurve locationWall = selectedWall.Location as LocationCurve;

                    Curve curve = locationWall.Curve;

                    XYZ locationPoint = curve.Evaluate(0.5, true);

                    double zPosition = selectedWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble() / 2;

                    XYZ point = new XYZ(locationPoint.X, locationPoint.Y, zPosition);

                    using (Transaction tr = new Transaction(doc, "Создание окна"))
                    {
                        tr.Start();

                        doc.Create.NewFamilyInstance(
                            location: point,
                            symbol: windowType,
                            host: selectedWall,
                            level: level,
                            structuralType: Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                        tr.Commit();
                    }
                }
            }
            return Result.Succeeded;
        }
    }
}
