using AQFileSearch.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AQFileSearch.Lib
{
    public class FileSearchAndReplace
    {
        private readonly string rootDirectory;

        public FileSearchAndReplace(string rootDirectory)
        {
            this.rootDirectory = rootDirectory;
        }

        public List<FileResult> SearchFiles(string[] includeExtensions, string[] excludeExtensions, string searchText, bool useRegex)
        {
            List<FileResult> results = new List<FileResult>();
            SearchFilesRecursively(rootDirectory, includeExtensions, excludeExtensions, searchText, useRegex, results);
            return results;
        }

        private void SearchFilesRecursively(string directory, string[] includeExtensions, string[] excludeExtensions, string searchText, bool useRegex, List<FileResult> results)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                string extension = Path.GetExtension(file);
                if (includeExtensions.Length == 0 || Array.Exists(includeExtensions, ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                {
                    if (excludeExtensions.Length == 0 || !Array.Exists(excludeExtensions, ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                    {
                        SearchFileContent(file, searchText, useRegex, results);
                    }
                }
            }

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                SearchFilesRecursively(subdirectory, includeExtensions, excludeExtensions, searchText, useRegex, results);
            }
        }

        private void SearchFileContent(string filePath, string searchText, bool useRegex, List<FileResult> results)
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                string line = lines[lineNumber];
                if ((useRegex && Regex.IsMatch(line, searchText)) || (!useRegex && line.Contains(searchText)))
                {
                    results.Add(new FileResult
                    {
                        FileName = Path.GetFileName(filePath),
                        LineNumber = lineNumber + 1,
                        LineValue = line,
                        StartIndex = useRegex ? -1 : line.IndexOf(searchText),
                        EndIndex = useRegex ? -1 : line.IndexOf(searchText) + searchText.Length,
                        RelativePath = filePath.Substring(rootDirectory.Length),
                        FullPath = filePath,
                        CreatedDate = File.GetCreationTime(filePath),
                        ModifiedDate = File.GetLastWriteTime(filePath)
                    });
                }
            }
        }
        public void ReplaceText(FileResult result, string replacementText)
        {

            string[] lines = File.ReadAllLines(result.FullPath);
            lines[result.LineNumber - 1] = lines[result.LineNumber - 1].Remove(result.StartIndex, result.EndIndex - result.StartIndex).Insert(result.StartIndex, replacementText);
            File.WriteAllLines(result.FullPath, lines);
        }
    }
}




