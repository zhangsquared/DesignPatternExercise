using ConsoleApplicationQuestpond.Interfaces;
using ConsoleApplicationQuestpond.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public class EFUnitOfWork : DbContext, IUnitOfWork
    {
        private string tableName = "CustomerTable";
        private string[] columnNames = { "CustomerName", "BillAmount", "BillDate", "PhoneNumber", "Address", "CustomerType" };


        public EFUnitOfWork(string connString) : base(connString)
        {
        }
        public void Commit()
        {
            SaveChanges();
        }

        public void RollBack()
        {
            Dispose();
        }

        // mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDbObject>().ToTable(tableName); // cannot create an abstract class
            modelBuilder.Entity<CustomerDbObject>() // create concrete classes here
                .Map<Customer>(m => m.Requires(columnNames[5]).HasValue(CustomerType.Customer.ToString()))
                .Map<Lead>(m => m.Requires(columnNames[5]).HasValue(CustomerType.Lead.ToString()));
            // notice that CustomerInformation's contructor needs an IValidation parameters
            modelBuilder.Entity<CustomerDbObject>().Ignore(t => t.InternalType);
        }
    }
}
