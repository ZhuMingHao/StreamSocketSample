using Models;
using SocketServiceSample.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SocketServiceSample.Toolkit
{
    public class MsgStyleSelector : DataTemplateSelector
    {
        public DataTemplate OtherMsgStyle { get; set; }

        public DataTemplate MyMsgStyle { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var msg = item as MessageModel;
            if (msg == null) return OtherMsgStyle;
            return msg.User == ViewModelLocator.Default.SettingViewModel.UserModel ? MyMsgStyle : OtherMsgStyle;
        }
    }
}
