using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReviewAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var lessThanFifteenWords = 0;
            var delim = " ,.";

            var metricNames = new string[] { "Mover", "Shaker", "Questions", "Spam" };
            var listOfWordsToCount = new string[]{ "mover", "shaker", "?", "http" }; //you can add more words over time
            var countOfWords = new int[listOfWordsToCount.Length];

            var allLines = new List<string>();

            //string[] folderToCheckForFileNames = Directory.GetFiles(".\\docs\\", "*.txt");
            //var tasks = new Task[8];
            //for (int i = 0; i < tasks.Length; i++)
            //{
            //    int index = i;
            //    tasks[i] = Task.Run(() =>
            //    {
            //        for (int current = index; current < folderToCheckForFileNames.Length; current += 8)
            //        {
            //            var lines = System.IO.File.ReadAllLines(folderToCheckForFileNames[current]);
            //            allLines.AddRange(lines);
            //        }
            //    });
            //}
            //Task.WaitAll(tasks);

            foreach (var file in Directory.EnumerateFiles(".\\docs\\", "*.txt"))
            {
                var lines = System.IO.File.ReadAllLines(file);
                allLines.AddRange(lines);
            }
            
            foreach (var line in allLines)
            {
                var fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length < 15)
                {
                    lessThanFifteenWords++;
                }
            }

            for (var i = 0; i < listOfWordsToCount.Length; i++)
            {
                countOfWords[i] = allLines.Count(x => x.Contains(listOfWordsToCount[i]));
            }

            Console.WriteLine("lessThanFifteenWords: " + lessThanFifteenWords);
            
            for (var i = 0; i < listOfWordsToCount.Length; i++)
            {
                Console.WriteLine(metricNames[i] + " (" + listOfWordsToCount[i] + ") :  " + countOfWords[i]);
            }
            
        }

        public List<FileResult> ProcessFile(string fileName)
        {
            return new List<FileResult>();
        }
    }

    public class FileResult
    {
        public string MetricName { get; set; }
        public string MetricValue { get; set; }
        public int MetricCount { get; set; }
    }
}
