using System.IO;
using SevenZip;

namespace Common
{
    public static class Archive
    {
        private static readonly string SEVENZIPLIBPATH = @".\SevenZipLib\7z.dll";

        public static void Compress(string inPath, string filemask, string archivePath)
        {
            if (!File.Exists(SEVENZIPLIBPATH))
            {
                throw new FileNotFoundException($"Unable to locate '{SEVENZIPLIBPATH}'");
            }

            SevenZipBase.SetLibraryPath(SEVENZIPLIBPATH);

            var ext = Path.GetExtension(archivePath);
            var files = Directory.GetFiles(inPath, filemask);

            // Get the files and add each one to the archive
            var cmp = new SevenZipCompressor
            {
                CompressionMode = CompressionMode.Create,
                TempFolderPath = Path.GetTempPath(),
                CompressionMethod = CompressionMethod.Lzma,
                CompressionLevel = CompressionLevel.Fast,
                DirectoryStructure = false
            };
            switch (ext)
            {
                case "7z":
                    cmp.ArchiveFormat = OutArchiveFormat.SevenZip;
                    break;
                case "zip":
                    cmp.ArchiveFormat = OutArchiveFormat.Zip;
                    break;
                case "tar":
                    cmp.ArchiveFormat = OutArchiveFormat.Tar;
                    break;
                default:
                    cmp.ArchiveFormat = OutArchiveFormat.SevenZip;
                    break;
            }

            cmp.CompressFiles(archivePath, files);
        }

        public static void Extract(string archivePath, string destPath)
        {
            if (!File.Exists(SEVENZIPLIBPATH))
            {
                throw new FileNotFoundException($"Unable to locate '{SEVENZIPLIBPATH}'");
            }

            SevenZipBase.SetLibraryPath(SEVENZIPLIBPATH);

            // Extract the files from the archive
            using (var ext = new SevenZipExtractor(archivePath))
            {
                var files = ext.ArchiveFileNames;
                foreach (var file in files)
                {
                    var filepath = $@"{destPath}\{file}";
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                }
                ext.ExtractArchive(destPath);
            }
        }
    }
}
