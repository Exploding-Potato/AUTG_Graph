using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTG_Graph.ViewModels
{
	class GraphViewModel
	{
		private Graph graph;

		public void GenerateRandom(int vertCount, float edgeChance, int maxWeight = 1)
		{
			graph = new Graph();
		}

		public int[,] GetGraphMatrix()
		{
			if (graph == null || graph.Matrix == null)
				return null;

			else
				return graph.vertMatrix;
		}
    }
}
