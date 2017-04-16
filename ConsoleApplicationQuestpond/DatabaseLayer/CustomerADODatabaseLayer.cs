using ConsoleApplicationQuestpond.Interfaces;
using ConsoleApplicationQuestpond.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public class CustomerSQLDAL : SQLDAL<CustomerDbObject>
    {
        protected string tableName = "CustomerTable";
        protected string[] columnNames = { "CustomerName", "BillAmount", "BillDate", "PhoneNumber", "Address", "CustomerType" };

        public CustomerSQLDAL(string connString) : base(connString)
        {
        }

        protected override void ExecuteInsertCommand(CustomerDbObject t)
        {
            command.CommandText = $"INSERT INTO {tableName} ("
                + string.Join(",", columnNames)
                + $") VALUES ('{t.CustomerName}', '{t.BillAmount}', '{t.BillDate}', '{t.PhoneNumber}', '{t.CustomerAddress}', '{t.InternalType}')";
            command.ExecuteNonQuery();
        }

        protected override IEnumerable<CustomerDbObject> ExecuteSearchCommand()
        {
            command.CommandText = $"SELECT * FROM {tableName}";
            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustomerDbObject cust = null;
                string typeString = reader[columnNames[5]].ToString();
                // Method 1: From value
                CustomerType cType;
                if (Enum.TryParse(typeString, out cType))
                {
                    cust = CustomerFactory.CreateCustomer(cType);
                }
                else
                {
                    throw new ArgumentNullException("Error at loading Customer table: don't know what type");
                }
                //// Method 2: From description
                //CustomerType type = EnumExtension.FromDescription<CustomerType>(typeString);
                //cust = CustomerFactory.CreateCustomer(type);

                cust.CustomerAddress = reader[columnNames[0]].ToString();
                cust.BillAmount = Convert.ToDecimal(reader[columnNames[1]]);
                cust.BillDate = Convert.ToDateTime(reader[columnNames[2]]);
                cust.PhoneNumber = reader[columnNames[3]].ToString();
                cust.CustomerAddress = reader[columnNames[4]].ToString();
                yield return cust;
            }
        }
    }

    public class CustomerOLEDAL : OLEDAL<CustomerDbObject>
    {
        protected string tableName = "CustomerTable";
        protected string[] columnNames = { "CustomerName", "BillAmount", "BillDate", "PhoneNumber", "Address", "CustomerType" };

        public CustomerOLEDAL(string connString) : base(connString)
        {
        }

        protected override void ExecuteInsertCommand(CustomerDbObject t)
        {
            command.CommandText = $"INSERT INTO {tableName} ("
                + string.Join(",", columnNames)
                + $") VALUES ('{t.CustomerName}', '{t.BillAmount}', '{t.BillDate}', '{t.PhoneNumber}', '{t.CustomerAddress}', '{t.InternalType}')";
            command.ExecuteNonQuery();
        }

        protected override IEnumerable<CustomerDbObject> ExecuteSearchCommand()
        {
            command.CommandText = $"SELECT * FROM {tableName}";
            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CustomerDbObject cust = null;
                string typeString = reader[columnNames[5]].ToString();
                // Method 1: From value
                CustomerType cType;
                if (Enum.TryParse(typeString, out cType))
                {
                    cust = CustomerFactory.CreateCustomer(cType);
                }
                else
                {
                    throw new ArgumentNullException("Error at loading Customer table: don't know what type");
                }
                //// Method 2: From description
                //CustomerType type = EnumExtension.FromDescription<CustomerType>(typeString);
                //cust = CustomerFactory.CreateCustomer(type);

                cust.CustomerAddress = reader[columnNames[0]].ToString();
                cust.BillAmount = Convert.ToDecimal(reader[columnNames[1]]);
                cust.BillDate = Convert.ToDateTime(reader[columnNames[2]]);
                cust.PhoneNumber = reader[columnNames[3]].ToString();
                cust.CustomerAddress = reader[columnNames[4]].ToString();
                yield return cust;
            }
        }
    }

}
