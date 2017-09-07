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
    class ProcessorInfoPage : ContentPage
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<AllInfo> AllInfo { get; set; }

        public ProcessorInfoPage()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<AllInfo>();

            this.AllInfo = new ObservableCollection<AllInfo>(database.Table<AllInfo>());

            Title = "Processor Info";

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
                        strInfo += "Серия процессора: " + str.KEY + "\r\n";
                        break;
                    case "Name":
                        strInfo += "Имя процессора: " + str.KEY + "\r\n";
                        break;
                    case "ProcessorId":
                        strInfo += "ID процессора: " + str.KEY + "\r\n";
                        break;
                    case "SocketDesignation":
                        strInfo += "Сокет процессора: " + str.KEY + "\r\n";
                        break;
                    case "AddressWidth":
                        strInfo += "Разрядность процессора: " + str.KEY + "\r\n";
                        break;
                    case "NumberOfCores":
                        strInfo += "Количество ядер процессора: " + str.KEY + "\r\n";
                        break;
                    case "NumberOfLogicalProcessors":
                        strInfo += "Количество логических ядер процессора: " + str.KEY + "\r\n";
                        break;
                    case "Status":
                        strInfo += "Статус процессора: " + str.KEY + "\r\n";
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
                  "WHERE MANCLASS = 'Win32_Processor' " +
                  "AND (NAME = 'Caption' " +
                  "OR NAME = 'Name' " +
                  "OR NAME = 'ProcessorId' " +
                  "OR NAME = 'SocketDesignation' " +
                  "OR NAME = 'AddressWidth' " +
                  "OR NAME = 'NumberOfCores' " +
                  "OR NAME = 'NumberOfLogicalProcessors' " +
                  "OR NAME = 'Status') ").
                  AsEnumerable();
            }
        }
    }
}
