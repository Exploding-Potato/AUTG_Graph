using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTG_Graph.Graph
{
	class Graph
	{
		private Random rand = new Random();

		private bool[,] nMatrix;
		public bool[,] NMatrix { get { return nMatrix; } }

		private uint size;
		public uint Size { get { return size; } }

		public Graph(uint size, float edgeChance)
		{
			this.size = size;
			nMatrix = new bool[size, size];

			for(uint i = 0; i < size; ++i)
			{
				for(uint j = 0; j < size; ++j)
				{
					nMatrix[i, j] = rand.NextDouble() <= edgeChance;
				}
			}
		}
	}
}
