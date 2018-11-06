using GalaSoft.MvvmLight.Messaging;
using Models;
using SocketClientSample.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SocketClientSample.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ClientMessage : Page
    {
        private MessagePageViewModel _vm = ViewModelLocator.Default.MessagePageViewModel;
        public ClientMessage()
        {
            this.InitializeComponent();
        }
        private async void SendedMsgAction(MessageModel obj)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // 新消息到达后 滚动到新消息项  
                MsgListView.ScrollIntoView(obj);
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //注册新消息到达的 Messeng  
            Messenger.Default.Register<MessageModel>(this, "NewMsgAction", SendedMsgAction);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            //取消注册的消息  
            Messenger.Default.Unregister(this);
        }
    }
}
