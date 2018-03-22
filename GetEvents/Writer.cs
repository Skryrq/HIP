using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Timers;

public class Writer
{

    private readonly string date, pathEC, pathMT, fileTime, fileStrength, fileName, fileTrend;
    public Writer()
    {
        var date = DateTime.Now.ToString("dd/MM/yyyy");
        this.date = date;
        this.pathEC = @"C:\Users\Max\Documents\ECONOMICCALENDAR\EC." + date + ".html";
        this.pathMT = @"C:\Users\Max\AppData\Roaming\MetaQuotes\Terminal\2010C2441A263399B34F537D91A53AC9\MQL4\Files\";
        this.fileTime = @"time." + date + ".txt";
        this.fileStrength = @"strength." + date + ".txt";
        this.fileTrend = @"trend." + date + ".txt";
        this.fileName = @"name." + date + ".txt";
    }
    private static string URL = "https://fr.investing.com/currencies/eur-usd";

    public static int i = 0;
    public static string[] name = new string[100];
    public static string[] currency = new string[100];
    public static string[] time = new string[100];
    public static string[] trend = new string[100];
    public static string[] strength = new string[100];
    public static int?[] total = new int?[100];
    public static int? dailytotal = 0;
        
    private void HTMLwriter()
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load(URL);
        string ec1 = @"	
<link rel=""stylesheet"" href=""https://i-invdn-com.akamaized.net/css/video-js_v3.css""type=""text/css"">
<link rel=""stylesheet"" href=""https://i-invdn-com.akamaized.net/css/mainOldMin_v3b.css""type=""text/css"" >
<link rel=""stylesheet"" href=""https://i-invdn-com.akamaized.net/css/newMainCssMin_v36n.css""type=""text/css"" >
          

<link href=""https://i-invdn-com.akamaized.net/css/printContent_v7i.css""media=""print""rel=""stylesheet""type=""text/css""/>
                 

                 </div><span class=""dateHP float_lang_base_2 arial_11 lightgrayFont bold"" style=""padding - top:5px; "">Heure: 20/04/2017 20:08 (GMT +2:00)</span><h2><a href="" / economic - calendar / ""> Calendrier économique</a></h2>
       
<table class=""genTbl closedTbl ecoCalTbl"">
	  <thead>
	    
	  </thead>
	  <tbody>";
        string ec2 = @"</tbody>
	</table>
	</div>
	<div class=""clear"">&nbsp;</div>";

        System.IO.File.Delete(@"C:\Users\Max\Documents\ECONOMICCALENDAR\EC." + date + ".txt");
        System.IO.File.Delete(pathEC);

        System.IO.File.AppendAllText(pathEC, (ec1) + Environment.NewLine);

        int count = 0;
        HtmlNode[] nodes4 = document.DocumentNode.SelectNodes(".//div[@id='economicCalendarHPMain']//tr").ToArray();
        foreach (HtmlNode item2 in nodes4)
        {
            if (item2.InnerHtml.Contains("theDay"))
                count++;
            if (count <= 1)
            {
                System.IO.File.AppendAllText(pathEC, ("<tr>" + item2.InnerHtml + "</tr>") + Environment.NewLine);
            }
        }
        System.IO.File.AppendAllText(pathEC, (ec2) + Environment.NewLine);
    }
    private void Title()
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
            //Console.WriteLine(item.InnerHtml.Trim());
            name[i] = item.InnerHtml.Trim();
            i++;
        }
    }
    private void Currency()
    {
        HtmlDocument calendar = new HtmlDocument();
        calendar.Load(pathEC);
        HtmlNode[] nodes = calendar.DocumentNode.SelectNodes("//td[@class='left flagCur noWrap']/text()[last()]").ToArray();
        i = 0;
        foreach (HtmlNode item in nodes)
        {
            //Console.WriteLine(item.InnerHtml);
            currency[i] = item.InnerHtml.Trim();
            i++;
        }

    }
    private void Time()
    {
        HtmlDocument calendar = new HtmlDocument();
        calendar.Load(pathEC);
        HtmlNode[] nodes = calendar.DocumentNode.SelectNodes("//td[@class='first left time js-time']").ToArray();
        System.IO.File.Delete(pathMT + fileTime);
        i = 0;
        foreach (HtmlNode item in nodes)
        {
            System.IO.File.AppendAllText(pathMT + fileTime, item.InnerHtml + Environment.NewLine);
            //Console.WriteLine(item.InnerHtml);
            time[i] = item.InnerHtml;
            i++;
        }
    }
    private void Trend()
    {
        HtmlDocument calendar = new HtmlDocument();
        calendar.Load(pathEC);
        HtmlNode[] nodes2 = calendar.DocumentNode.SelectNodes("//tr//td").ToArray();
        System.IO.File.Delete(pathMT + fileTrend);
        i = 0;
        foreach (HtmlNode item in nodes2)
        {
            string str = item.Attributes["class"].Value;

            if (str.Contains("actual"))
            {
                if (str.Contains("blackFont"))
                {
                    //Console.WriteLine("0");
                    System.IO.File.AppendAllText(pathMT + fileTrend, "0" + Environment.NewLine);
                    trend[i] = "0";
                    i++;
                }
                if (str.Contains("redFont"))
                {
                    if (currency[i] == "EUR")
                    {//Console.WriteLine("-1");
                        System.IO.File.AppendAllText(pathMT + fileTrend, "-1" + Environment.NewLine);
                        trend[i] = "-1";
                    }
                    if (currency[i] == "USD")
                    {//Console.WriteLine("-1");
                        System.IO.File.AppendAllText(pathMT + fileTrend, "1" + Environment.NewLine);
                        trend[i] = "1";
                    }
                    i++;
                }
                if (str.Contains("greenFont"))
                {
                    if (currency[i] == "EUR")
                    {//Console.WriteLine("1");
                        System.IO.File.AppendAllText(pathMT + fileTrend, "1" + Environment.NewLine);
                        trend[i] = "1";
                    }
                    if (currency[i] == "USD")
                    {//Console.WriteLine("1");
                        System.IO.File.AppendAllText(pathMT + fileTrend, "-1" + Environment.NewLine);
                        trend[i] = "-1";
                    }
                    i++;
                }
            }

        }
    }
    private void Strength()
    {
        HtmlDocument calendar = new HtmlDocument();
        calendar.Load(pathEC);
        HtmlNode[] nodes3 = calendar.DocumentNode.SelectNodes("//td[@class='left textNum sentiment noWrap']").ToArray();
        System.IO.File.Delete(pathMT + fileStrength);
        i = 0;
        foreach (HtmlNode item in nodes3)
        {
            string str = item.Attributes["data-img_key"].Value;
            if (str.Contains("bull1"))
            {
                //Console.WriteLine("1");
                System.IO.File.AppendAllText(pathMT + fileStrength, "1" + Environment.NewLine);
                strength[i] = "1";
                i++;
            }
            if (str.Contains("bull2"))
            {
                //Console.WriteLine("2");
                System.IO.File.AppendAllText(pathMT + fileStrength, "2" + Environment.NewLine);
                strength[i] = "2";
                i++;
            }
            if (str.Contains("bull3"))
            {
                //Console.WriteLine("3");
                System.IO.File.AppendAllText(pathMT + fileStrength, "3" + Environment.NewLine);
                strength[i] = "3";
                i++;
            }
        }
    }
    private void Total()
    {
        for (int c = 0; c < i; c++)
        {
            int s = 0, t = 0;
            int.TryParse(strength[c], out s);
            int.TryParse(trend[c], out t);
            total[c] = s * t;
            if (total[c] == 0) total[c] = null;
        }
    }

    private void DailyTotal()
    {
        dailytotal = total.Sum();
    }

    private void WriteConsole()
    {
        Console.WriteLine("\r\n    Currency:\t\tTime:\t\tTrend:\t\tStrength:\tTotal:\t\tName:\r\n");
        for (int c = 0; c < i; c++)
        {
            //Console.WriteLine(dailytotal);
            Console.Write("\t" + currency[c] + "\t\t" + time[c] + "\t\t" + trend[c] + "\t\t" + strength[c] + "\t\t" + total[c] + "\t\t" + name[c] + "\r\n");

        }
        if (dailytotal > 0)
            Console.ForegroundColor = ConsoleColor.Green;
        if (dailytotal < 0)
            Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Daily total=" + dailytotal + "\r\n");
        Console.ResetColor();

        Console.WriteLine();
    }

    public void Write()
    {
    this.HTMLwriter();
        {
            this.Title(); this.Currency(); this.Time(); this.Trend(); this.Strength(); this.Total(); this.DailyTotal();
            this.WriteConsole();
        }
    }
}    

