using System.IO;

namespace TestTask
{
    public class WorkFile
    {
        public WorkFile(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
            IsUnsaved = false;
        }

        public WorkFile(string fileName) : this($"{Directory.GetCurrentDirectory()}\\{fileName}", fileName) { }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsUnsaved { get; set; }
    }
}
