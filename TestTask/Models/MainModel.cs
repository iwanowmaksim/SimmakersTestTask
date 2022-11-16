using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestTask
{
    public class MainModel : INotifyPropertyChanged
    {
        private Function function;
        public Function Function
        {
            get => function;
            set 
            { 
                function = value;
                OnPropertyChanged("Function");
            }
        }

        private WorkFile file;
        public WorkFile File
        {
            get => file;
            set
            {
                file = value;
                OnPropertyChanged("File");
            }
        }

        public static MainModel DefaultModel
        {
            get
            {
                ObservableCollection<Node> defaultNodes = new ObservableCollection<Node>
                {
                    new Node(0, 0),
                    new Node(1, 1),
                    new Node(2, 4),
                    new Node(3, 9),
                    new Node(4, 16),
                };

                MainModel model = new MainModel()
                {
                    Function = new Function(defaultNodes),
                    File = new WorkFile(StringHelper.DefaultFileName)
                };

                return model;
            }
        }

        public override string ToString() => File.FileName;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
