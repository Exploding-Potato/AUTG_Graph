using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		

		public RelayCommand GenerateGraphCommand { get; private set; }
		
		public MainViewModel()
		{
			GenerateGraphCommand = new RelayCommand(GenerateRandom, CanGenerate);
		}


		bool CanGenerate(Object obj)
		{
			return UInt32.TryParse(VertCount, out uint vertCount);
		}
		
		public void GenerateRandom(Object obj/*uint maxWeight = 1*/)
		{
			if (UInt32.TryParse(VertCount, out uint vertCount))
				graph = new Graph(vertCount, (float)EdgeChance);
		}

		public Object GetGraphMatrix()
		{
			if (graph == null || graph.NMatrix == null)
				return null;

			return graph.NMatrix;
		}
    }
}
