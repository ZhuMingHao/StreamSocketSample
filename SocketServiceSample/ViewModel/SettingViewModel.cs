using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Models;
using SocketBusiness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace SocketServiceSample.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public UserModel UserModel { get; set; } = new UserModel { UserName = "服务器" };

        /// <summary>  
        ///     Socket服务端  
        /// </summary>  
        public SocketBase ServerSocket { get; set; }

        /// <summary>  
        ///     监听状态文本描述  
        /// </summary>  
        public string ListeningStateTxt { get; set; }

        /// <summary>  
        ///     监听TCP链接的端口号  
        /// </summary>  
        public string LocalServiceName { get; set; }

        /// <summary>  
        ///     本地IP  
        /// </summary>  
        public string LocalHostName { get; set; }

        /// <summary>  
        ///     是否已开启 Socket 服务  
        /// </summary>  
        public bool SocketState { get; set; }

        /// <summary>  
        /// 消息集合  
        /// </summary>  
        public ObservableCollection<MessageModel> MessageCollection { get; set; } =
            new ObservableCollection<MessageModel>();

        public SettingViewModel()
        {
            LocalHostName = GetLocalIp();
            LocalServiceName = "9900";

            //创建服务端Socket  
            //（方法名忘记改了 就这样吧 CreatInkSocket 是创建Ink墨迹的服务端，前段时间做的Ink墨迹同步。大家如果看着不爽就自行改吧）  
            ServerSocket = SocketFactory.CreatInkSocket(true, LocalHostName, LocalServiceName);
            //新消息到达通知  
            ServerSocket.MsgReceivedAction += data =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => { MessageCollection.Add(data); });
                Messenger.Default.Send(data, "NewMsgAction");
            };
        }

        /// <summary>  
        /// 获取本地ip地址  
        /// </summary>  
        /// <returns>ip</returns>  
        private string GetLocalIp()
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();

            if (icp?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .SingleOrDefault(
                        hn =>
                            hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == icp.NetworkAdapter.NetworkAdapterId);

            // the ip address  
            return hostname?.CanonicalName;
        }
    }
}
