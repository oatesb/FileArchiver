using System;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver
{
    class ArchiveManager
    {
        public IFileSearcher FileSearcher { get; set; }

        private IEnumerable<DetailedFileInfo> _masterFileList;
        private List<ArchiveFileTask> _archiveFileTasks = new List<ArchiveFileTask>();

        public ArchiveManager(IFileSearcher fileSearcher)
        {
            FileSearcher = fileSearcher;
            _masterFileList = null;
        }

        public void SearchFiles()
        {
            _masterFileList = FileSearcher.FindFiles();
        }

        public DateTime GetMinDateForMatchingFiles()
        {
            return FileSearcher.ReturnMinLastModifiedTime();
        }

        public IEnumerable<DetailedFileInfo> GetAllFilesFromSearch()
        {
            if (_masterFileList != null)
            {

                return _masterFileList;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DetailedFileInfo> GetFilesThatMatchedSearch()
        {
            if (_masterFileList != null)
            {

                return _masterFileList.Where(f => f.MatchedSearch == true);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DetailedFileInfo> GetFilesThatFailedToMatchedSearch()
        {
            if (_masterFileList != null)
            {

                return _masterFileList.Where(f => f.MatchedSearch == false);
            }
            else
            {
                return null;
            }
        }

        public List<ArchiveFileTask> MakeJobList()
        {
            var groups = _masterFileList.GroupBy(f => f.HashCode.Hash)
                .Select(group => new
                {
                    Metric = group.Key,
                    Details = group,
                    Count = group.Count()
                })
                .OrderByDescending(o => o.Count);

            foreach (var item in groups)
            {
                Console.WriteLine($"{item.Metric}: {item.Count}: {item.Details.First().TheFile.Name}");
            }

            

            foreach(var item in groups)
            {
                ArchiveFileTask mainTask = new ArchiveFileTask(item.Details.First());
                if (item.Details.Count() > 1)
                {
                    foreach (var subitem in item.Details.Skip(1))
                    {
                        ArchiveFileTask t = new ArchiveFileTask(subitem, mainTask);
                        _archiveFileTasks.Add(t);
                    }
                }
                _archiveFileTasks.Add(mainTask);

            }

            return _archiveFileTasks;
        }
    }
}
