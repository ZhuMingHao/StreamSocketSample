using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SocketServiceSample.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        private string _txtMsg;

        /// <summary>  
        ///     要发送的文本  
        /// </summary>  
        public string TxtMsg
        {
            get { return _txtMsg; }
            set
            {
                _txtMsg = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>  
        /// 消息集合  
        /// </summary>  
        public ObservableCollection<MessageModel> MessageCollection { get; } =
             ViewModelLocator.Default.SettingViewModel.MessageCollection;


        /// <summary>  
        /// 发送消息  
        /// </summary>  
        /// <returns></returns>  
        public async Task SendTxtMsg()
        {
            if (string.IsNullOrEmpty(TxtMsg)) return;
            var msg = new MessageModel
            {
                MessageType = MessageType.TextMessage,
                Message = TxtMsg,
                SetDateTime = DateTime.Now,
                User = ViewModelLocator.Default.SettingViewModel.UserModel
            };
            var socket = ViewModelLocator.Default.SettingViewModel.ServerSocket;
            await socket.SendMsg(msg);

            msg.Horizontal = Windows.UI.Xaml.HorizontalAlignment.Right;
            DispatcherHelper.CheckBeginInvokeOnUI(() => { MessageCollection.Add(msg); });
            Messenger.Default.Send(msg, "NewMsgAction");
            TxtMsg = null;
        }


        public async void MsgTextBoxKeyUp(object sender, KeyRoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null) TxtMsg = textBox.Text;
            if (e.Key != Windows.System.VirtualKey.Enter) return;
            if (string.IsNullOrEmpty(TxtMsg))
                return;
            await SendTxtMsg();
        }
    }
}
