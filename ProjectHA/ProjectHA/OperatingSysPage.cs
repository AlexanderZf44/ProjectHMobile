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
    class OperatingSysPage : ContentPage
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<AllInfo> AllInfo { get; set; }

        public OperatingSysPage()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<AllInfo>();

            this.AllInfo = new ObservableCollection<AllInfo>(database.Table<AllInfo>());

            Title = "Operating System Info";

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
                        strInfo += "Название операционной системы: " + str.KEY + "\r\n";
                        break;
                    case "NumberOfProcesses":
                        strInfo += "Кол-во процессов: " + str.KEY + "\r\n";
                        break;
                    case "NumberOfUsers":
                        strInfo += "Кол-во пользователей: " + str.KEY + "\r\n";
                        break;
                    case "OSArchitecture":
                        strInfo += "Разрядность системы: " + str.KEY + "\r\n";
                        break;
                    case "RegisteredUser":
                        strInfo += "Текущий пользователь: " + str.KEY + "\r\n";
                        break;
                    case "SystemDirectory":
                        strInfo += "Путь системы: " + str.KEY + "\r\n";
                        break;
                    case "Version":
                        strInfo += "Версия системы: " + str.KEY + "\r\n";
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
                  "WHERE MANCLASS = 'Win32_OperatingSystem' " +
                  "AND (NAME = 'Caption' " +
                  "OR NAME = 'NumberOfProcesses' " +
                  "OR NAME = 'NumberOfUsers' " +
                  "OR NAME = 'OSArchitecture' " +
                  "OR NAME = 'RegisteredUser' " +
                  "OR NAME = 'SystemDirectory' " +
                  "OR NAME = 'Version') ").
                  AsEnumerable();
            }
        }
    }
}
