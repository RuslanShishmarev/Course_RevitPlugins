using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            FilteredElementCollector collectorWalls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType();
            IList<Element> walls = collectorWalls.ToElements();

            FilteredElementCollector collectorWallTypess = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType();
            IList<Element> wallTypes = collectorWallTypess.ToElements();

            FilteredElementCollector collectorRoofs = new FilteredElementCollector(doc).OfClass(typeof(RoofBase));
            var roofs = collectorRoofs.WhereElementIsNotElementType().ToElements().Cast<RoofBase>();

            RoofBase firstRoof = roofs.FirstOrDefault(x => x.LevelId.IntegerValue != -1);

            Level level = doc.GetElement(firstRoof.LevelId) as Level;
            MessageBox.Show(level.Name);

            RoofType roofType = firstRoof.RoofType;

            MessageBox.Show(roofType.Name);

            MessageBox.Show($"Всего стен: {walls.Count}\nВсего типов стен: {wallTypes.Count}");
            MessageBox.Show($"Всего крыш: {roofs.Count()}");

            return Result.Succeeded;
        }
    }
}
