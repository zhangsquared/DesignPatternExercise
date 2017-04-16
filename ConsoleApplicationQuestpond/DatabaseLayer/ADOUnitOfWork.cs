using ConsoleApplicationQuestpond.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public abstract class ADOUnitOfWork<ConnType> : IUnitOfWork
        where ConnType : DbConnection, new()
    {
        public ConnType Connection { get; set; }
        public DbTransaction Transaction { get; set; }

        public ADOUnitOfWork(string connString)
        {
            Connection = new ConnType();
            Connection.ConnectionString = connString;
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public virtual void Commit()
        {
            Transaction.Commit();
            Connection.Close();
        }

        /// <summary>
        /// Design Pattern: Adapter Pattern
        /// </summary>
        public virtual void RollBack()
        {
            Transaction.Dispose();
            Connection.Close();
        }
    }

    public class SQLUnitOfWork : ADOUnitOfWork<SqlConnection>
    {
        public SQLUnitOfWork(string connString) : base(connString)
        {
        }
    }

    public class OLEUnitIOfWork : ADOUnitOfWork<OleDbConnection>
    {
        public OLEUnitIOfWork(string connString) : base(connString)
        {
        }
    }
}
