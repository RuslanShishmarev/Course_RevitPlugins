using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_7 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector levelCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType();
            var levels = levelCollector.ToElements().Cast<Level>();
            Level firstLevel = levels.FirstOrDefault(l => l.Name == "Level 1");
            Level secondLevel = levels.FirstOrDefault(l => l.Name == "Level 2");

            FilteredElementCollector wallCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType();
            WallType wallType = wallCollector.ToElements().FirstOrDefault(x => x.Name == "Generic - 200mm") as WallType;

            List<Curve> profile = new List<Curve>()
            {
                Line.CreateBound(new XYZ(0,0,0), new XYZ(10,0,0)),
                Line.CreateBound(new XYZ(10,0,0), new XYZ(10,0,10)),
                Line.CreateBound(new XYZ(10,0,10), new XYZ(0,0,10)),
                Line.CreateBound(new XYZ(0,0,10), new XYZ(0,0,0)),

            };

            Line curve = Line.CreateBound(new XYZ(10, 10, 0), new XYZ(30, 50, 0));

            using (Transaction tr = new Transaction(doc, "Создать стену по профилю"))
            {
                tr.Start();

                Wall.Create(doc, profile, wallType.Id, firstLevel.Id, false);

                tr.Commit();

                if(tr.GetStatus() == TransactionStatus.Committed)
                {
                    tr.Start("Создать стену по контуру");
                    Wall newWall = Wall.Create(doc, curve, wallType.Id, firstLevel.Id, 5000 / 304.8, 0, false, false);
                    newWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).Set(secondLevel.Id);
                    tr.Commit();
                }
            }

            return Result.Succeeded;
        }
    }
}
