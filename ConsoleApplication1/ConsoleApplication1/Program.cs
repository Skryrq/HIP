using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Try
{
  
    }
    class Program
    {
        static void Main()
        {
        
        string URL = "https://fr.investing.com/currencies/eur-usd";
        string date = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
        string hour = DateTime.Now.ToString("HH.mm");
        string path = @"C:\Users\Max\Documents\ECONOMICCALENDAR\time." + date + ".txt";

        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load(URL);


        HtmlNode calNode = document.DocumentNode.SelectSingleNode("//div[@id='economicCalendarHPMain']");
        System.IO.File.WriteAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\EC." + date + ".html", calNode.InnerHtml);
        System.IO.File.WriteAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\EC." + date + ".txt", calNode.InnerHtml);
        //Console.WriteLine(calNode.InnerHtml); 

        HtmlNode[] nodes = document.DocumentNode.SelectNodes("//div[@id='economicCalendarHPMain']//td[@class='first left time js-time']").ToArray();        
        foreach (HtmlNode item in nodes)
        {                   
            System.IO.File.AppendAllText(path, item.InnerHtml + Environment.NewLine);
            Console.WriteLine(item.InnerHtml);
        }
                                    
        HtmlNode[] nodes2 = document.DocumentNode.SelectNodes("//div[@id='economicCalendarHPMain']//tr//td").ToArray();
        foreach (HtmlNode item in nodes2)
        {
            string str = item.Attributes["class"].Value;

            if (str.Contains("actual"))
            { 
                if (str.Contains("blackFont"))
                   {Console.WriteLine("0");
                    System.IO.File.AppendAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\trend." + date + ".txt", "0" + Environment.NewLine );
                   }
                if (str.Contains("redFont"))
                {   Console.WriteLine("-1");
                    System.IO.File.AppendAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\trend." + date + ".txt", "-1" + Environment.NewLine);
                }
                if (str.Contains("greenFont"))
                {   Console.WriteLine("1");
                    System.IO.File.AppendAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\trend." + date + ".txt", "1" + Environment.NewLine);
                }
            }
                              
        }

        HtmlNode[] nodes3 = document.DocumentNode.SelectNodes("//div[@id='economicCalendarHPMain']//td[@class='left textNum sentiment noWrap']").ToArray();
            foreach (HtmlNode item in nodes3)
            {
                System.IO.File.AppendAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\strength." + date + ".txt", item.Attributes["data-img_key"].Value + Environment.NewLine);
                Console.WriteLine(item.Attributes["data-img_key"].Value);
            }




//td[@class='theDay']
        HtmlNode[] nodes5 = document.DocumentNode.SelectNodes("//div[@id='economicCalendarHPMain']").ToArray();
        foreach (HtmlNode item in nodes5)
        {
            System.IO.File.AppendAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\ONE." + date + ".txt", item.InnerHtml + Environment.NewLine);
            Console.WriteLine(item.InnerHtml);
        }


        int count=0;
            HtmlNode[] nodes4 = document.DocumentNode.SelectNodes(".//div[@id='economicCalendarHPMain']//tr").ToArray();
            foreach (HtmlNode item2 in nodes4)
            {
                if (item2.InnerHtml.Contains("theDay"))
                    count++;
                if (count <= 1)
                {
                    Console.WriteLine(count);
                    System.IO.File.AppendAllText(@"C:\Users\Max\Documents\ECONOMICCALENDAR\tr." + date + ".txt", item2.InnerHtml + Environment.NewLine);
                    Console.WriteLine(item2.InnerHtml);
                
            }
              
            
            
            
        }

        Console.WriteLine(document);
        Console.ReadKey();

        }
}

