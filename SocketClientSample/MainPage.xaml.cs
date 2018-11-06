using Models;
using SocketClientSample.Controls;
using SocketClientSample.Pages;
using System.Collections.Generic;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace SocketClientSample
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Frame MainFrame { get; set; }
        public List<NavModel> NavList = new List<NavModel>
    {
        new NavModel {Icon = Symbol.Message,PageType = typeof(ClientMessage),Title = "消息"},
        new NavModel {Icon = Symbol.Setting,PageType = typeof(ClientSetting),Title = "设置"}
    };
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame = MainPageFrame;
        }

        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item is NavModel)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavModel)args.Item).Title);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

        private void NavMenuList_ItemInvoked(object sender, ListViewItem e)
        {
            var item = (NavModel)((NavMenuListView)sender).ItemFromContainer(e);

            if (item?.PageType != null && item.PageType != typeof(object) &&
                item.PageType != MainFrame.CurrentSourcePageType)
            {
                MainFrame.Navigate(item.PageType);
            }
        }
    }
}
