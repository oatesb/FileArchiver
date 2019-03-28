using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FileArchiver
{
    class ArchiverTaskCopy : IArchiverTask
    {
        public ArchiveFileTask TaskFile { get; set; }
        public string CopyDestFolderPath { get; set; }
        public long MaxZipSizeBytes { get; set; }

        public ArchiverTaskCopy(ArchiveFileTask file, string copyDestFolderPath)
        {
            TaskFile = file;
            CopyDestFolderPath = copyDestFolderPath;
        }
        public void DoTask()
        {
            var destPath = Path.Combine(CopyDestFolderPath, TaskFile.FileDetails.TheFile.Name);
            File.Copy(TaskFile.FileDetails.TheFile.FullName, destPath, true);
            TaskFile.Status = FileTaskStatus.Done;
        }
    }
}
