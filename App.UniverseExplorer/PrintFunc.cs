using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using static System.Console;

namespace App.UniverseExplorer
{
    public static class PrintFunc
    {
        public static void EnumerableResult<T>(
            Func<IEnumerable<T>> db,
            string msg,
            Func<T, string> loopBody)
        {
            WriteLine(msg);
            foreach (var entry in db.Invoke())
                {
                    WriteLine(loopBody.Invoke(entry));
                }
            WriteLine("Press enter to continue");
            ReadLine();
        }
        
        public static void DigitResult(
            Func<double> db,
            string msg,
            Func<double, string> loopBody)
        {
            WriteLine(msg);
            WriteLine(loopBody.Invoke(db.Invoke()));
            WriteLine("Press enter to continue");
            ReadLine();
        } 
        
        public static void DtoResult<T>(
            Func<T> db,
            string msg,
            Func<T, string> loopBody)
        {
            WriteLine(msg);
            WriteLine(loopBody.Invoke(db.Invoke()));
            WriteLine("Press enter to continue");
            ReadLine();
        }
    }
}