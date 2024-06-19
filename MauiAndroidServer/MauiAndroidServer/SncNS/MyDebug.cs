using System.Diagnostics;

namespace MauiAndroidServer.SncNS;

public class MyDebug
{
    public static void Log(string message)
    {
#if WINDOWS
        Debug.WriteLine("snc : " + message);
#else
        Console.WriteLine("snc : " + message);
#endif

    }
}
