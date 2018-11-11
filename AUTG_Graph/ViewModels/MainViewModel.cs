using System;
using AUTG_Graph.BaseClasses;
using AUTG_Graph.Model;
using AUTG_Graph.ViewModels.Commands;

namespace AUTG_Graph.ViewModels
{
	class MainViewModel : BindableBase
	{
		#region Private variables

		private Graph graph;

		#endregion

		#region Public Proerties (and their private values)

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
						graph = new Graph(vertCount, (float)EdgeChance);

					NMatrix = graph.NMatrix;
				},
				delegate
				{
					return UInt32.TryParse(VertCount, out uint vertCount);
				});

			FixToEulerCommand = new RelayCommand(
				delegate { },					// For testing, put FixToEuler call here
				delegate { return true; });     // For testing, put CanFixToEuler call here (if aplicable)

			FindEulerCommand = new RelayCommand(
				delegate { },					// For testing, put FindEuler call here
				delegate { return true; });		// For testing, put CanFindEuler here
		}

		#endregion 
	}
}
