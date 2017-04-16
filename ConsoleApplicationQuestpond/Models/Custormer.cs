using ConsoleApplicationQuestpond.Interfaces;
using System;

namespace ConsoleApplicationQuestpond.Models
{
    public class Customer : BasicCustomer
    {
        public Customer() : base()
        {
            InternalType = CustomerType.Customer;
        }
        public Customer(IValidate<ICustomer> validate) : base(validate)
        {
            InternalType = CustomerType.Customer;
        }
        public Customer(string customerName, string phoneNumber,
            decimal billAmount, DateTime billDate, string customerAddress, IValidate<ICustomer> validate)
            : base(customerName, phoneNumber, validate)
        {
            BillAmount = billAmount;
            BillDate = billDate;
            CustomerAddress = customerAddress;
            InternalType = CustomerType.Customer;
        }
    }

    public class Lead : BasicCustomer
    {
        public Lead() : base()
        {
            InternalType = CustomerType.Lead;
        }
        public Lead(IValidate<ICustomer> validate) : base(validate)
        {
            InternalType = CustomerType.Lead;
        }
        public Lead(string customerName, string phoneNumber, IValidate<ICustomer> validate)
            : base(customerName, phoneNumber, validate)
        {
            InternalType = CustomerType.Lead;
        }
    }

    /// <summary>
    /// Add more constructors : add validataion method
    /// </summary>
    public abstract class BasicCustomer : CustomerDbObject
    {
        private IValidate<ICustomer> validation = null;

        public BasicCustomer(): base() { }
        public BasicCustomer(IValidate<ICustomer> validate)
        {
            validation = validate;
        }
        public BasicCustomer(string customerName, string phoneNumber, IValidate<ICustomer> validate)
        {
            CustomerName = customerName;
            PhoneNumber = phoneNumber;
            validation = validate;
        }
        public override bool Validate()
        {
            return validation.Validate(this);
        }
    }

    
}
