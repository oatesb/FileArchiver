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
        public IEnumerable<DetailedFileInfo> FindFiles()
        {
            Console.WriteLine("Compiling File List...");

            List<DetailedFileInfo> list = new List<DetailedFileInfo>();
            // only filer on the modified date.  Never return anything before the last mod date
            var allFileList = Directory.GetFiles(RootDirectory, SearchPattern, DirSearchOption)
                                .Where(s => new FileInfo(s).LastWriteTime > LastModGreaterThan)
                                .Select(s => Path.GetFullPath(s));

            Console.WriteLine("Marking the Matching Files...");
            foreach (var item in allFileList)
            {
                 list.Add(FileMatchCriteria(new FileInfo(item)));
            }
            
            Console.WriteLine("Returning File List");

            return list.ToArray();
        }

        private DetailedFileInfo FileMatchCriteria(FileInfo fileInfo)
        {
            if (IgnoreFilesGreaterThanBytes < 0 || fileInfo.Length < IgnoreFilesGreaterThanBytes)
            {
                return new DetailedFileInfo(fileInfo, true);
            }
            else
            {
                return new DetailedFileInfo(fileInfo, false);
            }
        }

        public DateTime ReturnMinLastModifiedTime()
        {
            return LastModGreaterThan;
        }
    }
}
