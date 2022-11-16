using LiveCharts.Defaults;
using LiveCharts;
using System.Collections.Generic;

namespace TestTask
{
    public class Node
    {
        public Node(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public static Node Zero => new Node(0, 0);
        public ObservablePoint ObservablePoint => new ObservablePoint(X, Y);
        public static ChartValues<ObservablePoint> ToObservablePoints(IEnumerable<Node> nodes)
        {
            ChartValues<ObservablePoint> observablePoints = new ChartValues<ObservablePoint>();

            foreach (var node in nodes)
            {
                observablePoints.Add(node.ObservablePoint);
            }

            return observablePoints;
        }

        public override string ToString() => $"{X} {Y}";
    }
}