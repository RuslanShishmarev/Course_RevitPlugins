using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Lessons.Commands;
using Lessons.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Lessons.ViewModels
{
    public class Lesson_ModalessWndViewModel : INotifyPropertyChanged
    {
        private UIDocument _uidoc;
        private Window _window;
        private Lesson_ModalessWndHandler _handler;
        public Lesson_ModalessWndViewModel(UIDocument uidoc, Window window, Lesson_ModalessWndHandler handler)
        {
            _uidoc = uidoc; 
            _window = window;
            _handler = handler;
        }

        private Wall _selectedWall;
        public Wall SelectedWall
        {
            get => _selectedWall;
            set
            {
                _selectedWall = value;
                OnPropertyChanged(nameof(SelectedWall));
            }
        }

        private double _height = 1000;
        public double Height
        {
            get => _height;
            set 
            { 
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public RelayCommand SetHeightCommand
        {
            get
            {
                return new RelayCommand(obj => 
                {
                    _handler?.Raise(SelectedWall, Height);
                });
            }
        }

        public RelayCommand SelectWallCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Document doc = _uidoc.Document;
                    Selection choices = _uidoc.Selection;
                    Reference hasPickOne = choices.PickObject(ObjectType.Element, new WallFilter());

                    if (hasPickOne != null)
                    {
                        Wall selectedWall = doc.GetElement(hasPickOne) as Wall;
                        SelectedWall = selectedWall;
                    }

                    _window.WindowState = WindowState.Normal;
                    _window.Focus();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
