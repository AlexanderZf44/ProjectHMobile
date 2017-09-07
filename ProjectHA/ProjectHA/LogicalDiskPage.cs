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
    class LogicalDiskPage : ContentPage
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<AllInfo> AllInfo { get; set; }

        public LogicalDiskPage()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<AllInfo>();

            this.AllInfo = new ObservableCollection<AllInfo>(database.Table<AllInfo>());

            Title = "Logical Disks Info";

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
                        strInfo += "Имя накопителя: " + str.KEY + "\r\n";
                        break;
                    case "Description":
                        strInfo += "Описание накопителя: " + str.KEY + "\r\n";
                        break;
                    case "FileSystem":
                        if (str.KEY != null)
                        {
                            strInfo += "Файловая система накопителя: " + str.KEY + "\r\n";
                        }
                        else
                        {
                            strInfo += "Файловая система накопителя: Накопитель не имеет файловой системы" + "\r\n";
                        }
                        break;
                    case "FreeSpace":
                        if (str.KEY != null)
                        {
                            strInfo += "Свободное пространство накопителя: " + str.KEY + "\r\n";
                        }
                        else
                        {
                            strInfo += "Свободное пространство накопителя: Диск не вставлен" + "\r\n";
                        }
                        break;
                    case "Size":
                        if (str.KEY != null)
                        {
                            strInfo += "Размер накопителя: " + str.KEY + "\r\n";
                        }
                        else
                        {
                            strInfo += "Размер накопителя: Диск не вставлен" + "\r\n";
                        }
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
                  "WHERE MANCLASS = 'Win32_LogicalDisk' " +
                  "AND (NAME = 'Caption' " +
                  "OR NAME = 'Description' " +
                  "OR NAME = 'FileSystem' " +
                  "OR NAME = 'FreeSpace' " +
                  "OR NAME = 'Size') ").
                  AsEnumerable();
            }
        }
    }
}
