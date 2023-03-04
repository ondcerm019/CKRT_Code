using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotKeyAppSupportV1.Models
{
    public static class NotificationManager
    {
        static int counter = 0;

        public static void ShowNotification(string title, string content)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(content)
                .Show(toast =>
                {
                    toast.ExpirationTime = DateTime.Now.AddSeconds(5);
                    toast.Group = "HotkeyInformation";
                    toast.Tag = counter.ToString();
                });

            counter++;
        }
    }
}
