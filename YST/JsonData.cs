using System;
using System.Collections.Generic;
using System.Text;

namespace YST
{
    /// <summary>
    /// 单位
    /// </summary>
    public class UnitType
    {
        /// <summary>
        /// 单位Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
    }
    public class ProductType
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 类型名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否是套餐
        /// </summary>
        public string IsSet { get; set; }
        /// <summary>
        /// 是否称重
        /// </summary>
        public string IsWeight { get; set; }
    }
    /// <summary>
    /// 菜品大类
    /// </summary>
    public class Group
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品类型ID
        /// </summary>
        public string ProductTypeId { get; set; }
    }
    /// <summary>
    /// 菜品小类
    /// </summary>
    public class SubGroup
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 小类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 大类Id
        /// </summary>
        public string GroupId { get; set; }
    }
    /// <summary>
    /// 菜品信息
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 菜品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位Id
        /// </summary>
        public string UnitType { get; set; }
        /// <summary>
        /// 小类名称
        /// </summary>
        public string UnitTypeName { get; set; }
        /// <summary>
        /// 菜品小类Id
        /// </summary>
        public string SubGroup { get; set; }
        /// <summary>
        /// 菜品小类
        /// </summary>
        public string SubGroupName { get; set; }
        /// <summary>
        /// 是否是套餐
        /// </summary>
        public string IsPackage { get; set; }
        /// <summary>
        /// 是否称重
        /// </summary>
        public string IsWeight { get; set; }
    }
}
