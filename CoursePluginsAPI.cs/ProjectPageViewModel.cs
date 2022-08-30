using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoursePluginsAPI.cs
{
    public class ProjectPageViewModel : BindableBase
    {
        public ObservableCollection<ElementVM> Elements { get; private set; }

        private ElementVM _selectedElement;

        public ElementVM SelectedElement
        {
            get => _selectedElement;
            set
            {
                _selectedElement = value;
                RaisePropertyChanged(nameof(SelectedElement));
            }
        }


        public ProjectPageViewModel()
        {
            Elements = new ObservableCollection<ElementVM>();
        }

        public void AddElement(ElementVM newElement)
        {
            newElement.SetId((Elements.LastOrDefault()?.Id ?? 0) + 1);
            Elements.Add(newElement);
        }

        public void RemoveElement(int elementId)
        {
            var elementToRemove = Elements.FirstOrDefault(e => e.Id == elementId);

            if (elementToRemove != null) Elements.Remove(elementToRemove);
        }
    }
}
