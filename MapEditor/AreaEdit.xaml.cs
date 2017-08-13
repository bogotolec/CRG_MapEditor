using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// Interaction logic for AreaEdit.xaml
    /// </summary>
    public partial class AreaEdit : Window
    {
        public AreaEdit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int xb, xe, yb, ye;

            try
            {
                xb = int.Parse(LTX.Text);
                xe = int.Parse(RBX.Text);
                yb = int.Parse(LTY.Text);
                ye = int.Parse(RBY.Text);

                if (xb < 0 || yb < 0 || ye >= App.UsedMap.Height || xe > App.UsedMap.Width || xb > xe || yb > ye)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                MessageBox.Show("Incorrect coords!", "Error", MessageBoxButton.OK);
                return;
            }

            if (Landscape.SelectedItem == null)
            {
                MessageBox.Show("You not selected landscape", "Error", MessageBoxButton.OK);
            }

            Landscape land = MapEditor.Landscape.Field;

            switch (((TextBlock)Landscape.SelectedItem).Text)
            {
                case "Field":
                    land = MapEditor.Landscape.Field;
                    break;
                case "Desert":
                    land = MapEditor.Landscape.Desert;
                    break;
                case "Forest":
                    land = MapEditor.Landscape.Forest;
                    break;
                case "Water":
                    land = MapEditor.Landscape.Water;
                    break;
                case "Bricks":
                    land = MapEditor.Landscape.Bricks;
                    break;
                case "Lava":
                    land = MapEditor.Landscape.Lava;
                    break;
            }

            for (int i = yb; i <= ye; i++)
            {
                for (int j = xb; j <= xe; j++)
                {
                    App.UsedMap[i, j].LandscapeId = land;
                    App.UsedMap[i, j].IsPassable = (IsPassableCheckbox.IsChecked == true ? true : false);
                    App.UsedMap[i, j].EnglishMessage = EnglishMessageTextBox.Text;
                    App.UsedMap[i, j].RussianMessage = RussianMessageTextBox.Text;
                }
            }

            MessageBox.Show("Succes", "WOW", MessageBoxButton.OK);

            this.Close();
        }
    }
}
