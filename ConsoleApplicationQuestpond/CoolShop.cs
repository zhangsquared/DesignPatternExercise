using ConsoleApplicationQuestpond.DatabaseLayer;
using ConsoleApplicationQuestpond.Interfaces;
using ConsoleApplicationQuestpond.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApplicationQuestpond
{
    public class CoolShop
    {
        public void Run()
        {
            CustomerDbObject cust = CustomerFactory.CreateCustomer(CustomerType.Lead);
            CustomerDbObject cust1 = CustomerFactory.CreateCustomer(CustomerType.Lead);
            bool same = object.ReferenceEquals(cust, cust1);
            cust.CustomerName = "zz";
            cust.PhoneNumber = "123456789";
            bool v = cust.Validate();
            string name = cust.CustomerName;
        }

        public void AddCustomerInfo()
        {
            CustomerDbObject customer = GenericFactory<CustomerDbObject>.Create("Lead");

            IRepositoryDAL<CustomerDbObject> databaseLayer0 = GenericFactory<IRepositoryDAL<CustomerDbObject>>.Create("SQLDatabase");
            IRepositoryDAL<CustomerDbObject> databaseLayer = DALFactory.CreateCustomerDAL(DALType.ADOSQL);
            IRepositoryDAL<CustomerDbObject> databaseLayer1 = DALFactory.CreateCustomerDAL(DALType.EF);
            databaseLayer.Add(customer); // in memory
            databaseLayer.Save(); // physical saving
        }


        public void Load()
        {
            IRepositoryDAL<CustomerDbObject> databaseLayer = DALFactory.CreateCustomerDAL(DALType.ADOSQL);
            IRepositoryDAL<CustomerDbObject> databaseLayer0 = DALFactory.CreateCustomerDAL(DALType.ADOOLEDB);
            IRepositoryDAL<CustomerDbObject> databaseLayer1 = DALFactory.CreateCustomerDAL(DALType.EF);
            IEnumerable<CustomerDbObject> allCustomers = databaseLayer.Search();
        }

        public void UOW() // should be within one transaction unit
        {
            CustomerDbObject cust = new Customer();
            cust.CustomerName = "sample customer";
            CustomerDbObject lead = new Lead();
            lead.CustomerName = "sample lead";

            IRepositoryDAL<CustomerDbObject> dal = DALFactory.CreateCustomerDAL(DALType.ADOSQL);
            IUnitOfWork uow = new SQLUnitOfWork(GlobalConnectionString.ConnString);
            dal.SetUnitWork(uow);
            dal.Add(cust);
            try
            {
                uow.Commit();
            }
            catch(Exception)
            {
                uow.RollBack();
            }

            IRepositoryDAL<CustomerDbObject> dal2 = DALFactory.CreateCustomerDAL(DALType.EF);
            IUnitOfWork uow2 = new EFUnitOfWork(GlobalConnectionString.ConnString);
            dal2.SetUnitWork(uow2);
            dal2.Add(lead);
            dal2.Add(cust);
            try
            {
                uow2.Commit();
            }
            catch (Exception)
            {
                uow2.RollBack();
            }

        }

    }




}
