using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTG_Graph.Model
{
	class Graph
	{
		private Random rand = new Random();
		
		public bool[,] NMatrix { get; private set; }
		public uint Size { get; private set; }

		public Graph(uint size, float edgeChance)
		{
			GenerateGraph(size, edgeChance);
		}

		private void GenerateGraph(uint size, float edgeChance)
		{
			this.Size = size;
			NMatrix = new bool[size, size];

			for (uint i = 0; i < size; ++i)
			{
				for (uint j = i + 1; j < size; ++j)
				{
					NMatrix[i, j] = NMatrix[j, i] = rand.NextDouble() <= edgeChance;
				}
			}
		}
	}
}
