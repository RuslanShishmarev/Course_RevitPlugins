using CoursePlugins.Models;
using CoursePlugins.Views;
using CoursePluginsAPI.cs;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;

namespace CoursePlugins.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public List<CommandVM> Commands { get; private set; }

        public ProjectPage ProjectPage { get; }

        private ProjectPageViewModel _projectPageViewModel;
        public MainWindowViewModel()
        {
            Commands = new List<CommandVM>()
            {
                new CommandVM("Кнопка 1", new DelegateCommand(Button1)),
                new CommandVM("Кнопка 2", new DelegateCommand(Button2)),
                new CommandVM("Создать елемент", new DelegateCommand(Button3)),
                new CommandVM("Удалить елемент", new DelegateCommand(Button4)),
            };

            _projectPageViewModel = new ProjectPageViewModel();
            ProjectPage = new ProjectPage();
            ProjectPage.DataContext = _projectPageViewModel;

            LoadCommands();
        }

        private void Button1()
        {
            MessageBox.Show(nameof(Button1));
        }
        private void Button2()
        {
            MessageBox.Show(nameof(Button2));
        }

        private void Button3()
        {
            _projectPageViewModel.AddElement(new ElementVM("Новый элемент", "Просто новый элемент для теста"));
        }

        private void Button4()
        {
            if (_projectPageViewModel.SelectedElement != null)
                _projectPageViewModel.RemoveElement(_projectPageViewModel.SelectedElement.Id);
        }

        private void LoadCommands()
        {
            string commandSelectorsFolderPath = "C://Users//Admin//Desktop//Programming//Revit//PluginsForRevit//Selectors";

            string selectorPattern = "*.myaddin";

            string[] files = Directory.GetFiles(commandSelectorsFolderPath, selectorPattern);

            foreach (string file in files)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AddinSelector));

                using (StreamReader sr = new StreamReader(file))
                {
                    var selector = serializer.Deserialize(sr) as AddinSelector;

                    var assembly = Assembly.LoadFrom(selector.AssemblyPath);

                    IMyCommand command = assembly.CreateInstance(selector.ClassName) as IMyCommand;

                    DelegateCommand delegateCommand = new DelegateCommand(() => { command.Execute(_projectPageViewModel); });

                    Commands.Add(new CommandVM(selector.ButtonName, delegateCommand));
                }
            }
        } 
    }
}
