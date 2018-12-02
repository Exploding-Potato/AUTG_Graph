using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using AUTG_Graph.BaseClasses;
using AUTG_Graph.Model;
using AUTG_Graph.ViewModels.Commands;

namespace AUTG_Graph.ViewModels
{
	class MainViewModel : BindableBase
	{
		#region Private variables

		private GraphVisual graph;

		#endregion

		#region Public Properties (and their private values)

		private string _vertCount;
		public string VertCount
		{
			get { return _vertCount; }
			set { SetProperty(ref _vertCount, value); }
		}

		private double _edgeChance;
		public double EdgeChance
		{
			get { return _edgeChance; }
			set { SetProperty(ref _edgeChance, value); }
		}

		private bool[,] _nMatrix;
		public bool[,] NMatrix
		{
			get { return _nMatrix; }
			set { SetProperty(ref _nMatrix, value); }
		}
		
		private Canvas _graphCanvas;
		public Canvas GraphCanvas
		{
			get { return _graphCanvas; }
			set { SetProperty(ref _graphCanvas, value); }
		}

		#endregion

		#region RelayCommands

		public RelayCommand GenerateGraphCommand { get; private set; }
		public RelayCommand FixToEulerCommand { get; private set; }
		public RelayCommand FindEulerCommand { get; private set; }

		#endregion

		#region Constructor

		public MainViewModel()
		{
			GenerateGraphCommand = new RelayCommand(
				delegate
				{
					if (UInt32.TryParse(VertCount, out uint vertCount))
						graph = new GraphVisual(vertCount, (float)EdgeChance, (10, 10), 50);

					NMatrix = graph.NMatrix;

					GraphDrawing.DrawGraph(graph, GraphCanvas, Brushes.Pink, Brushes.Red);
				},
				delegate
				{
					return UInt32.TryParse(VertCount, out uint vertCount);
				});

			FixToEulerCommand = new RelayCommand(
				delegate {
					GraphDrawing.DrawGraph(graph, GraphCanvas, Brushes.Pink, Brushes.Red);
				},											// For testing, put FixToEuler call here
				delegate { return !(graph == null); });     // For testing, put CanFixToEuler call here (if aplicable)

			FindEulerCommand = new RelayCommand(
				delegate
				{
					GraphDrawing.DrawGraph(graph, GraphCanvas, Brushes.Pink, Brushes.Red);
				},											// For testing, put FindEuler call here
				delegate { return !(graph == null); });     // For testing, put CanFindEuler here

			GraphCanvas = new Canvas();
			
			Console.WriteLine(GraphCanvas.ActualHeight);

			Ellipse ellipse = new Ellipse();
			ellipse.Fill = Brushes.Beige;
			ellipse.Height = 200;
			ellipse.Width = 400;
			GraphCanvas.Children.Add(ellipse);

			Line line = new Line();
			line.Fill = Brushes.Red;
			line.X1 = 0;
			line.Y1 = 0;
			line.X2 = 999;
			line.Y2 = 999;
			line.StrokeThickness = 666;
			GraphCanvas.Children.Add(line);
		}

		#endregion
	}
}
