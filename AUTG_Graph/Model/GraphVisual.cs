﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace AUTG_Graph.Model
{
	class GraphVisual : Graph
	{
		public Point[] vertPositions;

		public GraphVisual(uint size, float edgeChance, (uint horizontalPositions, uint verticalPositions) positionCount, uint positionDistance) : base(size, edgeChance)
		{
			SetRandomPositions(size, positionCount, positionDistance);
		}

		void SetRandomPositions(uint size, (uint horizontalPositions, uint verticalPositions) positionCount, uint positionDistance)
		{
			#region Sanity checks

			if (size > positionCount.horizontalPositions * positionCount.verticalPositions)
				throw new ArgumentException("He graph too big for he gotdamn canvass");

			#endregion

			List<Point> tempPositions = new List<Point>();

			uint vertsSet = 0;
			uint positionsTotal = positionCount.horizontalPositions * positionCount.verticalPositions;
			Random random = new Random();

			// We iterate over all points in the given grid
			for (int i = 0; i < positionCount.horizontalPositions && vertsSet < size; i++)
			{
				for (int j = 0; j < positionCount.verticalPositions; j++)
				{
					double pointHereChance
						= (double)(size - vertsSet)												// Remaining points to set
						/ (double)(positionsTotal - (i * positionCount.verticalPositions + j));	// Remaining points to consider

					if (random.NextDouble() <= pointHereChance)
					{
						tempPositions.Add(new Point(
							(i + 1) * positionDistance,
							(j + 1) * positionDistance));

						vertsSet++;
					}
				}
			}


			vertPositions = tempPositions.ToArray();
		}
	}
}