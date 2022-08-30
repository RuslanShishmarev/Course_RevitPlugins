using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System.Linq;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_6 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector levelCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType();
            Level firstLevel = levelCollector.ToElements().Cast<Level>().FirstOrDefault(l => l.Name == "Level 1");

            FilteredElementCollector columnCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_Columns);
            FamilySymbol columnType = columnCollector.FirstElement() as FamilySymbol;

            using (Transaction tr = new Transaction(doc, "Create column"))
            {
                tr.Start();

                XYZ origin = new XYZ(0, 0, 0);
                doc.Create.NewFamilyInstance(origin, columnType, firstLevel, StructuralType.NonStructural);

                tr.Commit();
            }

            return Result.Succeeded;
        }
    }
}
