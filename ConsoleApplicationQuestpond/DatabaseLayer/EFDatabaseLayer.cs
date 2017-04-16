using ConsoleApplicationQuestpond.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public abstract class EFDAL<T> : IRepositoryDAL<T>
        where T : class
    {
        DbContext dbContext = null;
        public EFDAL(string connString)
        {
            dbContext = new EFUnitOfWork(connString); // self-contained manner
        }

        public void Add(T t)
        {
            dbContext.Set<T>().Add(t); // in memory commit
        }

        public void Save()
        {
            dbContext.SaveChanges(); // physical commit
        }

        public IEnumerable<T> Search()
        {
            return dbContext.Set<T>().AsQueryable<T>().ToList<T>();
        }

        public void Update(T t)
        {
            throw new NotImplementedException();
        }

        public virtual void SetUnitWork(IUnitOfWork uow)
        {
            EFUnitOfWork worker = uow as EFUnitOfWork;
            if (worker == null) throw new Exception("Cannot convert EFUnitOfWork to IUnitOfWork");

            dbContext = worker;
        }
    }
}
