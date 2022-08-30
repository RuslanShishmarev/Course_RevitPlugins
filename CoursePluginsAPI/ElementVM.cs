using System;

namespace CoursePluginsAPI.cs
{
    public class ElementVM
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ElementVM(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public ElementVM(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
