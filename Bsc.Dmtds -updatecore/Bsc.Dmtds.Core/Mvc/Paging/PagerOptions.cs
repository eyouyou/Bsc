using System;

namespace Bsc.Dmtds.Core.Mvc.Paging
{
    public class PagerOptions
    {
        public PagerOptions()
        {
            AutoHide = true;
            PageIndexParameterName = "page";
            NumericPagerItemCount = 10;
            AlwaysShowFirstLastPageNumber = false;
            ShowPrevNext = true;
            PrevPageText = "<";
            NextPageText = ">";
            ShowNumericPagerItems = true;
            ShowFirstLast = false;
            FirstPageText = "第一页";
            LastPageText = "最后一页";
            ShowMorePagerItems = true;
            MorePageText = "...";
            ShowDisabledPagerItems = true;
            SeparatorHtml = "&nbsp;&nbsp;";
            UseJqueryAjax = false;
            ContainerTagName = "div";
            ShowPageIndexBox = false;
            ShowGoButton = true;
            PageIndexBoxType = PageIndexBoxType.TextBox;
            MaximumPageIndexItems = 80;
            GoButtonText = "去吧皮卡丘";
            ContainerTagName = "div";
            InvalidPageIndexErrorMessage = "无效的页面索引";
            PageIndexOutOfRangeErrorMessage = "页面索引超出范围";
            CurrentPagerItemWrapperFormatString = "<span class=\"active\">{0}</span>";
            StatisticsTextFormatString = "<span><strong>{0}</strong>-<strong>{1}</strong> of <strong>{2}</strong></span>";
        }

        /// <summary>
        /// Gets or sets the statistics text format string.<example> FROM {0} TO {1} OF {2}</example>
        /// </summary>
        /// <value>
        /// The statistics text format string.
        /// </value>
        public string StatisticsTextFormatString { get; set; }
        /// <summary>
        /// 只有一页是否隐藏
        /// </summary>
        public bool AutoHide { get; set; }

        /// <summary>
        /// 页面索引超出
        /// </summary>
        public string PageIndexOutOfRangeErrorMessage { get; set; }

        /// <summary>
        /// 无效页面索引
        /// </summary>
        public string InvalidPageIndexErrorMessage { get; set; }
        /// <summary>
        /// url?后面key
        /// </summary>
        public string PageIndexParameterName { get; set; }

        /// <summary>
        /// 是否显示索引box
        /// </summary>
        public bool ShowPageIndexBox { get; set; }

        /// <summary>
        /// 索引box类型
        /// </summary>
        public PageIndexBoxType PageIndexBoxType { get; set; }

        /// <summary>
        /// 下拉菜单最多显示数
        /// </summary>
        public int MaximumPageIndexItems { get; set; }

        /// <summary>
        /// 是否显示去吧皮卡丘
        /// </summary>
        public bool ShowGoButton { get; set; }

        /// <summary>
        /// 去吧皮卡丘按钮上面的字
        /// </summary>
        public string GoButtonText { get; set; }

        /// <summary>
        /// numeric pager item format string
        /// </summary>
        public string PageNumberFormatString { get; set; }

        private string _containerTagName;
        /// <summary>
        /// html 标签名
        /// </summary>
        public string ContainerTagName
        {
            get
            {
                return _containerTagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("标签名不能为空", "ContainerTagName");
                _containerTagName = value;
            }
        }

        /// <summary>
        /// all pageritem wrapper format string
        /// </summary>
        public string PagerItemWrapperFormatString { get; set; }

        /// <summary>
        /// current pager item format string
        /// </summary>
        public string CurrentPageNumberFormatString { get; set; }

        /// <summary>
        /// NumericPager Item Wrapper Format String
        /// </summary>
        public string NumericPagerItemWrapperFormatString { get; set; }

        /// <summary>
        /// Current Pager Item Wrapper Format String
        /// </summary>
        public string CurrentPagerItemWrapperFormatString { get; set; }

        /// <summary>
        /// NavigationPager Item Wrapper Format String
        /// </summary>
        public string NavigationPagerItemWrapperFormatString { get; set; }

        /// <summary>
        /// MorePagerItem Wrapper Format String
        /// </summary>
        public string MorePagerItemWrapperFormatString { get; set; }

        /// <summary>
        /// PageIndexBox Wrapper Format String
        /// </summary>
        public string PageIndexBoxWrapperFormatString { get; set; }

        /// <summary>
        /// GoToPage Section Wrapper Format String
        /// </summary>
        public string GoToPageSectionWrapperFormatString { get; set; }

        /// <summary>
        /// 是否显示第一页和最后一页的数字
        /// </summary>
        public bool AlwaysShowFirstLastPageNumber { get; set; }
        /// <summary>
        /// numeric pager items count
        /// </summary>
        public int NumericPagerItemCount { get; set; }
        /// <summary>
        /// 是否显示上一页下一页按钮
        /// </summary>
        public bool ShowPrevNext { get; set; }
        /// <summary>
        /// 上一页按钮上的字
        /// </summary>
        public string PrevPageText { get; set; }
        /// <summary>
        /// 下一页按钮上的字
        /// </summary>
        public string NextPageText { get; set; }
        /// <summary>
        /// whether or not show numeric pager items
        /// </summary>
        public bool ShowNumericPagerItems { get; set; }
        /// <summary>
        /// 是否显示第一个和最后一页
        /// </summary>
        public bool ShowFirstLast { get; set; }
        /// <summary>
        /// 第一页按钮上的字
        /// </summary>
        public string FirstPageText { get; set; }
        /// <summary>
        /// 最后一页按钮上的字
        /// </summary>
        public string LastPageText { get; set; }
        /// <summary>
        /// 是否显示更多页
        /// </summary>
        public bool ShowMorePagerItems { get; set; }
        /// <summary>
        /// 更多页按钮上的字
        /// </summary>
        public string MorePageText { get; set; }
        /// <summary>
        /// 客户端id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 水平线向，水平校准;
        /// </summary>
        public string HorizontalAlign { get; set; }
        /// <summary>
        /// CSS class 应用
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        /// whether or not show disabled pager items
        /// </summary>
        public bool ShowDisabledPagerItems { get; set; }
        /// <summary>
        /// seperating item html
        /// </summary>
        public string SeparatorHtml { get; set; }

        /// <summary>
        /// 是否使用 jQuery ajax, 与 MicrosoftAjax相反
        /// </summary>
        internal bool UseJqueryAjax { get; set; }
         
    }
    public enum PageIndexBoxType
    {
        TextBox,//输入框
        DropDownList //下拉菜单
    }
}