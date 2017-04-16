using ConsoleApplicationQuestpond.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationQuestpond.DatabaseLayer
{
    public static class GlobalConnectionString
    {
        public static string ConnString =>
            @"Initial Catalog=DatabaseName;Server=MSSQL1;;Integrated Security=true;";
    }

}
