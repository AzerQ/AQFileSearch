using CommandLine;

namespace AQFileSearch
{
    public class Options
    {
        [Option('d', "directory", Required = true, HelpText = "Root directory for the search.")]
        public string RootDirectory { get; set; }

        [Option('s', "search", Required = true, HelpText = "Text to search.")]
        public string SearchText { get; set; }

        [Option('r', "replace", Default = null, HelpText = "Text to replace the found text.")]
        public string ReplacementText { get; set; }

        [Option('i', "include", Default = new string[] { }, HelpText = "Include file extensions (comma-separated).")]
        public IEnumerable<string> IncludeExtensions { get; set; }

        [Option('e', "exclude", Default = new string[] { }, HelpText = "Exclude file extensions (comma-separated).")]
        public IEnumerable<string> ExcludeExtensions { get; set; }

        [Option("regex", Default = false, HelpText = "Use regular expressions for search.")]
        public bool UseRegex { get; set; }

        [Option("details", Default = false, HelpText = "Show the lines where the text was found with highlighting.")]
        public bool ShowFoundLines { get; set; }
    }
}
