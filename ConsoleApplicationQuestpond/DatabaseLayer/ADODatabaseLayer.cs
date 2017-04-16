using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using ConsoleApplicationQuestpond.Interfaces;
using System;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public abstract class ADODAL<T, ConnType, CommandType> : IRepositoryDAL<T>
        where ConnType : DbConnection, new()
        where CommandType : DbCommand, new()
    {
        protected string connectionString = string.Empty;
        protected IList<T> internalList = new List<T>();

        protected DbConnection connection = null;
        protected DbCommand command = null;
        protected IUnitOfWork uow = null;

        public ADODAL(string connString)
        {
            connectionString = connString;
        }

        public virtual void Add(T t)
        {
            internalList.Add(t);
        }

        public virtual void Update(T t)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Search()
        {
            return ExecuteSearch();
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public abstract void SetUnitWork(IUnitOfWork uow);

        private void Open()
        {
            if(connection == null) // in case that SetUnitWork is not called
            {
                // How to initialize generic parameter type T?
                // http://stackoverflow.com/questions/13989093
                connection = new ConnType();
                connection.ConnectionString = connectionString;
                connection.Open();
                command = new CommandType();
                command.Connection = connection;
            }
        }
        private void Close()
        {
            if (uow == null)
            {
                connection.Close();
            }
        }


        protected abstract void ExecuteInsertCommand(T t);

        /// <summary>
        /// Design patter : Template pattern
        /// Fixed Sequence
        /// 1. Open Connection
        /// 2. Execute Command (SQL). the child classes will define the behavior
        /// 3. Close Connection
        /// </summary>
        protected void ExecuteInsert(T t)
        {
            Open();
            ExecuteInsertCommand(t);
            Close();
        }


        protected abstract IEnumerable<T> ExecuteSearchCommand();

        /// <summary>
        /// Fixed Sequence
        /// </summary>
        protected IEnumerable<T> ExecuteSearch()
        {
            IEnumerable<T> list = null;
            Open();
            list = ExecuteSearchCommand();
            Close();
            return list;
        }

    }

    public abstract class SQLDAL<T> : ADODAL<T, SqlConnection, SqlCommand>
    {
        public SQLDAL(string connString) : base(connString)
        {
        }

        public override void SetUnitWork(IUnitOfWork uow)
        {
            SQLUnitOfWork worker = uow as SQLUnitOfWork;
            if (worker == null) throw new Exception("Cannot convert IUnitOfWork to SQLUnitOfWork");

            this.uow = uow;
            connection = worker.Connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = worker.Transaction;
        }
    }

    public abstract class OLEDAL<T> : ADODAL<T, OleDbConnection, OleDbCommand>
    {
        public OLEDAL(string connString) : base(connString)
        {
        }

        public override void SetUnitWork(IUnitOfWork uow)
        {
            OLEUnitIOfWork worker = uow as OLEUnitIOfWork;
            if (worker == null) throw new Exception("Cannot convert IUnitOfWork to SQLUnitOfWork");

            this.uow = uow;
            connection = worker.Connection;
            command = new OleDbCommand();
            command.Connection = connection;
            command.Transaction = worker.Transaction;
        }
    }
}
