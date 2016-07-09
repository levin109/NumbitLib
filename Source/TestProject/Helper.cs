using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
   internal class Helper
    {
       public static void ArrayDisplay<T>(T[] arr)
       {
           string str = "";
           for (int i = 0; i < arr.Length; i++)
               str += arr[i] + "\t";
           Console.WriteLine(str);
       }
    }
}
