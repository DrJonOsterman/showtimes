using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CsQuery;
using System.Text.RegularExpressions;

namespace request
{
    public class Theater
    {
        public static List<Theater> theaterList = new List<Theater>();
        private Theater() {}
        public Theater(string pName){
            this.name = pName;
            this.movies = new List<Movie>();
        }
        public string name;
        public List<Movie> movies;
    }

    public class Movie
    {
        public Movie(){
            this.showTimes = new List<string>();
        }
        public Movie(string pName){
            this.name = pName;
            this.showTimes = new List<string>();
        }
        public string name;
        public List<string> showTimes;
    }

    class Program
    {
        static void printMovies(IDomObject pTheater, string side, ref Theater tempTheaterObj)
        {
            CQ movies = CQ.Create(pTheater)[".showtimes .show_" + side + " .movie"];
            for (int i = 0; i < movies.Length; i++)
            {
                Movie tempMovObj = new Movie(CQ.Create(movies)[".name"][i].TextContent);
                var showTimes = CQ.Create(movies[i])[".times span"];
                for (var j = 0; j < showTimes.Length; j++)
                {
                    Match match = Regex.Match(showTimes[j].TextContent, @"(\d{1,2}:\d\d[ap]?)");
                    if (match.Success)
                    {
                        tempMovObj.showTimes.Add(match.Groups[1].Value);
                    }
                }
                tempTheaterObj.movies.Add(tempMovObj);
            }
        }

        static void Main(string[] args)
        {        
            WebRequest reqObj = WebRequest.Create("movDump.htm");
            Stream streamObj = reqObj.GetResponse().GetResponseStream();
            string data = (new StreamReader(streamObj)).ReadToEnd();
            CQ lol = CQ.Create(data);
            var theaters = lol["#movie_results .movie_results .theater"];
            
            foreach (var theater in theaters)
            {
                Theater tempTheaterObj = new Theater(CQ.Create(theater)[".desc .name a"].Text());
                Program.printMovies(theater, "left", ref tempTheaterObj);
                Program.printMovies(theater, "right", ref tempTheaterObj);
                Theater.theaterList.Add(tempTheaterObj);
            }
           
            for (int i = 0 ; i < Theater.theaterList.Count ; i++)
            {
                Console.WriteLine(Theater.theaterList[i].name.ToUpper());
                for (int j = 0 ; j < Theater.theaterList[i].movies.Count ; j++)
                {
                    Console.Write(" s " + Theater.theaterList[i].movies[j].name + "\n    [");
                    for (int k = 0; k < Theater.theaterList[i].movies[j].showTimes.Count; k++)
                    {
                        Console.Write(Theater.theaterList[i].movies[j].showTimes[k] + ((k == Theater.theaterList[i].movies[j].showTimes.Count - 1) ? "" : ", "));
                    }
                    Console.Write("]\n");
                }
                Console.WriteLine("\n");
            }
            Console.ReadLine();
        }
    }
}
