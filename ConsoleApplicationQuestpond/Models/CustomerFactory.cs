using ConsoleApplicationQuestpond.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace ConsoleApplicationQuestpond.Models
{
    public static class CustomerFactory
    {
        /// <summary>
        /// Design Pattern : Simple Facotry Pattern, Centralize new keyword
        /// </summary>
        public static CustomerDbObject CreateCustomer(CustomerType type)
        {
            switch (type)
            {
                case CustomerType.Customer:
                    return new Customer(new CustomerValidationMethod());
                case CustomerType.Lead:
                    return new Lead(new LeadValidationMethod());
                default:
                    throw new ArgumentException("which type?");
            }
        }
    }

    public static class LazyLoadingCustomerFactory
    {
        private static IDictionary<CustomerType, Lazy<CustomerDbObject>> custs
            = new Dictionary<CustomerType, Lazy<CustomerDbObject>>();

        /// <summary>
        /// Design Pattern : Lazy loading
        /// Design Pattern : RIP (Replace it with Polymorphism)
        /// </summary>
        static LazyLoadingCustomerFactory()
        {
            custs[CustomerType.Customer] = new Lazy<CustomerDbObject>(() => new Customer(new CustomerValidationMethod()));
            custs[CustomerType.Lead] = new Lazy<CustomerDbObject>(() => new Lead(new LeadValidationMethod()));
        }

        public static CustomerDbObject CreateCustomer(CustomerType type)
        {
            //if (!custs[type].IsValueCreated) throw new ArgumentException("No lazy loading");
            return custs[type].Value;
        }
    }


    /// <summary>
    /// Unity
    /// </summary>
    public static class CustomerFactoryUnity
    {
        private static IUnityContainer custs = null;

        public static ICustomer Create(CustomerType type)
        {
            if (custs == null)
            {
                custs = new UnityContainer();
                custs.RegisterType<CustomerDbObject, Customer>("Customer", new InjectionConstructor(new CustomerValidationMethod()));
                custs.RegisterType<CustomerDbObject, Lead>("Lead", new InjectionConstructor(new LeadValidationMethod()));
            }
            return custs.Resolve<CustomerDbObject>(type.ToString());
        }
    }

}
