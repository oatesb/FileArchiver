using System;
using System.Collections.Generic;
using System.Text;

namespace FileArchiver
{
    interface IArchiverTask
    {
        ArchiveFileTask TaskFile { get; set; }
        string TaskStatusMsg { get; set; }
        void DoTask();
    }
}
