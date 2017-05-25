using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T004
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = System.Environment.CurrentDirectory;
            Console.WriteLine(str);

            //分析命令
            bool showhelp = true;
            if (args.Length > 0)
            {
                String infile = args[0];
                String outfile = args.Length >= 2 ? args[1] : args[0];
                if (!(infile.Equals("-h") || infile.Equals("-help") || infile.Equals("-?")))
                {
                    infile = CheckFilePath(infile);
                    outfile = CheckFilePath(outfile);
                    StartConvert(infile, outfile);
                    showhelp = false;
                }
            }

            //未得到正确参数则显示帮助
            if (showhelp)
            {
                ShowHelp();
            }

            //Console.WriteLine("按任意键继续...");
            //Console.ReadKey();
        }

        static void ShowHelp()
        {
            Console.WriteLine("用法:");
            Console.WriteLine("  UTF8Convert <in> [out]");
            Console.WriteLine("<in> 指定要转换的文件，可以为绝对路径或则相对exe所在目录的路径");
            Console.WriteLine("[out] 可选参数，指定转换保存路径，若不输入则覆盖转换的文件");
        }

        static String CheckFilePath(String infile)
        {
            //相对路径转换成绝对路径
            if (!infile.Contains(":"))
            {
                string str = System.Environment.CurrentDirectory;
                infile = str + "\\" + infile;
            }
            return infile;
        }

        static void StartConvert(String infile, String outfile)
        {
            Console.WriteLine("开始转换 {0} -> {1}", infile, outfile);
            try
            {
                Encoding encoding = GetFileEncodeType(infile);
                FileStream fread = new FileStream(infile, FileMode.Open);
                StreamReader sr = new StreamReader(fread, Encoding.GetEncoding("gb2312"));
                String filestring = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                sr = null;
                fread.Close();
                fread.Dispose();
                fread = null;

                UTF8Encoding utf8 = new UTF8Encoding(true);
                FileStream fwrite = new FileStream(outfile, FileMode.Create);
                StreamWriter sw = new StreamWriter(fwrite, utf8);
                sw.Write(filestring);
                sw.Flush();
                sw = null;
                fwrite.Close();
                fwrite.Dispose();
                fwrite = null;
            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
            }
        }


        static System.Text.Encoding GetFileEncodeType(string filename) 
        { 
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read); 
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs); 
            Byte[] buffer = br.ReadBytes(2);
            br.Close();
            br.Dispose();
            br = null;

            if(buffer[0]>=0xEF) 
            { 
                if(buffer[0]==0xEF && buffer[1]==0xBB) 
                { 
                     return System.Text.Encoding.UTF8; 
                } 
                else if(buffer[0]==0xFE && buffer[1]==0xFF) 
                { 
                     return System.Text.Encoding.BigEndianUnicode; 
                } 
                else if(buffer[0]==0xFF && buffer[1]==0xFE) 
                { 
                     return System.Text.Encoding.Unicode; 
                } 
            }
            return System.Text.Encoding.Default; 
        }
    }
}
