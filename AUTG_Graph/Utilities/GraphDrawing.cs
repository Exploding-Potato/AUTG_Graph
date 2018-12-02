using AUTG_Graph.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AUTG_Graph
{
	static class GraphDrawing
	{
		public static void DrawGraph (GraphVisual graph, Canvas canvas, Brush vertBrush, Brush lineBrush, int shapeSize = 25, int lineWidth = 5)
		{
			canvas.Children.Clear();

			Point[] vertPositions = graph.vertPositions;

			// Draws edges
			foreach((int, int) e in graph.Edges())
			{
				Line line = new Line
				{
					X1 = vertPositions[e.Item1].X,
					Y1 = vertPositions[e.Item1].Y,
					X2 = vertPositions[e.Item2].X,
					Y2 = vertPositions[e.Item2].Y,
					Stroke = lineBrush
				};

				canvas.Children.Add(line);
			}

			// Draws vertices
			foreach (Point p in vertPositions)
			{
				Ellipse ellipse = new Ellipse();
				ellipse.Height = ellipse.Width = shapeSize;
				ellipse.Fill = vertBrush;
				
				Canvas.SetLeft(ellipse, p.X - shapeSize / 2);
				Canvas.SetTop(ellipse, p.Y - shapeSize / 2);

				canvas.Children.Add(ellipse);
			}
		}
	}
}
