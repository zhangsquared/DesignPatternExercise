using ConsoleApplicationQuestpond.Interfaces;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public class CustomerEFDAL : EFDAL<CustomerDbObject>
    {
        public CustomerEFDAL(string connString) : base(connString)
        {
        }
    }
}
