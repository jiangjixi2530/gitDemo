using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;

namespace Tools
{
    /// <summary>
    /// 分页Panel，by .net小组 jjx
    /// </summary>
    public class BoxPagePanel : Panel
    {
        #region 全局变量定义
        /// <summary>
        /// 首页
        /// </summary>
        public readonly int homepage = 1;
        /// <summary>
        /// 需要分页的控件集合
        /// </summary>
        private object dataSource;
        /// <summary>
        /// 需要分页的控件集合
        /// </summary>
        private List<Control> listSource;
        /// <summary>
        /// 列数
        /// </summary>
        private int columnsCount = 1;
        /// <summary>
        /// 行数
        /// </summary>
        private int rowsCount = 1;
        /// <summary>
        /// 每页数量
        /// </summary>
        private int pageControlCount = 1;
        /// <summary>
        /// 页数集合
        /// </summary>
        private List<Panel> listPages;
        /// <summary>
        /// 控件之间左右间距
        /// </summary>
        private int inSpaceLeftAndRight;
        /// <summary>
        /// 控件之间上下间距
        /// </summary>
        private int inSpaceTopAndBottom;
        #endregion
        #region 可配置属性定义
        /// <summary>
        /// 外边距
        /// </summary>
        [Description("外边距"), Category("WinBoxProperty")]
        [DefaultValue(0)]
        public Padding OutSpace { get; set; }
        /// <summary>
        /// 分页方法
        /// </summary>
        [Description("分页方法"), Category("WinBoxProperty")]
        [DefaultValue(PageMethodEnum.Normal)]
        public PageMethodEnum PageMethod { get; set; }
        /// <summary>
        /// 内间距的左右边距，当值大于0时，外边距的右边距将失效
        /// </summary>
        [Description("控件之间左右边距，设置后外边距的右边距将失效"), Category("WinBoxProperty")]
        [DefaultValue(0)]
        public int InSpaceLeftAndRight { get; set; }
        /// <summary>
        /// 内间距的左右边距，当值大于0时，外边距的下边距将失效
        /// </summary>
        [Description("控件之间上下边距，设置后外边距下边距将失效"), Category("WinBoxProperty")]
        [DefaultValue(0)]
        public int InSpaceTopAndBottom { get; set; }
        /// <summary>
        /// 需要分页的数据源
        /// </summary>           
        [Description("需要分页的数据源"), Category("WinBoxProperty")]
        public object DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
                Paging();
                if (DataSourceChanged != null)
                {
                    DataSourceChanged(this, new EventArgs());
                }
            }
        }
        /// <summary>
        ///  当前页
        /// </summary>   
        [Description("当前页"), Category("WinBoxProperty")]
        public int CurrentPage { get; private set; }
        /// <summary>
        /// 总页数
        /// </summary>      
        [Description("总页数"), Category("WinBoxProperty")]
        public int TotalPage { get; private set; }
        #endregion
        #region 事件定义
        /// <summary>
        /// 定义数据源变化事件
        /// </summary>
        [Description("分页数据源变化后发生"), Category("WinBoxProperty")]
        public event EventHandler DataSourceChanged;
        /// <summary>
        /// 定义翻页事件
        /// </summary>
        [Description("页面翻页后发生"), Category("WinBoxProperty")]
        public event EventHandler PageTurned;
        #endregion
        /// <summary>
        /// 外边距默认10，控件之间的边距自动排版和计算
        /// </summary>
        public BoxPagePanel()
        {
            OutSpace = new Padding(10, 10, 10, 10);
            CurrentPage = 1;
            TotalPage = 1;
        }
        /// <summary>
        /// 重写 防止刷新时界面闪烁
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        private void Paging()
        {
            if (!IsHandleCreated) return;
            try
            {
                CurrentPage = homepage;
                TotalPage = homepage;
                listSource = new List<Control>();
                listPages = new List<Panel>();
                foreach (var item in (IEnumerable)DataSource)
                {
                    listSource.Add(item as Control);
                }
                while (Controls.Count > 0)
                {
                    var c = Controls[0];
                    Controls.Remove(c);
                    //c.Dispose();
                }
                if (listSource == null || listSource.Count <= 0) return;
                switch (PageMethod)
                {
                    case PageMethodEnum.Normal:
                        PagingByNormal();
                        break;
                    case PageMethodEnum.Abnormity:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 无异形分页
        /// </summary>
        private void PagingByNormal()
        {
            if (InSpaceLeftAndRight > 0)
            {
                inSpaceLeftAndRight = InSpaceLeftAndRight;
                columnsCount = (Width + InSpaceLeftAndRight - OutSpace.Left) /
                                (listSource[0].Width + InSpaceLeftAndRight);
            }
            else
            {
                columnsCount = (Width - OutSpace.Left - OutSpace.Right) / listSource[0].Width;
                if (columnsCount > 0)
                {
                    inSpaceLeftAndRight = (Width - OutSpace.Left - OutSpace.Right) % listSource[0].Width /
                                          columnsCount;
                }
            }
            if (InSpaceTopAndBottom > 0)
            {
                inSpaceTopAndBottom = InSpaceTopAndBottom;
                rowsCount = (Height + InSpaceTopAndBottom - OutSpace.Top) /
                             (listSource[0].Height + InSpaceTopAndBottom);
            }
            else
            {
                rowsCount = (Height - OutSpace.Top - OutSpace.Bottom) / listSource[0].Height;
                if (rowsCount > 0)
                {
                    inSpaceTopAndBottom = (Height - OutSpace.Top - OutSpace.Bottom) % listSource[0].Height /
                                          rowsCount;
                }
            }
            columnsCount = columnsCount == 0 ? 1 : columnsCount;
            rowsCount = rowsCount == 0 ? 1 : rowsCount;
            pageControlCount = columnsCount * rowsCount;
            TotalPage = listSource.Count / pageControlCount;
            if (listSource.Count % pageControlCount > 0)
                TotalPage++;
            FillUpControls();
            AppointPage(homepage);
        }

        #region 翻页事件
        /// <summary>
        /// 上一页
        /// </summary>
        public void NextPage()
        {
            if (CurrentPage == homepage)
                return;
            ShowContolByPage(CurrentPage - 1);
            if (PageTurned != null) PageTurned(this, new EventArgs());
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public void PreviousPage()
        {
            if (CurrentPage == TotalPage)
                return;
            ShowContolByPage(CurrentPage + 1);
            if (PageTurned != null) PageTurned(this, new EventArgs());
        }
        /// <summary>
        /// 指定页跳转
        /// </summary>
        /// <param name="page"></param>
        public void AppointPage(int page)
        {
            if (page == CurrentPage || page <= 0 || page > TotalPage) return;
            ShowContolByPage(page);
            if (PageTurned != null) PageTurned(this, new EventArgs());
        }
        /// <summary>
        /// 根据页码显示
        /// </summary>
        /// <param name="page"></param>
        private void ShowContolByPage(object page)
        {
            try
            {
                // ReSharper disable once InconsistentNaming
                var Page = page as int? ?? 0;
                for (var i = 0; i < listPages.Count; i++)
                {
                    if (listPages[i].Visible && i != Page - 1)
                        listPages[i].Visible = false;
                    if (i == Page - 1)
                        listPages[i].Visible = true;
                }
                CurrentPage = Page;
                if (PageTurned != null) PageTurned(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        /// <summary>
        /// 将控件加载进PANEL
        /// </summary>
        private void FillUpControls()
        {
            for (var i = 0; i < TotalPage; i++)
            {
                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Visible = false
                };
                for (var j = i * pageControlCount; j < (i + 1) * pageControlCount && j < listSource.Count; j++)
                {
                    var orderByCount = j % pageControlCount;
                    listSource[j].Location = new Point((orderByCount % columnsCount) * (listSource[j].Width + inSpaceLeftAndRight) + OutSpace.Left, (orderByCount / columnsCount) * (listSource[j].Height + inSpaceTopAndBottom) + OutSpace.Top);
                    panel.Controls.Add(listSource[j]);
                }
                Controls.Add(panel);
                listPages.Add(panel);
            }
            if (listPages.Count > 0)
                listPages[0].Visible = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Paging();
            if (DataSourceChanged != null)
                DataSourceChanged(this, new AddingNewEventArgs());
            base.OnSizeChanged(e);
        }
    }
}
