using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileArchiver
{
    class FileHash
    {
        public string Hash { get; set; }
        public bool HashSuccessful { get; set; }

        public FileHash(FileInfo inputFile)
        {
            try
            {
                Hash = FileUtils.GetSHA1Hash(inputFile.FullName);
                HashSuccessful = true;
            }
            catch (Exception)
            {
                HashSuccessful = false;
                throw;
            }
            
        }

        // hash on the string and not the file contents
        public FileHash(string str)
        {
            try
            {
                Hash = FileUtils.GetSHA1HashFromString(str);
                HashSuccessful = true;
            }
            catch (Exception)
            {
                HashSuccessful = false;
                throw;
            }

        }

        
    }
}
