using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ShoeShopManagement
{
    class CustomMessageBox
    {
        private static CustomMessageBox instance;

        public static CustomMessageBox Instance
        {
            get { if (instance == null) instance = new CustomMessageBox(); return CustomMessageBox.instance; }
            private set { CustomMessageBox.instance = value; }
        }
        private CustomMessageBox()
        {

        }
        public void Success(string message)
        {
            Notifier notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
            notifier.ShowSuccess(message);
        }
    } 
}
