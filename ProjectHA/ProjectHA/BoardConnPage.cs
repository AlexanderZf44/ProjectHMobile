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
    class BoardConnPage : ContentPage
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<AllInfo> AllInfo { get; set; }

        public BoardConnPage()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<AllInfo>();

            this.AllInfo = new ObservableCollection<AllInfo>(database.Table<AllInfo>());

            Title = "Board Connectors Info";

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
                    case "InternalReferenceDesignator":
                        strInfo += "Внутренний указатель разъема: " + str.KEY + "\r\n";
                        break;
                    case "Tag":
                        strInfo += "Тэг разъема платы: " + str.KEY + "\r\n";
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
                  "WHERE MANCLASS = 'Win32_PortConnector' " +
                  "AND (NAME = 'InternalReferenceDesignator' " +
                  "OR NAME = 'Tag') ").
                  AsEnumerable();
            }
        }
    }
}
