using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_gr
{
    class JobFile
    {
        public void Again(bool again) {
            StreamWriter writer = new StreamWriter("again.txt", false);
            if(again)
                writer.WriteLine("1");
            else
                writer.WriteLine("0");
            writer.Close();
        }
        public bool AgainAbout() 
        {
            if (File.Exists("again.txt")) 
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader("again.txt");
                    string again = sr.ReadLine();
                    sr.Close();
                    if (again == "1")
                        return true;
                    else
                        return false;
                }
                catch 
                {
                    sr.Close();
                    return true;
                    
                }
             }
            else
                return true;
        }
    }
}
