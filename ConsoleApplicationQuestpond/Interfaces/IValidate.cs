using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.Interfaces
{
    /// <summary>
    /// Stratergy Pattern : Helps to choose algoriths dynamically
    /// </summary>
    public interface IValidate<T>
    {
        bool Validate(T info);
    }
}
