﻿using System;
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

		public void FixToEulerGraph()
		{
			Random random = new Random();

			while (!IsEuler() || !IsConnected())
			{
				for(uint i = 0; i < Size; ++i)
				{
					uint deg = GetDeg(NMatrix, i);
					
					if(deg % 2 == 1)
					{
						for(uint j = 0; j < Size; ++j)
						{
							if(i == j)
							{
								continue;
							}

							uint deg2 = GetDeg(NMatrix, j);

							if(deg2 % 2 == 1)
							{
								if(NMatrix[i, j])
								{
									NMatrix[i, j] = false;
									NMatrix[j, i] = false;
								}
								else
								{
									NMatrix[i, j] = true;
									NMatrix[j, i] = true;
								}
								break;
							}
						}
					}

					if(deg == 0)
					{
						uint v1;
						do
						{
							v1 = (uint)random.Next((int)Size);
						} while (v1 == i);

						uint v2;
						do
						{
							v2 = (uint)random.Next((int)Size);
						} while (v2 == i || v1 == i);

						NMatrix[i, v1] = true;
						NMatrix[v1, i] = true;
						NMatrix[i, v2] = true;
						NMatrix[v2, i] = true;
					}

					if(!IsConnected())
					{
						bool[] visited = VisitedArray(i);
						int shift = random.Next();
						for(int j = 0; j < Size; ++j)
						{
							if(i == j)
							{
								continue;
							}

							int thisJ = (j + shift) % (int)Size;
							if(!visited[thisJ])
							{
								NMatrix[i, thisJ] = true;
								NMatrix[thisJ, i] = true;
								break;
							}
						}
					}
				}
			}
		}

		public List<uint> FindEulerPath()
		{
			List<uint> eulerPath = new List<uint>();

			uint current = 0;
			bool pathExists;
			bool[,] MatrixCopy = (bool[,])NMatrix.Clone();
		
			eulerPath.Add(current);

			do
			{
				pathExists = false;

				for(uint i = 0; i < Size; ++i)
				{
					if(MatrixCopy[current, i])
					{
						if(current == i)
						{
							continue;
						}

						uint nodeCount = FindNodes(MatrixCopy, i);
						MatrixCopy[current, i] = MatrixCopy[i, current] = false;
						if(nodeCount == FindNodes(MatrixCopy, i) || GetDeg(MatrixCopy, current) == 0)
						{
							current = i;
							eulerPath.Add(current);
							pathExists = true;
						}
						else
						{
							MatrixCopy[current, i] = MatrixCopy[i, current] = true;
						}
					}
				}

			}while(pathExists);

			return eulerPath;
		}

		private uint FindNodes(bool[,] Matrix, uint node)
		{
			bool[] visited = new bool[Size];
			Queue<uint> queue = new Queue<uint>();

			visited[node] = true;
			queue.Enqueue(node);

			while(queue.Count != 0)
			{
				uint current = queue.Dequeue();

				for(uint i = 0; i < Size; ++i)
				{
					if(!visited[i] && Matrix[current, i])
					{
						visited[i] = true;
						queue.Enqueue(i);
					}
				}
			}

			uint result = 0;

			for(int i = 0; i < Size; ++i)
			{
				if(visited[i])
				{
					result++;
				}
			}

			return result - 1;
		}

		private uint GetDeg(bool[,] Matrix, uint index)
		{
			uint vertDeg = 0;
			for (uint i = 0; i < Size; ++i)
			{
				vertDeg += Convert.ToUInt32(Matrix[index, i]);
			}

			return vertDeg;
		}

		public bool IsEuler()
		{
			for(uint i  = 0; i < Size; ++i)
			{
				if(GetDeg(NMatrix, i) % 2 == 1 || GetDeg(NMatrix, i) == 0)
				{
					return false;
				}
			}

			return true;
		}

		public bool IsConnected()
		{
			bool[] visited = VisitedArray(0);

			for (int i = 0; i < Size; i++)
			{
				if(!visited[i])
				{
					return false;
				}
			}

			return true;
		}

		private bool[] VisitedArray(uint start)
		{
			bool[] visited = new bool[Size];

			visited[start] = true;
			Queue<uint> queue = new Queue<uint>();
			queue.Enqueue(start);

			while (queue.Count != 0)
			{
				uint current = queue.Dequeue();

				for (uint i = 0; i < Size; i++)
				{
					if (i == current)
					{
						continue;
					}

					if (NMatrix[current, i] && !visited[i])
					{
						queue.Enqueue(i);
						visited[i] = true;
					}
				}
			}

			return visited;
		}
	}
}
