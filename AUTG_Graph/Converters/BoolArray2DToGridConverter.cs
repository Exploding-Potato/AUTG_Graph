using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AUTG_Graph.Converters
{
	class BoolArray2DToGridConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;


			bool[,] array = (bool[,]) value;

			uint rows = (uint) array.GetLength(0);
			uint cols = (uint) array.GetLength(1);

			Grid grid = new Grid();

			for (int i = 0; i < rows; i++)
			{
				RowDefinition newRowDefinition = new RowDefinition();
				newRowDefinition.MinHeight = 18;
				newRowDefinition.MaxHeight = 18;

				grid.RowDefinitions.Add(newRowDefinition);
			}

			for (int i = 0; i < cols; i++)
			{
				ColumnDefinition newColumnDefinition = new ColumnDefinition();
				newColumnDefinition.MinWidth = 18;
				newColumnDefinition.MaxWidth = 18;

				grid.ColumnDefinitions.Add(newColumnDefinition);
			}

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					if (i == j)
						continue;

					CheckBox checkBox = new CheckBox();
					checkBox.IsChecked = array[i, j];
					checkBox.IsEnabled = false;

					Grid.SetRow(checkBox, i);
					Grid.SetColumn(checkBox, j);
					
					//Binding binding = new Binding();
					//binding.Source = array[i, j];
					//binding.Mode = BindingMode.TwoWay;
					
					//myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

					//checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);

					grid.Children.Add(checkBox);
				}
			}

			return grid;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
