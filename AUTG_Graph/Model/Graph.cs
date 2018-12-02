using System;
using System.Collections.Generic;

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

		public (int, int)[] Edges()
		{
			List<(int, int)> returnList = new List<(int, int)>();

			for (int i = 0; i < Size; i++)
			{
				for (int j = i + 1; j < Size; j++)
				{
					if (NMatrix[i, j])
						returnList.Add((i, j));
				}
			}

			return returnList.ToArray();
		}
	}
}
