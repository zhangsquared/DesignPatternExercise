using ConsoleApplicationQuestpond.DatabaseLayer;
using ConsoleApplicationQuestpond.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.Models
{
    public static class GenericFactory<T>
    {
        // ReSharper warns: “Static field in generic type”
        // http://stackoverflow.com/questions/9647641/
        private static IUnityContainer custs = null;

        public static T Create(string type)
        {
            if (custs == null)
            {
                custs = new UnityContainer();
                custs.RegisterType<CustomerDbObject, Customer>("Customer", new InjectionConstructor(new CustomerValidationMethod()));
                custs.RegisterType<CustomerDbObject, Lead>("Lead", new InjectionConstructor(new LeadValidationMethod()));
                custs.RegisterType<IRepositoryDAL<CustomerDbObject>, CustomerSQLDAL>("SQLDatabase");
                custs.RegisterType<IRepositoryDAL<CustomerDbObject>, CustomerOLEDAL>("OleDbDatabase");
                custs.RegisterType<IRepositoryDAL<CustomerDbObject>, CustomerEFDAL>("EFDatabase");
                //custs.RegisterType<IUnitOfWork, Ado>("SQLUOW");

            }
            return custs.Resolve<T>(type, 
                new ResolverOverride[]
                {
                    new ParameterOverride("connectionString", GlobalConnectionString.ConnString)
                });
        }
    }


}
