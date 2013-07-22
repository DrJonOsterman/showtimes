using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CsQuery;

namespace request
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest reqObj = WebRequest.Create("C:\\Users\\Edd\\Desktop\\LOOKUPONMYWORKS\\moviesnearme\\movDump.htm");
            Stream streamObj = reqObj.GetResponse().GetResponseStream();
            string data = (new StreamReader(streamObj)).ReadToEnd();

            CQ lol = CQ.Create(data);

            var what = lol["#movie_results .movie_results .theater"];

            Console.Write(what[0].TextContent);


            Console.ReadLine();
        }
    }
}
