using System;
using System.Collections.Generic;
using System.IO;

namespace FileArchiver
{
    interface IFileMetadata
    {
        bool MatchedSearch { get; set; }
        FileInfoMapper TheFile { get; set; }
    }
}
