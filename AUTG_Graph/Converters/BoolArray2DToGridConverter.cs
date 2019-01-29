#define _ONE_NUMBERING

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AUTG_Graph.Converters
{
	class BoolArray2DToGridConverter : IValueConverter
	{
		private bool[,] oldArray;
		private Grid grid;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;
			
			bool[,] array = (bool[,]) value;
			uint rows = (uint) array.GetLength(0);
			uint cols = (uint) array.GetLength(1);

			if (oldArray != array)
			{
				grid = new Grid();

				for (int i = 0; i < rows + 1; i++)
				{
					grid.RowDefinitions.Add(new RowDefinition
					{
						MinHeight = 18,
						MaxHeight = 18
					});
				}

				for (int i = 0; i < cols + 1; i++)
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition
					{
						MinWidth = 18,
						MaxWidth = 18
					});
				}
			}
			
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					if (i == j)
						continue;

					CheckBox checkBox = new CheckBox
					{
						IsChecked = array[i, j],
						IsEnabled = false
					};

					Grid.SetRow(checkBox, i + 1);
					Grid.SetColumn(checkBox, j + 1);

					grid.Children.Add(checkBox);
				}
			}

			for (int i = 1; i < rows + 1; i++)
			{
				Label label = new Label
				{
					Content = i.ToString(),
					FontSize = 8
				};

				Grid.SetRow(label, i);
				Grid.SetColumn(label, 0);

				grid.Children.Add(label);
			}

			for (int i = 1; i < cols + 1; i++)
			{
				Label label = new Label
				{
					Content = i.ToString(),
					FontSize = 8
				};

				Grid.SetRow(label, 0);
				Grid.SetColumn(label, i);

				grid.Children.Add(label);
			}

			Thickness margin = new Thickness(5);
			grid.Margin = margin;

			oldArray = array;
			return grid;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
