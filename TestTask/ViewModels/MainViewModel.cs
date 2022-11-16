using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Wpf;
using Prism.Commands;

namespace TestTask
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _openFileDialog = new OpenFileDialog()
            {
                Filter = StringHelper.FileFormatFilter
            };
            _saveFileDialog = new SaveFileDialog()
            {
                Filter = StringHelper.FileFormatFilter
            };

            Chart = new SeriesCollection();

            // При инициализации создается базовая кусочно-линейная квадратичная функция y=x^2
            Models = new ObservableCollection<MainModel>
            {
                MainModel.DefaultModel
            };

            SelectedModel = Models.FirstOrDefault();
            OnPropertyChanged("Chart");
        }

        #region Fields
        private readonly OpenFileDialog _openFileDialog;
        private readonly SaveFileDialog _saveFileDialog;

        private bool isInverseChartCheckBoxChecked;
        private SeriesCollection chart;
        private Node selectedNode;
        private ObservableCollection<MainModel> models;
        private MainModel selectedModel;

        private RelayCommand exitCommand;
        private RelayCommand openCommand;
        private RelayCommand saveCommand;
        private RelayCommand saveAsCommand;
        private RelayCommand closeCommand;
        private RelayCommand buttonAddCommand;
        private RelayCommand rowEditEndingCommand;

        private DelegateCommand copyCommand;
        private DelegateCommand pasteCommand;
        #endregion

        #region String constants
        public string TemperatureTitle => StringHelper.TemperatureTitle;
        public string AbsoluteMarkTitle => StringHelper.AbsoluteMarkTitle;
        public string AddTitle => StringHelper.AddTitle;
        public string InverseTitle => StringHelper.InverseTitle;
        public string FileTitle => StringHelper.FileTitle;
        public string OpenTitle => StringHelper.OpenTitle;
        public string SaveTitle => StringHelper.SaveTitle;
        public string SaveAsTitle => StringHelper.SaveAsTitle;
        public string CloseTitle => StringHelper.CloseTitle;
        public string ExitTitle => StringHelper.ExitTitle;
        #endregion

        #region Properties
        public bool IsInverseChartCheckBoxChecked
        {
            get => isInverseChartCheckBoxChecked;
            set
            {
                isInverseChartCheckBoxChecked = value;
                OnPropertyChanged("IsInverseChartCheckBoxChecked");
                OnPropertyChanged("Chart");
            }
        }

        public SeriesCollection Chart
        {
            get
            {
                chart = new SeriesCollection();

                if (IsInverseChartCheckBoxChecked)
                {
                    foreach (MainModel model in Models)
                    {
                        chart.Add(new LineSeries
                        {
                            Title = model.ToString(),
                            Values = Node.ToObservablePoints(model.Function.InverseValues),
                            LineSmoothness = 0,
                        });
                    }
                }
                else
                {
                    foreach (MainModel model in Models)
                    {
                        chart.Add(new LineSeries
                        {
                            Title = model.ToString(),
                            Values = Node.ToObservablePoints(model.Function.Values),
                            LineSmoothness = 0
                        });
                    }
                }

                ResetChartPointsGeometry();
                return chart;
            }
            set
            {
                chart = value;
                OnPropertyChanged("Chart");
            }
        }

        public ObservableCollection<Node> FunctionValues => SelectedModel.Function.Values;

        public Node SelectedNode
        {
            get => selectedNode;
            set
            {
                selectedNode = value;
                OnPropertyChanged("SelectedNode");
                OnPropertyChanged("Chart");
            }
        }

        public ObservableCollection<MainModel> Models
        {
            get => models;
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
        }

        public MainModel SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                ResetChartPointsGeometry();
                OnPropertyChanged("SelectedModel");
                OnPropertyChanged("FunctionValues");
            }
        }
        #endregion

        #region Methods
        // У графика функции, с которой ведется работа, выделяются узлы
        private void ResetChartPointsGeometry()
        {
            int index = Models.IndexOf(SelectedModel);

            if (index != -1 && index <= chart.Count - 1)
            {
                foreach (LineSeries lineSeries in chart.Cast<LineSeries>())
                {
                    lineSeries.PointGeometry = null;
                }

                ((LineSeries)chart[index]).PointGeometry = DefaultGeometries.Circle;
            }
        }

        private int IndexOfFile(string filePath)
        {
            for (int i = 0; i < Models.Count; i++)
            {
                if (Models[i].File.FilePath == filePath)
                {
                    return i;
                }
            }

            return -1;
        }

        private void PasteFromClipboard()
        {
            string clipboardText = Clipboard.GetText();
            ObservableCollection<Node> nodes = new ObservableCollection<Node>();

            string[] lines = clipboardText.Split('\n');

            foreach (string line in lines)
            {
                string[] words = line.Split(new char[] { ' ', '\t' });

                if (words.Length > 1 &&
                    double.TryParse(words[0].Replace('.', ','), out double x) &&
                    double.TryParse(words[1].Replace('.', ','), out double y))
                {
                    nodes.Add(new Node(x, y));
                }
            }

            SelectedModel.Function = new Function(nodes);
            SelectedModel.File.IsUnsaved = true;
            OnPropertyChanged("FunctionValues");
            OnPropertyChanged("Chart");
        }
        #endregion

        #region Commands
        public RelayCommand ExitCommand => exitCommand ?? (exitCommand = new RelayCommand(obj =>
        {
            string unsavedFiles = string.Empty;

            foreach (MainModel model in Models)
            {
                if (model.File.IsUnsaved)
                {
                    unsavedFiles += $"{model}{StringHelper.EndLine}";
                }
            }

            // При выходе из программы && при наличии несохранённых изменений открывается предупреждающее окно
            if (unsavedFiles != string.Empty)
            {
                DialogWindowView window = new DialogWindowView()
                {
                    DataContext = new DialogWindowViewModel(StringHelper.WarningUnsavedChanges, unsavedFiles)
                };

                if (window.ShowDialog() != true)
                {
                    return;
                }
            }

            Application.Current.MainWindow.Close();
        }));

        public RelayCommand OpenCommand => openCommand ?? (openCommand = new RelayCommand(obj =>
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                string filePath = _openFileDialog.FileName;
                int index = IndexOfFile(filePath);

                // При попытке открыть уже открытый файл текущая функция изменится на ту, которую пытаются открыть
                if (index != -1)
                {
                    SelectedModel = Models[index];
                }
                else
                {
                    string fileName = filePath.Split('\\').Last();

                    ObservableCollection<Node> nodes = new ObservableCollection<Node>();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (reader.Peek() >= 0)
                        {
                            string[] words = reader.ReadLine().Split(new char[] { ' ', '\t' });

                            if (words.Length > 1 && 
                            double.TryParse(words[0].Replace('.', ','), out double x) && 
                            double.TryParse(words[1].Replace('.', ','), out double y))
                            {
                                nodes.Add(new Node(x, y));
                            } 
                        }
                    }

                    Models.Add(new MainModel()
                    {
                        Function = new Function(nodes),
                        File = new WorkFile(filePath, fileName),
                    });

                    SelectedModel = Models.Last();
                    OnPropertyChanged("Chart");
                }
            }
        }));

        // "Ctrl + S" сохранит текущий файл
        public RelayCommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(obj =>
        {
            SelectedModel.File.IsUnsaved = false;
            File.WriteAllText(SelectedModel.File.FilePath, StringHelper.NodesToString(FunctionValues));
        }));

        // "Ctrl + Shift + S" сохранит в выбранный файл
        public RelayCommand SaveAsCommand => saveAsCommand ?? (saveAsCommand = new RelayCommand(obj =>
        {
            if (_saveFileDialog.ShowDialog() == true)
            {
                SelectedModel.File.IsUnsaved = false;
                File.WriteAllText(_saveFileDialog.FileName, StringHelper.NodesToString(FunctionValues));
            }
        }));

        public RelayCommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(obj =>
        {
            // При закрытии файла && при наличии в нём несохранённых изменений открывается предупреждающее окно
            if (SelectedModel.File.IsUnsaved)
            {
                DialogWindowView window = new DialogWindowView()
                {
                    DataContext = new DialogWindowViewModel(StringHelper.WarningUnsavedChanges, SelectedModel.File.FileName)
                };

                if (window.ShowDialog() != true)
                {
                    return;
                }
            }

            int index = Models.IndexOf(SelectedModel);

            if (index != -1 && Models.Count > 1)
            {
                if (index == 0)
                {
                    SelectedModel = Models[1];
                }
                else if (index == Models.Count - 1)
                {
                    SelectedModel = Models[index - 1];
                }
                else
                {
                    SelectedModel = Models[index + 1];
                }

                Models.RemoveAt(index);
                OnPropertyChanged("Chart");
            }
        }));

        public RelayCommand ButtonAddCommand => buttonAddCommand ?? (buttonAddCommand = new RelayCommand(obj =>
        {
            SelectedModel.Function.Add(Node.Zero);
            OnPropertyChanged("Chart");
        }));

        public RelayCommand RowEditEndingCommand => rowEditEndingCommand ?? (rowEditEndingCommand = new RelayCommand(obj =>
        {
            SelectedModel.File.IsUnsaved = true;
            OnPropertyChanged("FunctionValues");
        }));

        // "Ctrl + C" скопирует текущую функцию в буфер
        public DelegateCommand CopyCommand => copyCommand ?? (copyCommand = new DelegateCommand(CopyToClipboard));
        private void CopyToClipboard()
        {
            string clipboardText = StringHelper.NodesToString(FunctionValues);
            
            if (clipboardText.EndsWith("\n"))
            {
                clipboardText = clipboardText.Substring(0, clipboardText.Length - 1);
            }
                
            Clipboard.SetText(clipboardText);
        }

        // "Ctrl + V" вставит текущую функцию из буфера
        public DelegateCommand PasteCommand => pasteCommand ?? (pasteCommand = new DelegateCommand(PasteFromClipboard));
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
