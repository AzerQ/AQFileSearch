using AQFileSearch.Lib.Models;
using AQFileSearch.Lib;
using CommandLine;
using System.Text.RegularExpressions;

namespace AQFileSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    FileSearchAndReplace fileSearchAndReplace = new FileSearchAndReplace(options.RootDirectory);

                    List<FileResult> results = fileSearchAndReplace.SearchFiles(options.IncludeExtensions.ToArray(), options.ExcludeExtensions.ToArray(), options.SearchText, options.UseRegex);

                    if (results.Count > 0)
                    {
                        Console.WriteLine("Files containing the specified text ({0}):", results.Count);
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {result.FileName}, Line: {result.LineNumber}, Path: {result.RelativePath}");
                            
                            if (options.ShowFoundLines)
                            {
                                Console.WriteLine("Found Line:");

                                string line = result.LineValue;

                                if (options.UseRegex)
                                {
                                    var matches = Regex.Matches(line, options.SearchText, RegexOptions.IgnoreCase);
                                    foreach (Match match in matches)
                                    {
                                        int startIndex = match.Index;
                                        int endIndex = startIndex + match.Length;

                                        Console.Write(line.Substring(0, startIndex));
                                        Console.ForegroundColor = ConsoleColor.Red; // Устанавливаем цвет для искомой подстроки
                                        Console.Write(line.Substring(startIndex, endIndex - startIndex));
                                        Console.ResetColor(); // Сбрасываем цвет обратно
                                        Console.WriteLine(line.Substring(endIndex));
                                    }
                                }
                                else
                                {
                                    int startIndex = line.IndexOf(options.SearchText, StringComparison.OrdinalIgnoreCase);
                                    if (startIndex != -1)
                                    {
                                        int endIndex = startIndex + options.SearchText.Length;

                                        Console.Write(line.Substring(0, startIndex));
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write(line.Substring(startIndex, endIndex - startIndex));
                                        Console.ResetColor();
                                        Console.WriteLine(line.Substring(endIndex));
                                    }
                                }
                            }
                        }


                        if (options.ReplacementText != null)
                        {
                            Console.WriteLine("Do you want to replace the text? (Y/N):");
                            if (Console.ReadKey().Key == ConsoleKey.Y)
                            {
                                foreach (var result in results)
                                {
                                    fileSearchAndReplace.ReplaceText(result, options.ReplacementText);
                                    Console.WriteLine($"Text replaced in file: {result.FileName}, Line: {result.LineNumber}, Path: {result.RelativePath}");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No files found with the specified text.");
                    }

                });
        }
    }

}
