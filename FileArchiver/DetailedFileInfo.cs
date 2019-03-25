using System;
using System.Collections.Generic;
using System.IO;

namespace FileArchiver
{
    class DetailedFileInfo : IFileMetadata
    {
        public bool MatchedSearch { get; set; }
        public FileInfoMapper TheFile { get; set; }

        public FileHash HashCode { get; }

        public DetailedFileInfo(FileInfo file, bool matchedSearch)
        {
            TheFile = new FileInfoMapper(file);
            MatchedSearch = matchedSearch;
            // if it passed the search criteria hash the file
            if (MatchedSearch)
            {
                HashCode = new FileHash(file);
            }
            else // hash the file name and not the path.
            {
                HashCode = new FileHash(file.Name);
            }
            
        }
    }
}
