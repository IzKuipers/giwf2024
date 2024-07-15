using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace giwf2024
{
    public static class Logging
    {
        public static List<string> store = new List<string> { };

        public static void Log(string source, string text) {
            store.Add(text);

            DateTime now = DateTime.Now;

            Console.WriteLine("[" + now + "] " + source + ": " + text);
        }
    }
}
