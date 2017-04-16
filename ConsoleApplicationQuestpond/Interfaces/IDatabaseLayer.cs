using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.Interfaces
{
    /// <summary>
    /// Design Pattern : Generic Repository pattern
    /// Design Pattern : Adapter pattern
    /// </summary>
    public interface IRepositoryDAL<T>
    {
        void Add(T t); // in memory add
        void Update(T t); // in memory update

        IEnumerable<T> Search();

        void Save(); // physical commit

        void SetUnitWork(IUnitOfWork uow);
    }
}
