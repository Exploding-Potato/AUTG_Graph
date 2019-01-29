using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
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
			set
			{
				SetProperty(ref _vertCount, value);
			}
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

		private string _graphStateMessage;
		public string GraphStateMessage
		{
			get { return _graphStateMessage; }
			set { SetProperty(ref _graphStateMessage, value); }
		}

		private bool _showEulerPath;
		public bool ShowEulerPath
		{
			get { return _showEulerPath; }
			set
			{
				SetProperty(ref _showEulerPath, value);

				if (graph != null)
					DrawGraph();
			}
		}

		private string _eulerPathString;
		public string EulerPathString
		{
			get { return _eulerPathString; }
			set { SetProperty(ref _eulerPathString, value); }
		}

		#endregion

		#region RelayCommands

		public RelayCommand GenerateGraphCommand { get; private set; }
		public RelayCommand FixToEulerCommand { get; private set; }
		public RelayCommand FindEulerCommand { get; private set; }
		public RelayCommand ResizeWindowCommand { get; private set; }

		#endregion

		#region Constructor

		public MainViewModel()
		{
			GenerateGraphCommand = new RelayCommand(
				delegate
				{
					EulerPathString = "";

					if (UInt32.TryParse(VertCount, out uint vertCount))
						graph = new GraphVisual(vertCount, (float)EdgeChance, (10, 10), 50);

					NMatrix = graph.NMatrix;

					DrawGraph();
					SetGraphStateMessage();
					if (graph.IsEuler())
						SetEulerPathString();
				},
				delegate
				{
					bool parseSyccesful = UInt32.TryParse(VertCount, out uint vertCount);
					return parseSyccesful && vertCount < 100;
				});

			FixToEulerCommand = new RelayCommand(
				delegate {
					graph.FixToEulerGraph();
					OnPropertyChanged(nameof(NMatrix));

					DrawGraph();
					SetGraphStateMessage();
					SetEulerPathString();
				},
				delegate { return graph != null ? !graph.IsEuler() : false; });

			ResizeWindowCommand = new RelayCommand(
				delegate
				{
					DrawGraph();
					SetGraphStateMessage();
				});

			GraphCanvas = new Canvas();


		}
		
		private void SetGraphStateMessage()
		{
			if (graph == null)
				return;

			if (graph.IsEuler())
				GraphStateMessage = "Graf jest Eulerowski";
			else if (graph.IsConnected())
				GraphStateMessage = "Graf jest spójny";
			else
				GraphStateMessage = "Graf jest niespójny";
		}

		private void DrawGraph()
		{
			GraphDrawing.DrawGraph(graph, GraphCanvas, Brushes.Pink, Brushes.DarkGray);

			if (ShowEulerPath)
				GraphDrawing.DrawEuler(graph, GraphCanvas);
		}
		
		private void SetEulerPathString()
		{
			StringBuilder sb = new StringBuilder();

			List<uint> eulerPath = graph.FindEulerPath();
			foreach (var vertex in eulerPath)
			{
				sb.Append(vertex + 1);
				sb.Append(" -> ");
			}
			sb.Remove(sb.Length - 3, 3);

			EulerPathString = sb.ToString();
		}

		#endregion
	}
}
