using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.Interfaces
{
    public enum CustomerType
    {
        [Description("Lead"), Category("CustomerType")]
        Lead,
        [Description("Customer"), Category("CustomerType")]
        Customer
    }

    public interface ICustomer
    {
        int ID { get; set; }
        string CustomerName { get; set; }
        string PhoneNumber { get; set; }
        decimal BillAmount { get; set; }
        DateTime BillDate { get; set; }
        string CustomerAddress { get; set; }
        CustomerType InternalType { get; set; }

        bool Validate();
    }

    /// <summary>
    /// Need this abstract class for Entity Framework
    /// EF cannot directly link to Interface
    /// </summary>
    public abstract class CustomerDbObject : ICustomer
    {
        public CustomerDbObject() { }

        [Key] // key data annotation, EF needs a proporty that is uniqle
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string CustomerAddress { get; set; }
        public CustomerType InternalType { get; set; }

        public abstract bool Validate();
    }

}
