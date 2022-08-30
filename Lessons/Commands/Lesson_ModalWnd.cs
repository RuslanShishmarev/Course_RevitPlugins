using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Lessons.Models;
using Lessons.ViewModels;
using Lessons.Views;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_ModalWnd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection choices = uidoc.Selection;
            Reference hasPickOne = choices.PickObject(ObjectType.Element, new WallFilter());

            if (hasPickOne != null)
            {
                Wall selectedWall = doc.GetElement(hasPickOne) as Wall;

                Lesson_ModalWndViewModel vm = new Lesson_ModalWndViewModel(selectedWall);

                ModalWnd wnd = new ModalWnd();
                wnd.DataContext = vm;
                wnd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                wnd.ShowDialog();
            }

            return Result.Succeeded;
        }
    }    
}
