using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileArchiver
{
    class FileInfoMapper
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string FolderPath { get; set; }
        public DateTime LastModTimeUtc { get; set; }
        public DateTime CreateTimeUtc { get; set; }
        public long Size { get; set; }

        public FileInfoMapper(FileInfo file)
        {
            FullName = file.FullName;
            Name = file.Name;
            FolderPath = file.DirectoryName;
            LastModTimeUtc = file.LastWriteTimeUtc;
            Size = file.Length;
            CreateTimeUtc = file.CreationTimeUtc;
        }
    }
}
