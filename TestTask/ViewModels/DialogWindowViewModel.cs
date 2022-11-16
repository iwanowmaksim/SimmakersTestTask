namespace TestTask
{
    public class DialogWindowViewModel
    {
        public DialogWindowViewModel(string message, string text)
        {
            Message = message;
            Text = text;
        }

        public string OkTitle => StringHelper.OkTitle;
        public string CancelTitle => StringHelper.CancelTitle;

        public string Message { get; set; }
        public string Text { get; set; }
    }
}
