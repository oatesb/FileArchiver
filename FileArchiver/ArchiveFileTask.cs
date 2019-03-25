using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileArchiver
{
    enum FileTaskStatus { NotStarted, Done, Failed, SkippedDuplicate, SkippedFailedToHash };

    class ArchiveFileTask
    {
        //public DetailedFileInfo FileDetails { get; set; }
        public DetailedFileInfo FileDetails { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FileTaskStatus Status { get; set; }
        public ArchiveFileTask MasterFileTask { get; set; }

        public ArchiveFileTask(DetailedFileInfo file)
        {
            FileDetails = file;
            Status = CalcInitialStatus(FileDetails);
            MasterFileTask = null;
        }

        public ArchiveFileTask(DetailedFileInfo file, FileTaskStatus knownStatus)
        {
            FileDetails = file;
            Status = knownStatus;
            MasterFileTask = null;
        }

        public ArchiveFileTask(DetailedFileInfo file, ArchiveFileTask masterFile)
        {
            FileDetails = file;
            Status = FileTaskStatus.SkippedDuplicate;
            MasterFileTask = masterFile;
        }

        private FileTaskStatus CalcInitialStatus(DetailedFileInfo fileDetails)
        {
            if (fileDetails.HashCode.HashSuccessful)
            {
                return FileTaskStatus.NotStarted;
            }
            else
            {
                return FileTaskStatus.SkippedFailedToHash;
            }
        }
    }
}
