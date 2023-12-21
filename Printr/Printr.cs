using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public class Printr {

        // Static dictionary to hold the mappings from string to integer value
        public int findNumbers(string line)
        {
            var first = '0';
            var last = '0';

            for(var i = 0; i < line.Length && first == '0'; i++)
            {
                first = parseLine(line.Substring(i));
            }

            for(var i = line.Length-1; i >= 0 && last == '0'; i--)
            {
                last = parseLine(line.Substring(i));
            }
            return int.Parse(new string(new [] { first, last }));
        }
        
        public char parseLine(string line)
        {
            
            var nameNumbers = new Dictionary<string, char>
            {
                {"one", '1'},{"two", '2'}, {"three", '3'},{"four", '4'}, {"five", '5'}, {"six", '6'},{"seven", '7'},{"eight", '8'},{"nine", '9'}
            };
            
            foreach (var item in nameNumbers.Keys)
            {
                if(line.StartsWith(item)) 
                {
                    return nameNumbers[item];
                }
                if(char.IsNumber(line[0]))
                {
                    return line[0];
                }
            }      

            return '0';
        }
        
        public static void Main() {

            try 
            {
                // line will read the input, it will be ?, since it needs to handle null aswell
                string line;
                int sum = 0;
                StreamReader sr = new StreamReader("/Users/phillipravn/adventofcode/Printr/day1input.txt");
                line = sr.ReadLine();
                while(line != null) 
                {
                    // keep reading the line
                    var mc = new Printr();
                    sum += mc.findNumbers(line);
                    line = sr.ReadLine();

                }
                sr.Close();
                Console.WriteLine(sum);
            }
            catch(Exception e) 
            {
                Console.WriteLine("Received error message" + e.Message);
            }

    }
}
}
