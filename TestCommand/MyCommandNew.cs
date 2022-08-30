using CoursePluginsAPI;
using System.Windows;

namespace TestCommand
{
    public class MyCommandNew : IMyCommand
    {
        public void Execute(ProjectPageViewModel pageViewModel)
        {
            pageViewModel.Elements.Clear();

            MessageBox.Show("Все элементы удалены");
        }
    }
}
