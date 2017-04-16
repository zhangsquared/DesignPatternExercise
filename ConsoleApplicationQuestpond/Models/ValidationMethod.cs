using ConsoleApplicationQuestpond.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.Models
{
    public class CustomerValidationMethod : IValidate<ICustomer>
    {
        public bool Validate(ICustomer info)
        {
            return !string.IsNullOrWhiteSpace(info.CustomerName)
                && !string.IsNullOrWhiteSpace(info.PhoneNumber)
                && !string.IsNullOrWhiteSpace(info.CustomerAddress)
                && !string.IsNullOrWhiteSpace(info.PhoneNumber)
                && !string.IsNullOrWhiteSpace(info.CustomerName)
                && !string.IsNullOrWhiteSpace(info.PhoneNumber);
        }
    }

    public class LeadValidationMethod : IValidate<ICustomer>
    {
        public bool Validate(ICustomer info)
        {
            return !string.IsNullOrWhiteSpace(info.CustomerName)
                && !string.IsNullOrWhiteSpace(info.PhoneNumber);
        }
    }

    
}
