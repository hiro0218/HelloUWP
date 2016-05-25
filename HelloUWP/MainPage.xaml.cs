using System;
using Windows.System.Profile;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HelloUWP.Services;
using System.Xml;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace HelloUWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string DeviceFamily = "Device Family: " + AnalyticsInfo.VersionInfo.DeviceFamily;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog("Dialog Message", "Dialog Title");
            dlg.Commands.Add(new UICommand("はい", null, true));
            dlg.Commands.Add(new UICommand("いいえ", null, false));
            var selectedCommand = await dlg.ShowAsync();
            bool result = (bool)selectedCommand.Id;

            // 「はい」が選択された場合の動作
            if (result)
            {
                // トーストを表示させる
                templateNotify();
            } else
            {
                string text = "Cancel";
                string2Notify(text);
            }
        }

        private void templateNotify()
        {
            var xmlDoc = ToastService.CreateToast();
            var notifier = ToastNotificationManager.CreateToastNotifier();
            var toast = new ToastNotification(xmlDoc);

            notifier.Show(toast);
        }

        public static void string2Notify(String message)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var notifier = ToastNotificationManager.CreateToastNotifier();

            // Set Text
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(message));

            ToastNotification toast = new ToastNotification(toastXml);

            notifier.Show(toast);
        }
    }
}
