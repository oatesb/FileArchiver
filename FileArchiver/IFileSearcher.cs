using System;
using System.Collections.Generic;

namespace FileArchiver
{
    interface IFileSearcher
    {
        IReadOnlyList<DetailedFileInfo> FindFiles();
        DateTime ReturnMinLastModifiedTime();
    }
}
