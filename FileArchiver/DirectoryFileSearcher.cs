using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileArchiver
{
    class DirectoryFileSearcher : IFileSearcher
    {
        public string RootDirectory { get; }
        public string  SearchPattern { get; }
        public SearchOption DirSearchOption { get; }
        public long IgnoreFilesGreaterThanBytes { get; }
        public DateTime LastModGreaterThan { get; }

        public DirectoryFileSearcher(string rootDirectory, string seachPattern, SearchOption searchOption, long ignoreFilesGreaterThanBytes = -1, DateTime lastModGreaterThan = default(DateTime))
        {
            RootDirectory = rootDirectory;
            SearchPattern = seachPattern;
            DirSearchOption = searchOption;

            if (lastModGreaterThan == default(DateTime))
            {
                LastModGreaterThan = DateTime.MinValue;
            }
            else
            {
                LastModGreaterThan = lastModGreaterThan;
            }

            IgnoreFilesGreaterThanBytes = ignoreFilesGreaterThanBytes;
                        
        }
        public IReadOnlyList<DetailedFileInfo> FindFiles()
        {
            Console.WriteLine("Compiling File List...");

            List<DetailedFileInfo> list = new List<DetailedFileInfo>();
            // only filer on the modified date.  Never return anything before the last mod date
            return Directory.GetFiles(RootDirectory, SearchPattern, DirSearchOption)
                                .Select(fi => new FileInfo(fi))
                                .Where(fi =>fi.LastWriteTime > LastModGreaterThan)
                                .Select(fi => FileMatchCriteria(fi))
                                .ToList<DetailedFileInfo>();

        }

        private DetailedFileInfo FileMatchCriteria(FileInfo fileInfo)
        {
            if (IgnoreFilesGreaterThanBytes >= 0 && fileInfo.Length >= IgnoreFilesGreaterThanBytes)
            {
                return new DetailedFileInfo(fileInfo, false);
            }
            return new DetailedFileInfo(fileInfo, true);
        }

        public DateTime ReturnMinLastModifiedTime()
        {
            return LastModGreaterThan;
        }
    }
}
