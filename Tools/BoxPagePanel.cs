using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
        public readonly int Homepage = 1;
        /// <summary>
        /// 需要分页的控件集合
        /// </summary>
        private List<Control> _dataSource;
        /// <summary>
        /// 列数
        /// </summary>
        private int _columnsCount = 1;
        /// <summary>
        /// 行数
        /// </summary>
        private int _rowsCount = 1;
        /// <summary>
        /// 每页数量
        /// </summary>
        private int _pageControlCount = 1;
        /// <summary>
        /// 页数集合
        /// </summary>
        private List<Panel> _allPages;
        /// <summary>
        /// 搜索后的页Panel集合
        /// </summary>
        private List<Panel> _searchPages;
        #endregion
        #region 可配置属性定义
        /// <summary>
        /// 外边距
        /// </summary>
        [Description("外边距"), Category("WinBoxProperty")]
        public Padding OutSpace { get; set; }
        /// <summary>
        /// 分页方法
        /// </summary>
        [Description("分页方法"), Category("WinBoxProperty")]
        public PageMethodEnum PageMethod { get; set; }
        /// <summary>
        /// 内间距的左右边距，当值大于0时，外边距的右边距将失效
        /// </summary>
        [Description("内间左右边距，设置后外边距的右边距将失效"), Category("WinBoxProperty")]
        public int InSpaceLeftAndRight { get; set; }
        /// <summary>
        /// 内间距的左右边距，当值大于0时，外边距的下边距将失效
        /// </summary>
        [Description("内间距的上下边距，设置后外边距下边距将失效"), Category("WinBoxProperty")]
        public int InSpaceTopAndBottom { get; set; }
        /// <summary>
        /// 需要分页的数据源,list必须是Control类型或Control子类
        /// (赋值示例：list.ConvertAll<Control/>(input => input as Control);)
        /// </summary>           
        [Description("需要分页的数据源"), Category("WinBoxProperty")]
        public List<Control> DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
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
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        private void Paging(bool isSearch = false)
        {
            if (!IsHandleCreated) return;
            try
            {
                CurrentPage = Homepage;
                TotalPage = Homepage;
                _allPages = new List<Panel>();
                while (Controls.Count > 0)
                {
                    Control c = Controls[0];
                    Controls.Remove(c);
                    if (isSearch)
                        c.Dispose();
                }
                if (_dataSource == null || _dataSource.Count <= 0) return;
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
                _columnsCount = (Width + InSpaceLeftAndRight - OutSpace.Left) /
                                (_dataSource[0].Width + InSpaceLeftAndRight);
            }
            else
            {
                _columnsCount = (Width - OutSpace.Left - OutSpace.Right) / _dataSource[0].Width;
                if (_columnsCount > 0)
                {
                    InSpaceLeftAndRight = (Width - OutSpace.Left - OutSpace.Right) % _dataSource[0].Width /
                                          _columnsCount;
                }
            }
            if (InSpaceTopAndBottom > 0)
            {

                _rowsCount = (Height + InSpaceTopAndBottom - OutSpace.Top) /
                             (_dataSource[0].Height + InSpaceTopAndBottom);
            }
            else
            {
                _rowsCount = (Height - OutSpace.Top - OutSpace.Bottom) / _dataSource[0].Height;
                if (_rowsCount > 0)
                {
                    InSpaceTopAndBottom = (Height - OutSpace.Top - OutSpace.Bottom) % _dataSource[0].Height /
                                          _rowsCount;
                }
            }
            _columnsCount = _columnsCount == 0 ? 1 : _columnsCount;
            _rowsCount = _rowsCount == 0 ? 1 : _rowsCount;
            _pageControlCount = _columnsCount * _rowsCount;
            TotalPage = _dataSource.Count / _pageControlCount;
            if (_dataSource.Count % _pageControlCount > 0)
                TotalPage++;
            FillUpControls();
            AppointPage(Homepage);
        }

        #region 翻页事件
        /// <summary>
        /// 上一页
        /// </summary>
        public void NextPage()
        {
            if (CurrentPage == Homepage)
                return;
            ShowContolByPage(CurrentPage - 1);
            if (PageTurned != null)
            {
                PageTurned(this, new EventArgs());
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public void PreviousPage()
        {
            if (CurrentPage == TotalPage)
                return;
            ShowContolByPage(CurrentPage + 1);
            if (PageTurned != null)
            {
                PageTurned(this, new EventArgs());
            }
        }
        /// <summary>
        /// 指定页跳转
        /// </summary>
        /// <param name="page"></param>
        public void AppointPage(int page)
        {
            if (page == CurrentPage || page <= 0 || page > TotalPage) return;
            ShowContolByPage(page);
            if (PageTurned != null)
            {
                PageTurned(this, new EventArgs());
            }
        }

        public void Search<T>(Predicate<T> match)
        {
            if (match != null)
            {
                Predicate<Control> controlMatch = match as Predicate<Control>;
                _dataSource.FindAll(controlMatch);
            }
            else
            {

            }
        }
        #endregion
        /// <summary>
        /// 根据页码显示
        /// </summary>
        /// <param name="page"></param>
        private void ShowContolByPage(object page)
        {
            DateTime now = DateTime.Now;
            try
            {
                // ReSharper disable once InconsistentNaming
                var Page = page as int? ?? 0;
                for (int i = 0; i < _allPages.Count; i++)
                {
                    if (_allPages[i].Visible && i != Page - 1)
                        _allPages[i].Visible = false;
                    if (i == Page - 1)
                        _allPages[i].Visible = true;
                }
                CurrentPage = Page;
                Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 将控件加载进PANEL
        /// </summary>
        private void FillUpControls()
        {
            DateTime now = DateTime.Now;
            for (var i = 0; i < TotalPage; i++)
            {
                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.Visible = false;
                for (int j = i * _pageControlCount; j < (i + 1) * _pageControlCount && j < _dataSource.Count; j++)
                {
                    var orderByCount = j % _pageControlCount;
                    _dataSource[j].Location = new Point((orderByCount % _columnsCount) * (_dataSource[j].Width + InSpaceLeftAndRight) + OutSpace.Left, (orderByCount / _columnsCount) * (_dataSource[j].Height + InSpaceTopAndBottom) + OutSpace.Top);
                    panel.Controls.Add(_dataSource[j]);
                }
                Controls.Add(panel);
                _allPages.Add(panel);
            }
            if (_allPages.Count > 0)
                _allPages[0].Visible = true;

            //for (var i = 0; i < _dataSource.Count && i < _pageControlCount; i++)
            //{
            //    for (var j = 0; j < TotalPage; j++)
            //    {
            //        if ((i + j * _pageControlCount) >= _dataSource.Count) continue;
            //        _dataSource[i + j * _pageControlCount].Location = location;
            //    }
            //    _dataSource[i].Visible = true;
            //    Controls.Add(_dataSource[i]);
            //}
            Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
        }
    }
}
