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
    public class Lesson_3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Selection selection = uidoc.Selection;
            ICollection<ElementId> selectedIds = selection.GetElementIds();

            string allIdsAsString = "Нет выбранных элементов";

            string elementsNameAndType = "Нет элементов";

            if (selectedIds.Any())
            {
                allIdsAsString = string.Join("\n", selectedIds);
                var elementsByIds = selectedIds.Select(id => doc.GetElement(id));

                var elementsNameAndTypeStrs = elementsByIds.Select(e => $"{e.Id} {e.Name} {e.GetType()}");

                elementsNameAndType = string.Join("\n", elementsNameAndTypeStrs);
            }

            TaskDialog.Show("Список выбранных элементов", elementsNameAndType);


            return Result.Succeeded;
        }
    }
}
