using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Lessons.Models
{
    public class WallFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if ((BuiltInCategory)elem.Category.Id.IntegerValue == BuiltInCategory.OST_Walls)
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
