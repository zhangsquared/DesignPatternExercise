using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<IVisitable> animals = new IVisitable[] { new Cat(), new Cat(), new Dog() };
            IVisitor visitor = new FeedPlan();
            foreach(IVisitable animal in animals)
            {
                animal.Accept(visitor);
            }
        }
    }
}
