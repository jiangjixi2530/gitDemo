using System;
using System.Collections.Generic;
using System.Text;

namespace YST
{
    public class JsonToDataBase
    {
        private const string tableName = "box_goodsInfo";
        /// <summary>
        ///  同步更新数据
        /// </summary>
        /// <param name="goodsList"></param>
        /// <returns></returns>
        public static bool UpdateJsonData(List<Goods> goodsList)
        {
            string createSql = @"create table if not exists `" + tableName + @"`(`id`  bigint NOT NULL AUTO_INCREMENT ,
                                `dishId`  varchar(64) NULL ,
                                `dishName`  varchar(64) NULL ,
                                `type`  varchar(64) NULL ,
                                `dishPrice` varchar(64) null,
                                `isPackage` varchar(64) null,
                                `isWeight` varchar(64) null,
                                `categoryName`  varchar(64) NULL ,
                                PRIMARY KEY (`id`)
                                );";
            string clearSql = @"truncate table " + tableName + ";";
            string insertSql = "insert into " + tableName + " (dishId,dishName,type,dishPrice,isPackage,isWeight,categoryName)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            List<string> sqlList = new List<string>();
            sqlList.Add(createSql);
            sqlList.Add(clearSql);
            foreach (Goods goods in goodsList)
            {
                sqlList.Add(string.Format(insertSql, goods.Id, goods.Name, goods.UnitTypeName,0,goods.IsPackage,goods.IsWeight, goods.SubGroupName));
            }
            try
            {
                int i = DbHelperMySQL.ExecuteSqlTran(sqlList);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
