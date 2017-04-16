using ConsoleApplicationQuestpond.Interfaces;
using ConsoleApplicationQuestpond.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public enum DALType
    {
        ADOSQL,
        EF,
        ADOOLEDB
    }

    public static class DALFactory
    {
        public static IRepositoryDAL<CustomerDbObject> CreateCustomerDAL(DALType type)
        {
            switch (type)
            {
                case DALType.ADOSQL:
                    CustomerSQLDAL sqlDAL = new CustomerSQLDAL(GlobalConnectionString.ConnString);
                    //sqlDAL.SetUnitWork(new SQLUnitOfWork(GlobalConnectionString.ConnString));
                    return sqlDAL;
                case DALType.ADOOLEDB:
                    CustomerOLEDAL oleDAL = new CustomerOLEDAL(GlobalConnectionString.ConnString);
                    //oleDAL.SetUnitWork(new OLEUnitIOfWork(GlobalConnectionString.ConnString));
                    return oleDAL;
                case DALType.EF:
                    CustomerEFDAL efDal =  new CustomerEFDAL(GlobalConnectionString.ConnString);
                    //efDal.SetUnitWork(new EFUnitOfWork(GlobalConnectionString.ConnString));
                    return efDal;
                default:
                    throw new ArgumentException("Don't know what type of DALType");
            }
        }

        public static IRepositoryDAL<T> CreateDAL<T>(DALType type)
        {
            switch(type)
            {
                case DALType.ADOSQL:
                    IRepositoryDAL<CustomerDbObject> adoSql = new CustomerSQLDAL(GlobalConnectionString.ConnString);
                    adoSql.SetUnitWork(new SQLUnitOfWork(GlobalConnectionString.ConnString));
                    IRepositoryDAL<T> valSQL = adoSql as IRepositoryDAL<T>; // here cast T into AbsSimpleCustomerInfo. Bad idea
                    if (valSQL == null) throw new ArgumentNullException("Cannot convert IRepositoryDAL<CustomerDbObject> to IRepositoryDAL<T>");
                    return valSQL;
                case DALType.ADOOLEDB:
                    IRepositoryDAL<CustomerDbObject> adoOleDb = new CustomerOLEDAL(GlobalConnectionString.ConnString);
                    adoOleDb.SetUnitWork(new OLEUnitIOfWork(GlobalConnectionString.ConnString));
                    IRepositoryDAL<T> valOleDb = adoOleDb as IRepositoryDAL<T>;
                    if (valOleDb == null) throw new ArgumentNullException("Cannot convert IRepositoryDAL<CustomerDbObject> to IRepositoryDAL<T>");
                    return valOleDb;
                case DALType.EF:
                    CustomerEFDAL ef = new CustomerEFDAL(GlobalConnectionString.ConnString);
                    ef.SetUnitWork(new EFUnitOfWork(GlobalConnectionString.ConnString));
                    IRepositoryDAL<T> valEF = ef as IRepositoryDAL<T>;
                    if (valEF == null) throw new ArgumentNullException("Cannot convert IRepositoryDAL<CustomerDbObject> to IRepositoryDAL<T>");
                    return valEF;
                default:
                    throw new ArgumentException("Don't know what type of DALType");
            }
        }
    }
}
