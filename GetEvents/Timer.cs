using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Timers;

public class SetTimer
{
    private static Timer shutdownTimer;
    private static Timer timer;
    private static int s = 1;
    private static int c = 1;
    //private static string date = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
    //private string now = DateTime.Now.ToString("HH:mm:ss");

    public static void setTimer()
    {
        Console.WriteLine("Started at {0:HH:mm:ss}", DateTime.Now);
        var writer = new Writer();
        writer.Write(); 
        setTimer2();
    }

    private static void setTimer2()
    {
        timer = new Timer(1000 * s);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
    }
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        if (s == 1)
        {
            c--;
            if (c == 0)
            {
                c = 300;
                Console.WriteLine("{0:HH:mm:ss}: waiting for startTime (" + Sample.startTime + ") ", e.SignalTime);
            }
        }
        string date = DateTime.UtcNow.Date.ToString("dd/MM/yyyy"),
               now = DateTime.Now.ToString("HH:mm:ss");

        if (Sample.startTime == now)
        {
            s = Sample.period;
            timer.Stop();
            timer.Interval = (1000 * s);
            timer.Start();
            Console.WriteLine("Timer set (update every " + Sample.period + " sec.)", e.SignalTime);
        }
        if (s != 1)
        {
            var writer = new Writer();
            writer.Write();
            {
                string now2 = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine("Requested at {0:HH:mm:ss} - updated at " + now2, e.SignalTime);
            }
        }
    }
    public static void setshutdownTimer()
    {
        shutdownTimer = new Timer(500);
        shutdownTimer.Elapsed += CheckShutdown;
        shutdownTimer.AutoReset = true;
        shutdownTimer.Enabled = true;

    }
    private static void CheckShutdown(Object source, ElapsedEventArgs e)
    {
        string date = DateTime.UtcNow.Date.ToString("dd/MM/yyyy"),
               now = DateTime.Now.ToString("HH:mm:ss");
        if (Sample.shutdownTime == now)
        {
            System.Diagnostics.Process.Start("ShutDown", "-s -t 300");
        }
    }
}





