using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectHA
{
    class MonitorPage : ContentPage
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<AllInfo> AllInfo { get; set; }

        public MonitorPage()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<AllInfo>();

            this.AllInfo = new ObservableCollection<AllInfo>(database.Table<AllInfo>());

            Title = "Monitor Info";

            Frame frame = new Frame
            {
                OutlineColor = Color.Accent
            };

            var table = GetFilteredAllInfo();

            string strInfo = "";
            foreach (var str in table)
            {
                switch (str.NAME)
                {
                    case "Caption":
                        strInfo += "Подпись подключенного монитора: " + str.KEY + "\r\n";
                        break;
                    case "PNPDeviceID":
                        strInfo += "ID монитора: " + str.KEY + "\r\n";
                        break;
                    case "Status":
                        strInfo += "Статус монитора: " + str.KEY + "\r\n";
                        break;
                    default:
                        break;
                }
            }

            frame.Content = new Label
            {
                Text = strInfo,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            Padding = new Thickness(15);
            Content = frame;
        }

        public IEnumerable<AllInfo> GetFilteredAllInfo()
        {
            lock (collisionLock)
            {
                return database.Query<AllInfo>(
                  "SELECT NAME, KEY " +
                  "FROM ALLINFO " +
                  "WHERE MANCLASS = 'Win32_DesktopMonitor' " +
                  "AND (NAME = 'Caption' " +
                  "OR NAME = 'PNPDeviceID' " +
                  "OR NAME = 'Status') ").
                  AsEnumerable();
            }
        }
    }
}
