using Prism.Commands;

namespace CoursePlugins.Models
{
    public class CommandVM
    {
        public string Name { get; private set; }

        public DelegateCommand Command { get; private set; }
     
        public CommandVM(string name, DelegateCommand command)
        {
            Name = name;
            Command = command;
        }
    }
}
