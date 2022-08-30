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
    public class Lesson_4 : IExternalCommand
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
                TaskDialog.Show("Один элемент", selectedElement.Name);
            }

            IList<Reference> selectedEls = choices.PickObjects(ObjectType.Element);

            if (selectedEls.Any())
            {
                TaskDialog.Show("Несколько элементов по клику", selectedEls.Count().ToString());
            }

            IList<Element> selectedElsByReg = choices.PickElementsByRectangle();

            if (selectedElsByReg.Any())
            {
                TaskDialog.Show("Несколько элементов рамкой", selectedElsByReg.Count().ToString());
            }

            TaskDialog.Show("Несколько стен рамкой", selectedElsByReg.Where(el => el.GetType().Name == nameof(Wall)).Count().ToString());

            IList<Element> selectedWindows = choices.PickElementsByRectangle(new WindowsFilter());

            if (selectedWindows.Any())
            {
                TaskDialog.Show("Окна", selectedWindows.Count().ToString());
            }

            return Result.Succeeded;
        }
    }

    class WindowsFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if((BuiltInCategory)elem.Category.Id.IntegerValue == BuiltInCategory.OST_Windows)
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
