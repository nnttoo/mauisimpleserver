using MauiAndroidServer.SncNS;

namespace MauiAndroidServer
{
    public partial class MainPage : ContentPage
    {

        private MyWebserver myWebServer = new MyWebserver();

        public MainPage()
        {
            InitializeComponent();
            myWebServer.PropertyChanged += MyWebServer_PropertyChanged;
        }

        private void MyWebServer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(myWebServer.ServerRun))
            {
                if (myWebServer.ServerRun)
                {
                    CounterBtn.Dispatcher.Dispatch(() =>
                    {

                        CounterBtn.Background = Color.FromArgb("#FF55ff00");
                        CounterBtn.Text = "Stop Server";
                    });
                }
                else
                {
                    CounterBtn.Dispatcher.Dispatch(() =>
                    {
                        CounterBtn.Background = Color.FromArgb("#FFff0000");
                        CounterBtn.Text = "Start Server";
                    });
                }

                return;
            }

            if (e.PropertyName == nameof(myWebServer.ArgValue))
            {
                editor.Dispatcher.Dispatch(() =>
                {
                    editor.Text = myWebServer.ArgValue;
                });
            }

        }

        private void StartServer(object sender, EventArgs e)
        {
            if (CounterBtn.Text == "Start Server")
            {
                myWebServer.StartServer(entryIP.Text, entryPort.Text);
            }
            else
            {
                myWebServer.StopServer();
            }

        }
    }

}
