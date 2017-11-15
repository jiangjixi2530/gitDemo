/*************************************************************************************
 * Copyright (c) 2017 银盒宝成 All Rights Reserved.
 * 当前版本：       4.0.30319.42000
 * 机器名称：       XIGUA
 * 命名空间：       DALManage.DBUtility
 * 文件名称：       DbHelper
 * 作者信息：       JJX
 * 创建时间：       2017-11-15 10:46:43
 * 描述说明：       DbHelper 的摘要说明<未填写>
*************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace DALManage.DBUtility
{
    /// <summary>
    /// DbHelper 的摘要说明
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        private static DbTypes dbType = DbBaseSet.DbType;
        /// <summary>
        /// 获取某一列最大值
        /// </summary>
        /// <param name="fieldName">列名</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static int GetMaxId(string fieldName, string tableName)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.GetMaxID(fieldName, tableName);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.GetMaxID(fieldName, tableName);
                default:
                    return DbHelperSQL.GetMaxID(fieldName, tableName);
            }
        }
        /// <summary>
        /// 判断sql执行返回值是否大于0
        /// </summary>
        /// <param name="strSql">sql</param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.Exists(strSql);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.Exists(strSql);
                default:
                    return DbHelperSQL.Exists(strSql);
            }
        }

        public static bool Exists(string strSql, params DbParameter[] cmdParms)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.Exists(strSql, cmdParms as SqlParameter[]);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.Exists(strSql, cmdParms as SQLiteParameter[]);
                default:
                    return DbHelperSQL.Exists(strSql, cmdParms as SqlParameter[]);
            }
        }
        /// <summary>
        /// 执行sql语句，返回影响的记录行数
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static int ExecuteSql(string sqlString)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.ExecuteSql(sqlString);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.ExecuteSql(sqlString);
                default:
                    return DbHelperSQL.ExecuteSql(sqlString);
            }
        }
        /// <summary>
        /// 事务执行多条sql
        /// </summary>
        /// <param name="sqlStringList"></param>
        public static void ExecuteSqlTran(List<string> sqlStringList)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    DbHelperSQL.ExecuteSqlTran(sqlStringList);
                    break;
                case DbTypes.SQLLITE:
                    DbHelperSQLite.ExecuteSqlTran(sqlStringList);
                    break;
                default:
                    DbHelperSQL.ExecuteSqlTran(sqlStringList);
                    break;
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, string content)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.ExecuteSql(sqlString, content);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.ExecuteSql(sqlString, content);
                default:
                    return DbHelperSQL.ExecuteSql(sqlString, content);
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSql, byte[] fs)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.ExecuteSqlInsertImg(strSql, fs);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.ExecuteSqlInsertImg(strSql, fs);
                default:
                    return DbHelperSQL.ExecuteSqlInsertImg(strSql, fs);
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sqlString)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.GetSingle(sqlString);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.GetSingle(sqlString);
                default:
                    return DbHelperSQL.GetSingle(sqlString);
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.Query(sqlString);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.Query(sqlString);
                default:
                    return DbHelperSQL.Query(sqlString);
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, params DbParameter[] cmdParms)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.ExecuteSql(sqlString, cmdParms as SqlParameter[]);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.ExecuteSql(sqlString, cmdParms as SQLiteParameter[]);
                default:
                    return DbHelperSQL.ExecuteSql(sqlString, cmdParms as SqlParameter[]);
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable sqlStringList)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    DbHelperSQL.ExecuteSqlTran(sqlStringList);
                    break;
                case DbTypes.SQLLITE:
                    DbHelperSQLite.ExecuteSqlTran(sqlStringList);
                    break;
                default:
                    DbHelperSQL.ExecuteSqlTran(sqlStringList);
                    break;
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sqlString, params DbParameter[] cmdParms)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.GetSingle(sqlString, cmdParms as SqlParameter[]);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.GetSingle(sqlString, cmdParms as SQLiteParameter[]);
                default:
                    return DbHelperSQL.GetSingle(sqlString, cmdParms as SqlParameter[]);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString, params DbParameter[] cmdParms)
        {
            switch (dbType)
            {
                case DbTypes.SQLSERVER:
                    return DbHelperSQL.Query(sqlString, cmdParms as SqlParameter[]);
                case DbTypes.SQLLITE:
                    return DbHelperSQLite.Query(sqlString, cmdParms as SQLiteParameter[]);
                default:
                    return DbHelperSQL.Query(sqlString, cmdParms as SqlParameter[]);
            }
        }

    }
}
