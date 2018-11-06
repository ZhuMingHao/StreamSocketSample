using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MessageModel
    {
        /// <summary>  
        /// 消息类型  
        /// </summary>  
        public MessageType MessageType { get; set; }

        /// <summary>  
        /// 消息数据  
        /// </summary>  
        public object Message { get; set; }

        /// <summary>  
        /// 发送者  
        /// </summary>  
        public UserModel User { get; set; }

        /// <summary>  
        /// 发送时间  
        /// </summary>  
        public DateTime SetDateTime { get; set; }

        /// <summary>  
        /// 辅助属性，用来决定消息显示的时候是靠左还是靠右  
        /// 靠左是别人发来的消息  靠右是自己发出去的消息  
        /// </summary>  
        public Windows.UI.Xaml.HorizontalAlignment Horizontal { get; set; } = Windows.UI.Xaml.HorizontalAlignment.Left;

    }

    public enum MessageType
    {
        //文字消息  
        TextMessage,
        //系统消息之断开连接  
        Disconnect
    }
}
