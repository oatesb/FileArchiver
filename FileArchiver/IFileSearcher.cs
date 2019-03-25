using System;
using System.Collections.Generic;

namespace FileArchiver
{
    interface IFileSearcher
    {
        IEnumerable<DetailedFileInfo> FindFiles();
        DateTime ReturnMinLastModifiedTime();
    }
}
