using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AUTG_Graph.BaseClasses;
using AUTG_Graph.Model;
using AUTG_Graph.ViewModels.Commands;

namespace AUTG_Graph.ViewModels
{
	class MainViewModel : BindableBase
	{
		private Graph graph;

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
		
		public RelayCommand GenerateGraphCommand { get; private set; }

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
		}
	}
}
