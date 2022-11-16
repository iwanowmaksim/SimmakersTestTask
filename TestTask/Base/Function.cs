using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestTask
{
    public class Function : INotifyPropertyChanged
    {
        public Function()
        {
            values = new ObservableCollection<Node>();
        }

        public Function(Node node) : this()
        {
            AddNode(node);
        }

        public Function(ObservableCollection<Node> nodes) : this()
        {
            AddNodes(nodes);
        }

        private ObservableCollection<Node> values;
        public ObservableCollection<Node> Values
        {
            get => values;
            set
            {
                values = value;
                OnPropertyChanged("FunctionValues");
            }
        }

        public ObservableCollection<Node> InverseValues
        {
            get
            {
                if (IsInvertible)
                {
                    ObservableCollection<Node> inverseFunctionValues = new ObservableCollection<Node>();

                    foreach (var node in Values)
                    {
                        inverseFunctionValues.Add(new Node(node.Y, node.X));
                    }
                    
                    return inverseFunctionValues;
                }
                else
                {
                    return new ObservableCollection<Node>();
                }
            }
        }

        // Условием обратимости функции принята её монотонность
        private bool IsInvertible
        {
            get
            {
                if (values.Count < 3)
                {
                    return true;
                }

                double eps = 1e-6;
                bool isIncreasing = values[1].Y > values[0].Y + eps;

                for (int i = 2; i < values.Count; i++)
                {
                    double difference = values[i].Y - values[i - 1].Y - eps;

                    if (isIncreasing && difference < 0 || !isIncreasing && difference > 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private void AddNode(Node node)
        {
            Values.Add(node);
        }
        private void AddNodes(ObservableCollection<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                AddNode(node);
            }
        }

        public void Add(Node node)
        {
            AddNode(node);
        }
        public void Add(ObservableCollection<Node> nodes)
        {
            AddNodes(nodes);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
