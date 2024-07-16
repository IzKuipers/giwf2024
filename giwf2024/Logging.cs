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
            DateTime now = DateTime.Now;

            string message = "[" + now + "] " + source + ": " + text;

            store.Add(message);
            Console.WriteLine(message);
        }
    }
}
