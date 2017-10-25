using System;

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
