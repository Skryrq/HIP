using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

public class Sample
{

    public static void setstartTime(string[] args)
    {
        Regex hourFormat = new Regex(@"^\d{2}:\d{2}:\d{2}$");
        Console.WriteLine("startTime (HH:mm:ss) =");
        startTime = Console.ReadLine();

        if (hourFormat.IsMatch(startTime))
        {
            int h = 0, m = 0, s = 0;
            int.TryParse(startTime.Substring(0, 2), out h);
            int.TryParse(startTime.Substring(3, 2), out m);
            int.TryParse(startTime.Substring(6, 2), out s);

            if (h < 24 && m < 60 && s < 60 && h >= 0 && m >= 0 && s >= 0)
            { setPeriod(args); }
            else
            { Console.WriteLine("Invalid format (HH:mm:ss)"); setstartTime(args); }
        }
        else
        { Console.WriteLine("Invalid format (HH:mm:ss)"); setstartTime(args); }

    }
    public static void setPeriod(string[] args)
    {
        Console.WriteLine("timer (seconds) =");
        string readTimer = Console.ReadLine();
        period = 0;
        if (int.TryParse(readTimer, out period))
        {
            Sample.shutdown(args);
            //SetTimer.setTimer();
        }
        else
        {
            Console.WriteLine("Must be an integer number"); setPeriod(args);
        }
    }
    public static void shutdown(string[] args)
    {
        Console.WriteLine("Scheduled shutdown : '1' : YES / '2' : NO / 'spacebar' to restart");

        ConsoleKeyInfo readedKey = Console.ReadKey(true);
        switch (readedKey.Key)
        {
            case ConsoleKey.D1:

                Regex hourFormat = new Regex(@"^\d{2}:\d{2}:\d{2}$");
                Console.WriteLine("shutdownTime (HH:mm:ss) =");
                shutdownTime = Console.ReadLine();

                if (hourFormat.IsMatch(shutdownTime))
                {
                    int h = 0, m = 0, s = 0;
                    int.TryParse(shutdownTime.Substring(0, 2), out h);
                    int.TryParse(shutdownTime.Substring(3, 2), out m);
                    int.TryParse(shutdownTime.Substring(6, 2), out s);

                    if (h < 24 && m < 60 && s < 60 && h >= 0 && m >= 0 && s >= 0)
                    {
                        Console.WriteLine("Computer will shutdown at " + shutdownTime);
                        SetTimer.setshutdownTimer();
                        SetTimer.setTimer();
                    }
                    else
                    { Console.WriteLine("Invalid format (HH:mm:ss)"); goto case ConsoleKey.D1; }
                }
                else
                { Console.WriteLine("Invalid format (HH:mm:ss)"); goto case ConsoleKey.D1; }

                break;

            case ConsoleKey.D2:
                SetTimer.setTimer();
                break;

            case ConsoleKey.Spacebar:
                Console.Clear();
                Main(args);
                break;

            default:
                shutdown(args);
                break;
        }
    }

    public static int period;
    public static string startTime;
    public static string shutdownTime;
    public static void Main(string[] args)
    {
        Console.SetWindowSize(170, 50);
        Console.WriteLine("Press : '1' to update / '2' to set timer / 'spacebar' to restart");

        ConsoleKeyInfo readedKey = Console.ReadKey(true);
        switch (readedKey.Key)
        {
            case ConsoleKey.D1:
                Writer.Write();
                { Main(args); }
                break;

            case ConsoleKey.D2:
                setstartTime(args);
                break;

            case ConsoleKey.Spacebar:
                Console.Clear();
                Main(args);
                break;

            default:
                Main(args);
                break;
        }
        while (Console.ReadKey().Key != ConsoleKey.Escape) { }

    }
}
