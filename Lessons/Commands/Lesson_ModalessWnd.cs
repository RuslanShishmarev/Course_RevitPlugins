using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Lessons.ViewModels;
using Lessons.Views;

namespace Lessons.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Lesson_ModalessWnd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Lesson_ModalessWndHandler hadler = new Lesson_ModalessWndHandler();
            hadler.Initialize();

            ModalessWnd wnd = new ModalessWnd();
            Lesson_ModalessWndViewModel vm = new Lesson_ModalessWndViewModel(uidoc, wnd, hadler);

            wnd.DataContext = vm;
            wnd.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            wnd.Show();

            return Result.Succeeded;
        }
    }

    public class Lesson_ModalessWndHandler : IExternalEventHandler
    {
        private Wall _selectedWall;
        private double _height;
        public void Execute(UIApplication app)
        {
            if (_selectedWall != null)
            {
                using (Transaction tr = new Transaction(_selectedWall.Document, $"Задать высоту стене {_height}"))
                {
                    tr.Start();
                    _selectedWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).Set(_height / 304.8);
                    tr.Commit();
                }
            }
        }

        private ExternalEvent _externalEvent;
        public void Initialize()
        {
            _externalEvent = ExternalEvent.Create(this);
        }

        public void Raise(Wall selectedWall, double height) 
        {
            _selectedWall = selectedWall;
            _height = height;
            _externalEvent.Raise();
        }

        public string GetName()
        {
            return "SetHeight";
        }
    }
}
