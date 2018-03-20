using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Timers;
using System.Web;


class Write
{
    public static int d=1;
    public static string 
        day= "08.",
        month="12.",
        year="2017",
        date = day+month+year,
        pathEC = @"C:\Users\Max\Documents\ECONOMICCALENDAR\EC." + date + ".html",
        pathMT = @"C:\Users\Max\AppData\Roaming\MetaQuotes\Terminal\2010C2441A263399B34F537D91A53AC9\MQL4\Files\",
        //fileTime = @"time." + date + ".txt",
        fileName = @"name." + date + ".txt";

    public static int i = 0;
    public static string[] title = new string[100];
    public static string[] time = new string[100];
    
    public static void Writer()
    {
        {
            //for( d = 4; d<9; d++)
            Title(); Time();
            WriteConsole();
        }
    }
    
    private static void Title()
    {
        HtmlDocument calendar = new HtmlDocument();
        calendar.Load(pathEC);
        HtmlNode[] nodes = calendar.DocumentNode.SelectNodes("//td//a").ToArray();
        System.IO.File.Delete(pathMT + fileName);
        i = 0;
        foreach (HtmlNode item in nodes)
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] bytes = Encoding.Default.GetBytes(item.InnerHtml.Trim());
            item.InnerHtml = Encoding.UTF8.GetString(bytes);
            System.IO.File.AppendAllText(pathMT + fileName, item.InnerHtml.Trim() + Environment.NewLine);
            Console.WriteLine(item.InnerHtml.Trim());
            title[i] = item.InnerHtml.Trim();
            i++;
        }
    }

    private static void Time()
    {
        HtmlDocument calendar = new HtmlDocument();
        calendar.Load(pathEC);
        HtmlNode[] nodes = calendar.DocumentNode.SelectNodes("//td[@class='first left time js-time']").ToArray();
        
        i = 0;
        foreach (HtmlNode item in nodes)
        {
            
            Console.WriteLine(item.InnerHtml);
            time[i] = item.InnerHtml;
            i++;
        }
    }

    private static void WriteConsole()
    {
        Console.WriteLine("\r\n    Currency:\t\tTime:\t\tTrend:\t\tStrength:\tTotal:\r\n");
        for (int c = 0; c < i; c++)
        {
            
            Console.Write("\t"+ time[c]  + "\t\t" + title[c] + "\r\n");

        }
        

        Console.WriteLine();
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Press : '1' to update / '2' to set timer / 'spacebar' to restart");

        ConsoleKeyInfo readedKey = Console.ReadKey(true);
        switch (readedKey.Key)
        {
            case ConsoleKey.D1:
                Write.Writer();
                { Main(args); }
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

