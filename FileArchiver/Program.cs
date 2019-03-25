using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileArchiver
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"Z:\test\temp.file";
            //testHast(path);
            //testHast(@"Z:\test\test01\test0A.txt");
            //testHast(@"Z:\BenOatesCancelServiceContract.pdf");
            //testHast(@"Z:\Ben PC\izotope\iZotope_Nectar_v2_02_Production_Suite.exe");
            //testHast(@"Z:\Archiver\89edabfe-c6e6-401c-a8e4-e6d59075a280.zip");

            long testBytes = 3000000;
            var testDate = DateTime.Parse("2017-03-15 00:00:00");
            string rootDir = @"z:\test";
            //rootDir = @"Z:\Ben PC";
            DirectoryFileSearcher dfs = new DirectoryFileSearcher(
                rootDir,
                "*",
                SearchOption.AllDirectories,
                testBytes,
                testDate);

            ArchiveManager manager = new ArchiveManager(dfs);

            manager.SearchFiles();
            var l = manager.MakeJobList();

            var json = JsonConvert.SerializeObject(l, Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            var daFiles = manager.GetAllFilesFromSearch();
            var daFilesMatched = manager.GetFilesThatMatchedSearch();
            var daFilesNotMatched = manager.GetFilesThatFailedToMatchedSearch();

            Console.WriteLine($"Min Time: {manager.GetMinDateForMatchingFiles()} total files: {daFiles.Count()} NOT: {daFilesNotMatched.Count()} GOOD: {daFilesMatched.Count()} ");


            var filenames = dfs.FindFiles();
            PrintItems(filenames, "**********************");

            //testDate = DateTime.Parse("2019-03-18 15:00:00");
            //dfs = new DirectoryFileSearcher(
            //    rootDir,
            //    "*",
            //    SearchOption.AllDirectories,
            //    testBytes,
            //    testDate);

            //filenames = dfs.FindFiles();
            //PrintItems(filenames, "**********************");

            //dfs = new DirectoryFileSearcher(
            //    rootDir,
            //    "*",
            //    SearchOption.AllDirectories,
            //    ignoreFilesGreaterThanBytes: -1);

            //filenames = dfs.FindFiles();
            //PrintItems(filenames, "**********************");

            //dfs = new DirectoryFileSearcher(
            //    rootDir,
            //    "*",
            //    SearchOption.AllDirectories);

            //filenames = dfs.FindFiles();
            //PrintItems(filenames, "**********************");

        }
        static void PrintItems(IEnumerable<DetailedFileInfo> dfi, string extra)
        {
            Console.WriteLine(extra);

            var toobig = dfi.Where(f => f.MatchedSearch == false).Count();
            var passed = dfi.Where(f => f.MatchedSearch == true).Count();

            var groups = dfi.GroupBy(f => f.HashCode.Hash)
                .Select(group => new
                {
                    Metric = group.Key,
                    Details = group,
                    Count = group.Count()
                })
                .OrderByDescending(o => o.Count);

            foreach (var item in groups)
            {
                Console.WriteLine($"{item.Metric}: {item.Count}: {item.Details.First().TheFile.Name}");
            }

            Console.WriteLine($"total files: {dfi.Count()} NOT: {toobig} GOOD: {passed} ");

            Console.WriteLine();
            foreach (var item in dfi)
            {
                Console.WriteLine($"{item.TheFile.FullName} {item.MatchedSearch} {item.HashCode.Hash}");
            }
        }

    }

    
}
