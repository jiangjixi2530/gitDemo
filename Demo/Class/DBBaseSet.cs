/*************************************************************************************
 * Copyright (c) 2017 银盒宝成 All Rights Reserved.
 * 当前版本：       4.0.30319.42000
 * 机器名称：       XIGUA
 * 命名空间：       DALManage.DBUtility
 * 文件名称：       DBBaseSet
 * 作者信息：       JJX
 * 创建时间：       2017-11-15 10:18:25
 * 描述说明：       DBBaseSet 数据库类型和连接设置
*************************************************************************************/

using System.Configuration;

namespace DALManage.DBUtility
{
    /// <summary>
    /// 数据库类型和连接设置
    /// </summary>
    public static class DbBaseSet
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public static string ConnectString
        {
            get
            {
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                //string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                return connectionString;
            }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbTypes DbType
        {
            get { return 0; }
        }
    }
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbTypes
    {
        SQLSERVER,
        SQLLITE,
    }
}
