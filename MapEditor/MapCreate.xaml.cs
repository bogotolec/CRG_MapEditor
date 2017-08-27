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
    public partial class MapCreate : Window
    {
        MainWindow Sender;

        public MapCreate(MainWindow sender)
        {
            InitializeComponent();
            Sender = sender;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            int height, width;

            if (Landscape.SelectedItem == null)
            {
                MessageBox.Show("Landscape not choosed", "Error");
                return;
            }

            if (!int.TryParse(HeightField.Text, out height))
            {
                MessageBox.Show("Incorrect height", "Error");
                return;
            }

            if (!int.TryParse(WidthField.Text, out width))
            {
                MessageBox.Show("Incorrect width", "Error");
                return;
            }

            Landscape ls;

            switch(((TextBlock)Landscape.SelectedItem).Text)
            {
                case "Field":
                    ls = MapEditor.Landscape.Field;
                    break;
                case "Desert":
                    ls = MapEditor.Landscape.Desert;
                    break;
                case "Water":
                    ls = MapEditor.Landscape.Water;
                    break;
                case "Lava":
                    ls = MapEditor.Landscape.Lava;
                    break;
                case "Forest":
                    ls = MapEditor.Landscape.Forest;
                    break;
                case "Bricks":
                    ls = MapEditor.Landscape.Bricks;
                    break;
                case "Sign":
                    ls = MapEditor.Landscape.Sign;
                    break;
                default:
                    ls = MapEditor.Landscape.None;
                    break;
            }

            App.UsedMap = new Map(height, width, ls);
            MessageBox.Show("Map created succesfully!", "WOW");
            Sender.DrowMap();
            this.Close();
        }
    }
}
