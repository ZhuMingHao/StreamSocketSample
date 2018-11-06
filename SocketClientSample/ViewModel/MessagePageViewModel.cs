using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SocketClientSample.ViewModel
{
    public class MessagePageViewModel : ViewModelBase
    {
        /// <summary>  
        /// 本地聊天消息结合  
        /// </summary>  
        public ObservableCollection<MessageModel> MessageCollection { get; set; } =
            new ObservableCollection<MessageModel>();

        private string _textMsg;
        /// <summary>  
        /// 要发送的文本  
        /// </summary>  
        public string TextMsg
        {
            get { return _textMsg; }
            set
            {
                _textMsg = value;
                RaisePropertyChanged();
            }
        }

        public MessagePageViewModel()
        {
            //注册 MsgReceivedAction 的 Message   
            Messenger.Default.Register<MessageModel>(this, "MsgReceivedAction", MsgReceivedAction);
        }

        /// <summary>  
        /// 发送聊天消息  
        /// </summary>  
        /// <returns></returns>  
        public async Task SendMsg()
        {
            var client = ViewModelLocator.Default.SettingPageViewModel.ClientSocket;

            if (!client.Working) return;
            //要发送的消息对象  
            var msg = new MessageModel
            {
                MessageType = MessageType.TextMessage,
                Message = TextMsg,
                SetDateTime = DateTime.Now,
                User = ViewModelLocator.Default.SettingPageViewModel.UserModel
            };
            await client.SendMsg(msg);

            //发送完成后往本地的消息集合MessageCollection 添加一条刚发送的消息  
            msg.Horizontal = HorizontalAlignment.Right;
            MessageCollection.Add(msg);
            //发出 NewMsgAction Message  
            Messenger.Default.Send(msg, "NewMsgAction");
            TextMsg = null;
        }

        /// <summary>  
        /// MsgReceivedAction Message 的具体逻辑  
        /// </summary>  
        /// <param name="obj">接受到的消息数据</param>  
        private void MsgReceivedAction(MessageModel obj)
        {
            //访问UI线程添加新聊天消息到本地聊天记录  
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (obj.MessageType == MessageType.Disconnect)
                    ViewModelLocator.Default.SettingPageViewModel.ClientSocket.Dispose();
                else
                    MessageCollection.Add(obj);
            });
            //发出 NewMsgAction Message  
            Messenger.Default.Send(obj, "NewMsgAction");
        }

        /// <summary>  
        /// 输入框按键抬起事件  
        /// </summary>  
        /// <param name="sender">触发者</param>  
        /// <param name="e">按键数据</param>  
        public async void MsgTextBoxKeyUp(object sender, KeyRoutedEventArgs e)
        {
            TextMsg = (sender as TextBox).Text;
            if (e.Key == Windows.System.VirtualKey.Enter)    //如果按下Enter键 就发送聊天消息  
            {
                if (string.IsNullOrEmpty(TextMsg))
                    return;
                await SendMsg();
            }
        }
    }
}
