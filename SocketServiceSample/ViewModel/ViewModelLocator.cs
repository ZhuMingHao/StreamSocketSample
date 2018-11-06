using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml;

namespace SocketServiceSample.ViewModel
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _default;

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //在Ioc容器里面注册每个VM  
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<MessageViewModel>();
        }

        /// <summary>  
        ///     默认的全局ViewModelLocator实例，在App资源中声明  
        /// </summary>  
        public static ViewModelLocator Default
        {
            get
            {
                if (_default != null) return _default;
                _default = Application.Current.Resources["locator"] as ViewModelLocator;
                if (_default == null) throw new NotImplementedException("App资源中没有声明ViewModelLocator");
                return _default;
            }
        }

        /// <summary>  
        /// 提供给外部的SettingPageViewModel VM  
        /// </summary>  
        public SettingViewModel SettingViewModel => ServiceLocator.Current.GetInstance<SettingViewModel>();

        /// <summary>  
        /// 提供给外部的MessagePageViewModel VM  
        /// </summary>  
        public MessageViewModel MessageViewModel => ServiceLocator.Current.GetInstance<MessageViewModel>();
    }
}
