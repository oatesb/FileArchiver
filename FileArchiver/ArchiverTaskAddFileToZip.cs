using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FileArchiver
{
    class ArchiverTaskAddFileToZip : IArchiverTask
    {
        public ArchiveFileTask TaskFile { get; set; }
        public string ZipPath { get; set; }

        public ArchiverTaskAddFileToZip(ArchiveFileTask file, string zipPath)
        {
            TaskFile = file;
            ZipPath = zipPath;
        }

        public void DoTask()
        {
            using (var zipArchive = ZipFile.Open(ZipPath, ZipArchiveMode.Update))
            {
                var fileInfo = new FileInfo(TaskFile.FileDetails.TheFile.FullName);
                zipArchive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                TaskFile.Status = FileTaskStatus.Done;
            }
        }
    }
}
