using System;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace NotionHashRemover
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var path = args[0];

                if (!path.EndsWith(".zip"))
                    path += ".zip";

                RenameZipEntries(path);

                Console.WriteLine("Done");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        internal static void RenameZipEntries(string file)
        {
            using var archive = new ZipArchive(File.Open(file, FileMode.Open, FileAccess.ReadWrite), ZipArchiveMode.Update);
            
            var entries = archive.Entries.ToArray();
            foreach (var entry in entries)
            {
                var newEntry = archive.CreateEntry(RemoveMD5(entry.FullName));
                using (var a = entry.Open())
                using (var b = newEntry.Open())
                    a.CopyTo(b);
                entry.Delete();
            }

            archive.Dispose();
        }

        public static string RemoveMD5(string input)
        {
            foreach (var item in Regex.Matches(input, "[0-9a-fA-F]{32}", RegexOptions.Compiled).ToList())
            {
                if (item.Success)
                {
                    input = input.Replace(item.Value, "").TrimEnd();
                }
            }

            return input;
        }


    }
}