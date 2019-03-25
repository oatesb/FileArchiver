using System;
using System.Collections.Generic;
using System.IO;

namespace FileArchiver
{
    interface IFileMetadata
    {
        bool MatchedSearch { get; set; }
        FileInfo TheFile { get; set; }
    }
}
