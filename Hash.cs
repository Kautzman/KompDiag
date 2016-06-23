using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace sysinfo
{
  internal class Hash
  {
    private string[] hashTable = new string[100]
    {
      "a",
      "b",
      "c",
      "d",
      "e",
      "f",
      "g",
      "h",
      "i",
      "j",
      "k",
      "l",
      "m",
      "n",
      "o",
      "p",
      "q",
      "r",
      "s",
      "t",
      "u",
      "v",
      "w",
      "x",
      "y",
      "z",
      "A",
      "B",
      "C",
      "D",
      "E",
      "F",
      "G",
      "H",
      "I",
      "J",
      "K",
      "L",
      "M",
      "N",
      "O",
      "p",
      "Q",
      "R",
      "S",
      "T",
      "U",
      "V",
      "W",
      "X",
      "Y",
      "Z",
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "aa",
      "ac",
      "ae",
      "ai",
      "ao",
      "ax",
      "ay",
      "az",
      "ca",
      "cc",
      "cd",
      "ce",
      "cg",
      "ch",
      "ci",
      "cj",
      "cq",
      "cx",
      "da",
      "dc",
      "dd",
      "de",
      "dg",
      "dh",
      "di",
      "dq",
      "dt",
      "dy",
      "ta",
      "tc",
      "td",
      "te",
      "th",
      "tj",
      "tm",
      "tn",
      "tp",
      "tr"
    };
    private string finalhash;

        public string createHash()
        {
            main form = new main();
            try
            {
                string[] strArray = Enumerable.ToArray<string>(Enumerable.Select<char, string>((IEnumerable<char>) string.Format("{0:ddMMyyhhmmssffff}", (object) SimpleNTP.GetNetworkTime()), (Func<char, string>) (c => c.ToString())));
                string[] input = new string[8];

                for (int index = 0; index < 8; ++index)
                {
                    string str = strArray[index] + strArray[15 - index];
                    input[index] = str;
                }

                this.finalhash = this.createHash(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(((object) ex).ToString());
            }

        return this.finalhash;
        }

        private string createHash(string[] input)
        {
            string str1 = "";

            foreach (string str2 in input)
            {
                int index = Convert.ToInt32(str2);
                str1 = str1 + this.hashTable[index];
            }
                return str1;
        }
    }
}
