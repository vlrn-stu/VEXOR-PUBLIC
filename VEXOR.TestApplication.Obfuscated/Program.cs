internal class Program
{
#pragma warning disable IDE0051 // Remove unused private members

    private readonly string a0 = DecryptString("U2lnbWFHcmluZHNldA==");
#pragma warning restore IDE0051 // Remove unused private members

    private static void Main()
    {
        StartAntiDebugThread();
        a5();
        a6();
        Console.WriteLine(DecryptString("RG9uZS4="));
        Console.ReadKey();
    }

    private static void a5()
    {
        Console.Write(DecryptString("RW50ZXIgYSBudW1iZXI6IA=="));
        string? a1 = Console.ReadLine();
        Console.WriteLine(DecryptString("WW91IGVudGVyZWQ6IA==") + a1);
    }

    private static void a6()
    {
        int a2 = 42;
        int a3 = a7(a2);
        Console.WriteLine(DecryptString("UmVzdWx0IGlzOiA=") + a3);
    }

    private static int a7(int a4)
    {
        if (!(a4 > 10))
        {
            return a4 + 10;
        }
        else
        {
            return a4 * 2;
        }
    }

    private static string DecryptString(string input)
    {
        var bytes = System.Convert.FromBase64String(input);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    private static void StartAntiDebugThread()
    {
        static void AntiDebug()
        {
            while (true)
            {
                if (System.Diagnostics.Debugger.IsAttached || System.Diagnostics.Debugger.IsLogging())
                {
                    System.Environment.Exit(1);
                }

                System.Threading.Thread.Sleep(100);
            }
        }

        Thread antiDebugThread = new Thread(new ThreadStart(AntiDebug));
        antiDebugThread.IsBackground = true;
        antiDebugThread.Start();
    }
}