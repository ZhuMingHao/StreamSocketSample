using SocketClientSample.ViewModel;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SocketClientSample.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ClientSetting : Page
    {
        private readonly SettingPageViewModel _vm = ViewModelLocator.Default.SettingPageViewModel;
        public ClientSetting()
        {
            this.InitializeComponent();
        }
    }
}
