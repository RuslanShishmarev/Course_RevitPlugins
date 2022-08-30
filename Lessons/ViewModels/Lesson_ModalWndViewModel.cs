using Autodesk.Revit.DB;
using Lessons.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lessons.ViewModels
{
    public class Lesson_ModalWndViewModel : INotifyPropertyChanged
    {
        public Lesson_ModalWndViewModel(Wall selectedWall)
        {
            SelectedWall = selectedWall;
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
                if (_height <= 0) _height = 1000;
                OnPropertyChanged(nameof(Height));
            }
        }

        public RelayCommand SetHeightCommand
        {
            get
            {
                return new RelayCommand(obj => 
                {
                    if(SelectedWall != null)
                    {
                        using (Transaction tr = new Transaction(_selectedWall.Document, $"Задать высоту стене {Height}"))
                        {
                            tr.Start();
                            SelectedWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM)?.Set(Height/304.8);
                            tr.Commit();
                        }
                    }
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
