using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistorPattern
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
    public interface IAnimal : IVisitable
    {

    }

    public class Cat : IAnimal
    {
        public string CatName { get; set; }
        public void Accept(IVisitor visitor)
        {
            visitor.Feed(this);
        }
    }

    public class Dog : IAnimal
    {
        public string DogName { get; set; }
        public void Accept(IVisitor visitor)
        {
            visitor.Feed(this);
        }
    }
}
