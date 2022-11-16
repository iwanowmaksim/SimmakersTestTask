using System.Collections.Generic;

namespace TestTask
{
    public static class StringHelper
    {
        public const string EndLine = "\n";
        public const string DefaultFileName = "Default.txt";
        public const string FileFormatFilter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        public const string WarningUnsavedChanges = "You have unsaved changes in files:";
        public const string TemperatureTitle = "Temperature [°C]";
        public const string AbsoluteMarkTitle = "Absolute mark [mm]";
        public const string AddTitle = "Add";
        public const string InverseTitle = "Inverse";
        public const string FileTitle = "File";
        public const string OpenTitle = "Open";
        public const string SaveTitle = "Save";
        public const string SaveAsTitle = "Save as...";
        public const string CloseTitle = "Close";
        public const string ExitTitle = "Exit";
        public const string OkTitle = "OK";
        public const string CancelTitle = "Cancel";

        public static string NodesToString(IEnumerable<Node> nodes)
        {
            string nodesToString = string.Empty;

            foreach (var node in nodes)
            {
                nodesToString += $"{node}{EndLine}";
            }

            return nodesToString;
        }
    }
}
