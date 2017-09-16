using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.DataBase
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
    public class DataTableAttribute : Attribute
    {
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        public DataTableAttribute(string tableName)
        {
            this.tableName = tableName;
        }

    }
}
