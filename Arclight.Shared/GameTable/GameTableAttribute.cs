using System;

namespace Arclight.Shared.GameTable
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GameTableAttribute : Attribute
    {
        public string Name { get; }

        public GameTableAttribute(string name)
        {
            Name = name;
        }
    }
}
