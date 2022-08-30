using CoursePluginsAPI.cs;

namespace TestCommand
{
    public class MyCommand : IMyCommand
    {
        public void Execute(ProjectPageViewModel pageViewModel)
        {
            ElementVM elementVM1 = new ElementVM("Внешний элемент 1", "Элемент созданный с помощью плагина");
            ElementVM elementVM2 = new ElementVM("Внешний элемент 2", "Элемент созданный с помощью плагина");
            ElementVM elementVM3 = new ElementVM("Внешний элемент 3", "Элемент созданный с помощью плагина");

            pageViewModel.AddElement(elementVM1);
            pageViewModel.AddElement(elementVM2);
            pageViewModel.AddElement(elementVM3);
        }
    }
}
