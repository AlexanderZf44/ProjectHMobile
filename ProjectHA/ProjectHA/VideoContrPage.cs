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
    class VideoContrPage : ContentPage
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<AllInfo> AllInfo { get; set; }

        public VideoContrPage()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<AllInfo>();

            this.AllInfo = new ObservableCollection<AllInfo>(database.Table<AllInfo>());

            Title = "Video Controller Info";

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
                    case "AdapterDACType":
                        strInfo += "Тип адаптера ЦАП видеоконтроллера: " + str.KEY + "\r\n";
                        break;
                    case "Caption":
                        strInfo += "Имя видеоконтроллера: " + str.KEY + "\r\n";
                        break;
                    case "CurrentHorizontalResolution":
                        strInfo += "Текущее горизонтальное разрешение: " + str.KEY + "\r\n";
                        break;
                    case "CurrentVerticalResolution":
                        strInfo += "Текущее вертикальное разрешение: " + str.KEY + "\r\n";
                        break;
                    case "CurrentRefreshRate":
                        strInfo += "Текущая частота обновления кадров: " + str.KEY + "\r\n";
                        break;
                    case "DriverVersion":
                        strInfo += "Версия установленного драйвера: " + str.KEY + "\r\n";
                        break;
                    case "InstalledDisplayDrivers":
                        strInfo += "Пути установленных драйверов: " + str.KEY + "\r\n";
                        break;
                    case "Status":
                        strInfo += "Статус видеоконтроллера: " + str.KEY + "\r\n";
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
            ScrollView scrollView = new ScrollView();
            scrollView.Content = frame;
            this.Content = scrollView;
        }

        public IEnumerable<AllInfo> GetFilteredAllInfo()
        {
            lock (collisionLock)
            {
                return database.Query<AllInfo>(
                  "SELECT NAME, KEY " +
                  "FROM ALLINFO " +
                  "WHERE MANCLASS = 'Win32_VideoController' " +
                  "AND (NAME = 'AdapterDACType' " +
                  "OR NAME = 'Caption' " +
                  "OR NAME = 'CurrentHorizontalResolution' " +
                  "OR NAME = 'CurrentVerticalResolution' " +
                  "OR NAME = 'CurrentRefreshRate' " +
                  "OR NAME = 'DriverVersion' " +
                  "OR NAME = 'InstalledDisplayDrivers' " +
                  "OR NAME = 'Status') ").
                  AsEnumerable();
            }
        }
    }
}
