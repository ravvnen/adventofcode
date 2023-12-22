
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Drawing;

namespace System {
    public class Day2{

        Dictionary<string, (string Pattern, int Minimum)> rx = new Dictionary<string, (string Pattern, int Minimum)>()
            {
                {"red", (@"\d+\s*red", 0)},
                {"blue", (@"\d+\s*blue", 0)},
                {"green",(@"\d+\s*green", 0)}
            };
        public int QueryGameID(string line)
        {
            int colonIndex = line.IndexOf(':');
            string[] games = line.Split(';');

            // get the game number
            int gameID = int.Parse(line.Substring(5, colonIndex - 5));
            bool allGamesLegal = true;

            foreach (var game in games)
            {

                if(!ProcessGame(game))
                {
                    allGamesLegal = false;
                    return 0;
                }
            }

            return gameID;
        }
        public bool ProcessGame(string game)
        {
            Dictionary<string, (string Pattern, int Threshold)> rx = new Dictionary<string, (string Pattern, int Threshold)>()
            {
                {"red", (@"\d+\s*red", 12)},
                {"blue", (@"\d+\s*blue", 14)},
                {"green",(@"\d+\s*green", 13)}
            };

            foreach(var color in rx)
            {
                // we get the matches
                var matches = Regex.Matches(game, color.Value.Pattern);
                
                // iterate through the matches, and get the value for each
                foreach(Match match in matches)
                {
                    // getting the value for each match, fx "4 red", splitting it up ' ', so we get '4' and 'red', and then we take index 0 of that 
                    int value = int.Parse(match.Value.Split(' ')[0]);

                    // checking if the value is legal
                    if(value > color.Value.Threshold)
                    {
                        return false;
                    }

                }
            }
            
            return true;
        }
        
        public int QueryGameID2(string line)
        {
            int result = 1;
            int colonIndex = line.IndexOf(':');
            string[] games = line.Split(';');

            // get the game number
            int gameID = int.Parse(line.Substring(5, colonIndex - 5));

            foreach (var set in games)
            {
                ProcessGame2(set);
            }

            foreach(var color in rx)
            {
                result *= rx[color.Key].Minimum;
            }

            Console.WriteLine(result);
            return result;
        }       
        public void ProcessGame2(string set)
        {

            foreach(var color in rx)
            {
                // we get the matches
                var matches = Regex.Matches(set, color.Value.Pattern);
                
                // iterate through the matches, and get the value for each
                foreach(Match match in matches)
                {
                    // getting the value for each match, fx "4 red", splitting it up ' ', so we get '4' and 'red', and then we take index 0 of that 
                    int value = int.Parse(match.Value.Split(' ')[0]);

                    // checking if the value is legal
                    if(value > rx[color.Key].Minimum)
                    {
                        rx[color.Key] = (rx[color.Key].Pattern, value);
                    }

                }
            }
        }
        
        
        public static void Main() {

            try 
            {
                // line will read the input, it will be ?, since it needs to handle null aswell
                string line;
                int total_sum = 0;
                StreamReader sr = new StreamReader("/Users/phillipravn/adventofcode/Day2/day2input.txt");
                line = sr.ReadLine();
                 // keep reading the line
                while (line != null)
                {
                    var mc = new Day2();
                    total_sum += mc.QueryGameID2(line);
                    line = sr.ReadLine();
                }

                Console.WriteLine("total sum: " + total_sum);
                sr.Close();
            }
            catch(Exception e) 
            {
                Console.WriteLine("Received error message" + e.Message);
            }

    }
    }
}