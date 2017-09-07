using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectHA
{
    class StartPage : ContentPage
    {
        public StartPage()
        {
            Title = "ProjectH";

            StackLayout stackLayout = new StackLayout();

            Button BIOSButt = new Button
            {
                Text = "BIOS",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            BIOSButt.Clicked += BIOSButtClicked;
            stackLayout.Children.Add(BIOSButt);

            Button MotherboardButt = new Button
            {
                Text = "Материнская плата",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            MotherboardButt.Clicked += MotherboardButtClicked;
            stackLayout.Children.Add(MotherboardButt);

            Button BoardConnButt = new Button
            {
                Text = "Порты платы",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            BoardConnButt.Clicked += BoardConnButtClicked;
            stackLayout.Children.Add(BoardConnButt);

            Button ProcessorButt = new Button
            {
                Text = "Процессор",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            ProcessorButt.Clicked += ProcessorButtClicked;
            stackLayout.Children.Add(ProcessorButt);

            Button USBHubButt = new Button
            {
                Text = "Порты USB",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            USBHubButt.Clicked += USBHubButtClicked;
            stackLayout.Children.Add(USBHubButt);

            Button VideoContrButt = new Button
            {
                Text = "Видеоконтроллер",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            VideoContrButt.Clicked += VideoContrButtClicked;
            stackLayout.Children.Add(VideoContrButt);

            Button LogicalDiskButt = new Button
            {
                Text = "Накопители",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            LogicalDiskButt.Clicked += LogicalDiskButtClicked;
            stackLayout.Children.Add(LogicalDiskButt);

            Button OperatingSysButt = new Button
            {
                Text = "Операционная система",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            OperatingSysButt.Clicked += OperatingSysButtClicked;
            stackLayout.Children.Add(OperatingSysButt);

            Button UserAccButt = new Button
            {
                Text = "Пользователи",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            UserAccButt.Clicked += UserAccButtClicked;
            stackLayout.Children.Add(UserAccButt);

            Button MonitorButt = new Button
            {
                Text = "Монитор",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            MonitorButt.Clicked += MonitorButtClicked;
            stackLayout.Children.Add(MonitorButt);

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;
            this.Content = scrollView;          
        }

        private async void BIOSButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BIOSInfoPage());
        }

        private async void MotherboardButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MotherboardInfoPage());
        }

        private async void BoardConnButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BoardConnPage());
        }

        private async void ProcessorButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ProcessorInfoPage());
        }

        private async void USBHubButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new USBHubPage());
        }

        private async void VideoContrButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new VideoContrPage());
        }

        private async void LogicalDiskButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LogicalDiskPage());
        }

        private async void OperatingSysButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new OperatingSysPage());
        }

        private async void UserAccButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new UserAccPage());
        }

        private async void MonitorButtClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MonitorPage());
        }

    }
}
