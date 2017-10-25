using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YST
{
    public class ReadJson
    {
        /// <summary>
        /// Json文件地址（绝对地址）
        /// </summary>
        private string FilePath = string.Empty;
        /// <summary>
        /// Json文件内容
        /// </summary>
        public JObject JsonObject = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
        public ReadJson(string path)
        {
            FilePath = path;
        }
        /// <summary>
        /// 读取Json文件
        /// </summary>
        public ReadResult ReadFile()
        {
            ReadResult result = new ReadResult();
            if (string.IsNullOrEmpty(FilePath))
            {
                result.Msg = "Json文件地址不能为空！";
                return result;
            }
            if (!File.Exists(FilePath))
            {
                result.Msg = "Json文件地址不存在！";
                return result;
            }
            try
            {
                JsonObject = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(FilePath));
                if (JsonObject != null)
                {
                    result.IsSucess = true;
                    result.Msg = "数据读取成功";
                    return result;
                }
                result.Msg = "数据为空";
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                return result;
            }
        }
        /// <summary>
        /// 读取菜品单位列表
        /// </summary>
        public List<UnitType> UnitType
        {
            get
            {
                List<UnitType> listUnit = UnitTypeToList((JArray)JsonObject["systemMeta"]["unitTypes"]);
                return listUnit;
            }
        }
        /// <summary>
        /// 读取菜品类型
        /// </summary>
        public List<ProductType> ProductType
        {
            get
            {
                List<ProductType> list = ProductTypeToList((JArray)JsonObject["systemMeta"]["productTypes"]);
                return list;
            }
        }
        /// <summary>
        /// 读取菜品大类
        /// </summary>
        public List<Group> Group
        {
            get
            {
                List<Group> list = GroupToList((JArray)JsonObject["brandMeta"]["groups"]);
                return list;
            }
        }
        /// <summary>
        /// 读取菜品小类列表
        /// </summary>
        public List<SubGroup> SubGroup
        {
            get
            {
                List<SubGroup> list = SubGroupToList((JArray)JsonObject["brandMeta"]["subgroups"]);
                return list;
            }
        }
        /// <summary>
        /// 读取菜品信息
        /// </summary>
        public List<Goods> Goods
        {
            get
            {
                List<Goods> list = GoodsToList((JArray)JsonObject["brandMeta"]["goodses"]);
                return list;
            }
        }
        /// <summary>
        /// Json转单位
        /// </summary>
        /// <param name="jarray"></param>
        /// <returns></returns>
        private List<UnitType> UnitTypeToList(JArray jarray)
        {
            List<UnitType> listUnit = new List<UnitType>();
            try
            {
                foreach (var item in jarray)
                {
                    try
                    {
                        UnitType u = new UnitType();
                        u.Id = item["id"].ToString();
                        u.Name = item["name"].ToString();
                        listUnit.Add(u);
                    }
                    catch
                    {
                    }
                }

            }
            catch
            {
            }
            return listUnit;
        }
        /// <summary>
        /// Json转菜品类型
        /// </summary>
        /// <param name="jarray"></param>
        /// <returns></returns>
        private List<ProductType> ProductTypeToList(JArray jarray)
        {
            List<ProductType> listProductType = new List<ProductType>();
            try
            {
                foreach (var item in jarray)
                {
                    try
                    {
                        ProductType p = new ProductType();
                        p.Id = item["id"].ToString();
                        p.Name = item["name"].ToString();
                        p.IsSet = item["isSet"].ToString().ToUpper() == "FALSE" ? "0" : "1";
                        p.IsWeight = item["isWeight"].ToString().ToUpper() == "FALSE" ? "0" : "1";
                        listProductType.Add(p);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {

            }
            return listProductType;
        }
        /// <summary>
        /// Json转菜品大类
        /// </summary>
        /// <param name="jarray"></param>
        /// <returns></returns>
        private List<Group> GroupToList(JArray jarray)
        {
            List<Group> listGroup = new List<Group>();
            try
            {
                foreach (var item in jarray)
                {
                    try
                    {
                        Group s = new Group();
                        s.Id = item["id"].ToString();
                        s.Name = item["name"].ToString();
                        s.ProductTypeId = item["productType"].ToString();
                        listGroup.Add(s);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {

            }
            return listGroup;
        }
        /// <summary>
        /// json转菜品小类
        /// </summary>
        /// <param name="jarray"></param>
        /// <returns></returns>
        private List<SubGroup> SubGroupToList(JArray jarray)
        {
            List<SubGroup> listSubGroup = new List<SubGroup>();
            try
            {
                foreach (var item in jarray)
                {
                    try
                    {
                        SubGroup s = new SubGroup();
                        s.Id = item["id"].ToString();
                        s.Name = item["name"].ToString();
                        s.GroupId = item["group"].ToString();
                        listSubGroup.Add(s);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {

            }
            return listSubGroup;
        }
        /// <summary>
        /// json转菜品信息
        /// </summary>
        /// <param name="jarray"></param>
        /// <returns></returns>
        private List<Goods> GoodsToList(JArray jarray)
        {
            List<Goods> listGoods = new List<Goods>();
            try
            {
                foreach (var item in jarray)
                {
                    try
                    {
                        Goods g = new Goods();
                        g.Id = item["id"].ToString();
                        g.Name = item["name"].ToString();
                        g.SubGroup = item["subgroup"].ToString();
                        g.UnitType = item["unitType"].ToString();
                        listGoods.Add(g);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {

            }
            return listGoods;
        }
    }
    /// <summary>
    /// 读取结果
    /// </summary>
    public class ReadResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucess = false;
        /// <summary>
        /// 信息
        /// </summary>
        public string Msg = string.Empty;
    }
}
