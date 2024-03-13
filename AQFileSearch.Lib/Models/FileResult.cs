using System;
using System.Collections.Generic;
using System.Text;

namespace AQFileSearch.Lib.Models
{

    public class FileResult
    {
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string LineValue { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
