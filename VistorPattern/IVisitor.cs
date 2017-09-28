using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistorPattern
{
    public interface IVisitor
    {
        void Feed(Cat cat);
        void Feed(Dog dog);
    }

    public class FeedPlan : IVisitor
    {
        public void Feed(Cat cat)
        {
            Console.WriteLine($"Feed {cat} with chicken.");
        }

        public void Feed(Dog dog)
        {
            Console.WriteLine($"Feed {dog} with bones.");
        }
    }

}
