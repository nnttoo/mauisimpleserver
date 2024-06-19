using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace MauiAndroidServer.SncNS;

public partial class MyWebserver : ObservableObject
{
    [ObservableProperty]
    public bool _serverRun = false;

    [ObservableProperty]
    private string _argValue = "";

    HttpListener listener = new HttpListener();

    public NameValueCollection getQueryParam(HttpListenerContext ctx)
    {
        var request = ctx.Request;
        string queryString = request.Url.Query;
        NameValueCollection queryParams = HttpUtility.ParseQueryString(queryString);
        return queryParams;
    }

    public string getBodyString(HttpListenerContext ctx)
    {
        string requestBody = "";
        var request = ctx.Request;
        using (Stream body = request.InputStream)
        {
            using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
            {
                requestBody = reader.ReadToEnd();
            }
        }

        return requestBody;
    }

    private void processParam(HttpListenerContext ctx)
    {
        var qParam = getQueryParam(ctx);
        if (qParam != null)
        {
            for (int i = 0; i < qParam.Count; i++)
            {
                string key = qParam.GetKey(i);
                string value = qParam.Get(i);

                MyDebug.Log($"{key} : {value}");

                ArgValue = value;
            }
        }
    }

    private void StartServerIntetrnal(string ip, string port)
    {

        listener.Prefixes.Add($"http://{ip}:{port}/");
        listener.Start();

        while (ServerRun)
        {
            try
            {
                HttpListenerContext context = listener.GetContext();
                processParam(context);
                var path = context.Request.Url.AbsolutePath;

                string strResponse = "";

                if (path == null)
                {
                    strResponse = "";

                }
                else if (path == "/")
                {



                    strResponse = "" +
                        "<div style='margin-left:auto;margin-right:auto; padding:20px; width: 400px; border : solid 1px #ccc'>" +
                        "<h1>Masukan Nilai yang akan ditampilkan</h1>" +
                        "<form method='GET'>" +
                        "<textarea style='height: 200px; width:100%' name='arg'></textarea>" +
                        "<div><button style='margin-top:10px; padding: 10px' type='submit'>Kirim</button></div>" +
                        "</form>" +
                        "</div>";


                }



                using (Stream output = context.Response.OutputStream)
                {
                    context.Response.AddHeader("Content-Type", "text/html");
                    var buffer = Encoding.UTF8.GetBytes(strResponse);
                    output.Write(buffer, 0, buffer.Length);

                }

            }
            catch { }

        }


    }

    public void StopServer()
    {
        ServerRun = false;

        listener.Stop();
        listener.Close();



    }

    public void StartServer(string ip, string port)
    {
        new Thread(() =>
        {
            listener = new HttpListener();
            ServerRun = true;
            StartServerIntetrnal(ip, port);
        }).Start();
    }
}
