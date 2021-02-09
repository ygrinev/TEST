using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RenameFilesInAllSubs
{
    class Program
    {
        static void RenameFilesInFolder(string folder, string from, string to)
        {
            var parent = new DirectoryInfo(folder);
            foreach (FileInfo f in parent.GetFiles())
            {
                if(f.FullName.Length - f.FullName.LastIndexOf(from) <= 5)
                    File.Move(f.FullName, f.FullName.Replace(from, to)); // .j -> ,tj
            }

            foreach (var child in parent.GetDirectories())
            {
                RenameFilesInFolder(child.FullName, from, to);
            }
        }
        static void Main(string[] args)
        {
            string from = "", parent = "", to = "";
#if DEBUG
            parent = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "rename");
            from = ".j";
            to = ".tj";
#else
            if(args.Length < 3)
            {
                parent = args[0];
                from = args[1];
                to = args[2];
            }
            else
            {
                parent = new DirectoryInfo(Console.ReadLine());
                Console.WriteLine("Replace from:");
                from = Console.ReadLine();
                Console.WriteLine("Replace to:");
                to = Console.ReadLine();
            }
#endif
            RenameFilesInFolder(parent, from, to);
        }
    }
}
