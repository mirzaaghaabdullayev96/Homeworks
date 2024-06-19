using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Weaponry.Database
{
    public static class Data
    {
        public static string pathToActions = "Actions.txt";
        public static string pathToDatabase = "Database.txt";
        public static string relativePath = "Weaponry";
        public static string directoryPath = Path.Combine(Environment.CurrentDirectory, relativePath);
        public static string filePathActions = Path.Combine(directoryPath, pathToActions);
        public static string filePathDatabase = Path.Combine(directoryPath, pathToDatabase);


        
    }
}


