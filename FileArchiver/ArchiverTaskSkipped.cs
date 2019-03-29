using System;
using System.Collections.Generic;
using System.Text;

namespace FileArchiver
{
    class ArchiverTaskSkipped : IArchiverTask
    {
        public ArchiveFileTask TaskFile { get; set; }
        public string TaskStatusMsg { get; set; }

        public ArchiverTaskSkipped(ArchiveFileTask file, string message, FileTaskStatus statusType)
        {
            TaskFile = file;
            TaskStatusMsg = message;
            TaskFile.Status = statusType;
        }

        public void DoTask()
        {
            Console.WriteLine($"Skip Task File: {TaskFile.FileDetails.TheFile.FullName} was skipped with reason {TaskStatusMsg}");
        }
    }
}
