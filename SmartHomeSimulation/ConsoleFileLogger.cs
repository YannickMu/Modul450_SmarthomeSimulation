using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulation
{

    public class ConsoleFileLogger : ILogger
    {
        private readonly string _filePath;

        public ConsoleFileLogger(string filePath = null)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

}
