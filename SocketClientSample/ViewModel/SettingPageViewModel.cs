using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Models;
using SocketBusiness;

namespace SocketClientSample.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        private string _socketStateTxt = "未连接";
        /// <summary>  
        ///     监听状态文本描述  
        /// </summary>  
        public string SocketStateTxt
        {
            get
            {
                return _socketStateTxt;
            }
            set
            {
                _socketStateTxt = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>  
        ///     Socket服务端  
        /// </summary>  
        public SocketBase ClientSocket { get; set; }

        /// <summary>  
        /// 用户信息  
        /// </summary>  
        public UserModel UserModel { get; set; } = new UserModel();

        /// <summary>  
        /// 远程服务ip  
        /// </summary>  
        public string ServicerIp { get; set; }

        /// <summary>  
        /// 端口号  
        /// </summary>  
        public string ServicerPort { get; set; }

        /// <summary>  
        /// 连接按钮点击事件  
        /// </summary>  
        public async void ConnectionToServicer()
        {
            if (string.IsNullOrEmpty(UserModel.UserName) || string.IsNullOrEmpty(ServicerIp) ||
                string.IsNullOrEmpty(ServicerPort))
                return;
            //创建一个客户端Socket对象  
            ClientSocket = SocketFactory.CreatInkSocket(false, ServicerIp, ServicerPort);

            //当新消息到达时的行为  
            ClientSocket.MsgReceivedAction += data => { Messenger.Default.Send(data, "MsgReceivedAction"); };

            //连接成功时的行为  
            ClientSocket.OnStartSuccess += () =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => SocketStateTxt = "已连接");
            };

            //连接失败时的行为  
            ClientSocket.OnStartFailed += exc =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => SocketStateTxt = $"断开的连接：{exc.Message}");
            };

            //开始连接远程服务端  
            await ClientSocket.Start();
        }
    }
}
