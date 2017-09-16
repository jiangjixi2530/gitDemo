using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YST
{
    public partial class Form1 : Form
    {
        frmLoading loadform = new frmLoading();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "JSON文件|*.json;*.txt|所有文件|*.*";
            file.ShowDialog();
            this.txtFilePath.Text = file.FileName;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ReadJson read = new ReadJson(this.txtFilePath.Text);
            ReadResult result = read.ReadFile();
            if (result.IsSucess)
            {
                List<Goods> goodsList = read.Goods;
                List<UnitType> unitTypeList = read.UnitType;
                List<SubGroup> subGroupList = read.SubGroup;
                List<ProductType> productTypeList = read.ProductType;
                List<Group> groupList = read.Group;
                foreach (Goods g in goodsList)
                {
                    UnitType unit = unitTypeList.Find(u => u.Id == g.UnitType);
                    SubGroup subgroup = subGroupList.Find(sg => sg.Id == g.SubGroup);
                    if (unit != null)
                    {
                        g.UnitTypeName = unit.Name;
                    }
                    if (subgroup != null)
                    {
                        g.SubGroupName = subgroup.Name;
                        Group group = groupList.Find(gr => gr.Id == subgroup.GroupId);
                        if (group != null)
                        {
                            ProductType producttype = productTypeList.Find(pt => pt.Id == group.ProductTypeId);
                            if (producttype != null)
                            {
                                g.IsWeight = producttype.IsWeight;
                                g.IsPackage = producttype.IsSet;
                            }
                        }
                    }
                }
                this.gridGoods.DataSource = goodsList;
                this.labTotal.Text = "合计：" + goodsList.Count.ToString() + "项菜品";
                this.btnUpdate.Visible = goodsList.Count > 0;
            }
        }
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<Goods> goodsList = this.gridGoods.DataSource as List<Goods>;
            if (goodsList == null || goodsList.Count == 0)
            {
                MessageBox.Show("无菜品数据");
                return;
            }
            try
            {
                DbHelperMySQL.connectionString = string.Format(DbHelperMySQL.conn, this.txtAddress.Text, this.txtUser.Text, this.txtPwd.Text, this.txtDataBase.Text);
                if (!string.IsNullOrEmpty(this.txtPort.Text))
                {
                    DbHelperMySQL.connectionString = DbHelperMySQL.connectionString + string.Format("Port=;", this.txtPort.Text);
                }
                if (!DbHelperMySQL.TryConnOpen())
                {
                    MessageBox.Show("连接数据库失败!");
                    return;
                }
                loadform = new frmLoading();
                Thread th = new Thread(UpdateData);
                th.IsBackground = true;
                th.Start();
                loadform.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败：" + ex.Message);
                return;
            }

        }
        private void UpdateData()
        {

            List<Goods> goodsList = this.gridGoods.DataSource as List<Goods>;

            try
            {
                if (JsonToDataBase.UpdateJsonData(goodsList))
                {
                    this.Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("数据导入成功！");
                        });
                }
                else
                {
                    this.Invoke((EventHandler)delegate
                        {
                            MessageBox.Show("数据导入失败!");
                        });
                }
            }
            catch (Exception ex)
            {
                this.Invoke((EventHandler)delegate
                    {
                        MessageBox.Show("数据导入失败:" + ex.Message);
                    });
            }
            finally
            {
                this.Invoke((EventHandler)delegate
                   {
                       loadform.Close();
                   });
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.gridGoods.AutoGenerateColumns = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
