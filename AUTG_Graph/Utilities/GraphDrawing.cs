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

			// Draws vertices
			foreach (Point p in vertPositions)
			{
				Ellipse ellipse = new Ellipse();
				ellipse.Height = ellipse.Width = shapeSize;
				ellipse.Fill = vertBrush;
				
				Canvas.SetLeft(ellipse, p.X);
				Canvas.SetTop(ellipse, p.Y);

				canvas.Children.Add(ellipse);
			}

			// Draws edges
			foreach((int, int) e in graph.Edges())
			{
				Line line = new Line();
				line.X1 = vertPositions[e.Item1].X;
				line.Y1 = vertPositions[e.Item1].Y;
				line.X2 = vertPositions[e.Item2].X;
				line.Y2 = vertPositions[e.Item2].Y;
				line.Fill = lineBrush;

				canvas.Children.Add(line);
			}
		}
	}
}
