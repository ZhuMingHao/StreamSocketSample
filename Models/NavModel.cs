using System;

namespace Models
{
    public class NavModel
    {
        /// <summary>  
        /// 要显示的文字  
        /// </summary>  
        public string Title { get; set; }

        /// <summary>  
        /// 图标  
        /// </summary>  
        public Windows.UI.Xaml.Controls.Symbol Icon { get; set; }

        /// <summary>  
        /// 要导航到的页面  
        /// </summary>  
        public Type PageType { get; set; }
    }
}
