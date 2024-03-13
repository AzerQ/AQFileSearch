# AQFileSearch

CLI utility for searching and replacing text in files recursively.

## Usage

### Installation

Make sure you have .NET Core SDK installed.

```bash
dotnet build -c Release
dotnet publish -c Release -r {your-runtime} --self-contained
```

Replace {your-runtime} with your target runtime (e.g., win10-x64, linux-x64, osx-x64).

###  Command Line Options

Example usage:
```bash
dotnet AQFileSearch.dll --directory <root_directory> --search <search_text> --replace <replacement_text> [options]
```

Options:
* -d, --directory: Root directory for the search.
* -s, --search: Text to search.
* -r, --replace: Text to replace the found text.
* -i, --include: Include file extensions (comma-separated).
* -e, --exclude: Exclude file extensions (comma-separated).
* --regex: Use regular expressions for search.
* --search-mode: Enable search mode.
* --replace-mode: Enable replace mode.
* --show-found-lines: Show the lines where the text was found with highlighting.

### Examples

1. Search for text in all files with a specific extension:
```bash
dotnet AQFileSearch.dll --directory /path/to/directory --search "your_search_text" --include ".txt" --regex
```
2. Search and replace text in files:
```bash
dotnet AQFileSearch.dll --directory /path/to/directory --search "your_search_text" --replace "your_replacement_text" --include ".txt"
```
3. Search for text and display the found lines with highlighting:
```bash
dotnet AQFileSearch.dll --directory /path/to/directory --search "your_search_text" --details
```