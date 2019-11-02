using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Arclight.TableExtractor
{
    internal static class TableExtractor
    {
        #if DEBUG
        private const string Title = "Arclight Table Extractor (DEBUG)";
        #else
        private const string Title = "Arclight Table Extractor (RELEASE)";
        #endif

        private static void Main(string[] args)
        {
            Console.Title = Title;
            
            if (args.Length != 1)
                throw new ArgumentException();

            if (!Directory.Exists(args[0]))
                throw new DirectoryNotFoundException();

            string basePath = $"{args[0]}/base.dll";
            if (!File.Exists(basePath))
                throw new FileNotFoundException();

            Console.WriteLine("Extracting archive passwords...");
            var passwordExtractor = new PasswordExtractor(basePath);

            Console.WriteLine("Extracting tables...");

            string archivePath = $"{args[0]}/datas/data12.v";
            if (!File.Exists(archivePath))
                throw new FileNotFoundException();

            // create output directory for table files
            if (!Directory.Exists("table"))
                Directory.CreateDirectory("table");

            using var xorStream = new ArchiveXorStream(archivePath, FileMode.Open);
            using var zipFile = new ZipFile(xorStream)
            {
                Password = passwordExtractor.GetPassword("data12.v")
            };

            foreach (ZipEntry zipEntry in zipFile)
            {
                if (zipEntry.IsDirectory)
                    continue;

                string fileName = zipEntry.Name.Split('/')[^1];
                Console.WriteLine($"Extracting {fileName}...");

                using Stream zipEntryStream = zipFile.GetInputStream(zipEntry);
                using FileStream tableFileStream = File.Create($"table/{fileName}");
                zipEntryStream.CopyTo(tableFileStream);
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
