using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SQI.Dx
{
    class Program
    {        
        static void Main(string[] args)
        {
            string fileName = "input.txt";
            IEnumerable<string> idsFromFile;
            IEnumerable<string> verifiedInput;

            Console.WriteLine("Welcome to SQI.Dx\n\n");

            idsFromFile = loadSampleIdsFromFile(fileName);

            verifiedInput = verifySampleInputIds(idsFromFile);

            PrintVerifiedInput(verifiedInput);
            
        }

        private static void PrintVerifiedInput(IEnumerable<string> verifiedInput)
        {
            Console.WriteLine("\nSampleId SampleGroup");

            foreach (string line in verifiedInput)
            {
                var parts = line.Split(' ');
                
                Console.WriteLine("{0} {1}", parts[0], parts[1]);
            }
        }

        public static IEnumerable<string> loadSampleIdsFromFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadLines(filePath);
        }

        public static List<string> verifySampleInputIds(IEnumerable<string> idsFromFile)
        {
            List<string> verifiedIds = new List<string>();

            Console.WriteLine("SampleId SampleGroup Color");

            foreach (string line in idsFromFile)
            {
                var parts = line.Split(' ');

                if(parts.Length == 3)
                {
                    Console.WriteLine("{0} {1} {2}", parts[1], parts[2], getSampleColor(parts[2]));

                    if (isValidSampleId(parts[1]))
                    {
                        verifiedIds.Add( parts[1] + " " + parts[2]);
                    }
                }            
            }

            return verifiedIds;
        }

        private static bool isValidSampleId(string idToMatch)
        {
            IList<ExpectedIDs> db = new List<ExpectedIDs>()
            {
                new ExpectedIDs(){ UniqueID = "1", Protocol = "ABC", sampleID = "S10D1245", sampleGroup = "Standard"},
                new ExpectedIDs(){ UniqueID = "2", Protocol = "ABC", sampleID = "S20D1245", sampleGroup = "Standard"},
                new ExpectedIDs(){ UniqueID = "3", Protocol = "ABC", sampleID = "S30D1245", sampleGroup = "Standard"},
                new ExpectedIDs(){ UniqueID = "4", Protocol = "ABD", sampleID = "C1POS01", sampleGroup = "Control"},
                new ExpectedIDs(){ UniqueID = "5", Protocol = "ABD", sampleID = "C2POS01", sampleGroup = "Control"},
                new ExpectedIDs(){ UniqueID = "6", Protocol = "ABD", sampleID = "C3POS01", sampleGroup = "Control"},
                new ExpectedIDs(){ UniqueID = "7", Protocol = "COR", sampleID = "C2POS02", sampleGroup = "Control"},
                new ExpectedIDs(){ UniqueID = "8", Protocol = "COV", sampleID = "C1POS01", sampleGroup = "Control"},
            };

            foreach (var v in db)
            {
                return v.sampleID == idToMatch;
            }

            return false;
        }

        public class ExpectedIDs
        {
            public string UniqueID; public string Protocol; public string sampleID; public string sampleGroup;

            public string print() { return string.Format("{0} | {1} | {2} | {3} ", UniqueID, Protocol, sampleID, sampleGroup); }
        }

        public static string getSampleColor(string sampleGroup)
        {
            Dictionary<string, string> expectedColor = new Dictionary<string, string> {
                {"Standard","Green"},
                {"Control", "Blue"},
                {"Sample", "White"},
            };

            return expectedColor[sampleGroup];
        }       
    }    
}
