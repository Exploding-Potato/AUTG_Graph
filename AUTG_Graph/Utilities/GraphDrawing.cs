#define _ONE_NUMBERING

using AUTG_Graph.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AUTG_Graph
{
	static class GraphDrawing
	{
		public static void DrawGraph(GraphVisual graph, Canvas canvas, Brush vertBrush, Brush lineBrush, int shapeSize = 25, int lineWidth = 5)
		{
			canvas.Children.Clear();

			Point[] vertPositions = graph.vertPositions;

			// Draws edges
			foreach ((int, int) e in graph.Edges())
			{
				Line line = new Line
				{
					X1 = vertPositions[e.Item1].X * canvas.ActualWidth,
					Y1 = vertPositions[e.Item1].Y * canvas.ActualHeight,
					X2 = vertPositions[e.Item2].X * canvas.ActualWidth,
					Y2 = vertPositions[e.Item2].Y * canvas.ActualHeight,
					Stroke = lineBrush
				};

				canvas.Children.Add(line);
			}

			// Draws text
			foreach (Point p in vertPositions)
			{
				Ellipse ellipse = new Ellipse
				{
					Height = shapeSize,
					Width = shapeSize,
					Fill = vertBrush
				};

				Canvas.SetLeft(ellipse, p.X * canvas.ActualWidth - shapeSize / 2);
				Canvas.SetTop(ellipse, p.Y * canvas.ActualHeight - shapeSize / 2);

				canvas.Children.Add(ellipse);

				// Draws point indexes in matrix
				// It's ugly, but that's only for tests
				Label labeldx = new Label
				{
#if _ONE_NUMBERING
					Content = Array.IndexOf(vertPositions, p) + 1
#endif
				};

				Canvas.SetLeft(labeldx, p.X * canvas.ActualWidth - shapeSize / 2);
				Canvas.SetTop(labeldx, p.Y * canvas.ActualHeight - shapeSize / 2);

				canvas.Children.Add(labeldx);
			}
		}

		public static void DrawEuler(GraphVisual graph, Canvas canvas)
		{
			Point[] vertPositions = graph.vertPositions;

			// Draws indexes in and Euler cycle
			var eulerIdxArray = graph.FindEulerPath();
			if (graph.IsEuler())
				for (int i = 0; i < eulerIdxArray.Count - 1; i++)
				{
					Label labelEuler = new Label
					{
#if _ONE_NUMBERING
						Content = i + 1,
#else
						Content = i,
#endif
						Foreground = Brushes.Red
					};

					int nextIdx = i + 1 < eulerIdxArray.Count ? i + 1 : 0;
					double posX = (vertPositions[eulerIdxArray[i]].X + vertPositions[eulerIdxArray[nextIdx]].X) / 2;
					double posY = (vertPositions[eulerIdxArray[i]].Y + vertPositions[eulerIdxArray[nextIdx]].Y) / 2;
					Canvas.SetLeft(labelEuler, posX * canvas.ActualWidth - 12);
					Canvas.SetTop(labelEuler, posY * canvas.ActualHeight - 12);

					canvas.Children.Add(labelEuler);
				}
		}
	}
}
