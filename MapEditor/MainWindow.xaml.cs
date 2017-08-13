using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MapEditor
{
    public delegate void UpdateProgressBarDelegate(double value);

    public partial class MainWindow : Window
    {
        public UpdateProgressBarDelegate updProgress;
        private int X = 0, Y = 0;
        private int ChosenCellX = 0, ChosenCellY = 0;
        private List<Button> MapButtonList = new List<Button>();

        private string dir = Directory.GetCurrentDirectory();

        public MainWindow()
        {
            InitializeComponent();

            updProgress = new UpdateProgressBarDelegate(UpdateProgress);

            IsPassableCheckBox.Checked += IsPassableCheckBox_Checked;
            IsPassableCheckBox.Unchecked += IsPassableCheckBox_Unchecked;
            CellMessage.TextChanged += CellMessage_TextChanged;
            CellMessageEn.TextChanged += CellMessageEn_TextChanged;
            IsTaskableCheckbox.Checked += IsTaskableCheckbox_Checked;
            IsTaskableCheckbox.Unchecked += IsTaskableCheckbox_Unchecked;
            TaskIdTextbox.TextChanged += TaskIdTextbox_TextChanged;
            Landscape.SelectionChanged += Landscape_SelectionChanged;
        }

        #region Events
        private void CellMessageEn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            App.UsedMap[ChosenCellY, ChosenCellX].EnglishMessage = CellMessageEn.Text;
        }

        private void Landscape_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            switch (((TextBlock)Landscape.SelectedItem).Text)
            {
                case "Field":
                    App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId = MapEditor.Landscape.Field;
                    break;
                case "Desert":
                    App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId = MapEditor.Landscape.Desert;
                    break;
                case "Forest":
                    App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId = MapEditor.Landscape.Forest;
                    break;
                case "Water":
                    App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId = MapEditor.Landscape.Water;
                    break;
                case "Bricks":
                    App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId = MapEditor.Landscape.Bricks;
                    break;
                case "Lava":
                    App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId = MapEditor.Landscape.Lava;
                    break;
            }
            
            DrawCell(ChosenCellY, ChosenCellX);
        }

        private void TaskIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            if (!int.TryParse(TaskIdTextbox.Text, out App.UsedMap[ChosenCellY, ChosenCellX].TaskId))
            {
                TaskIdTextbox.Text = "0";
            }
        }

        private void IsTaskableCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            App.UsedMap[ChosenCellY, ChosenCellX].IsTaskable = false;
            TaskIdTextbox.IsEnabled = false;
        }

        private void IsTaskableCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            App.UsedMap[ChosenCellY, ChosenCellX].IsTaskable = true;
            TaskIdTextbox.IsEnabled = true;
        }

        private void CellMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            App.UsedMap[ChosenCellY, ChosenCellX].RussianMessage = CellMessage.Text;
        }

        private void IsPassableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            App.UsedMap[ChosenCellY, ChosenCellX].IsPassable = false;

            DrawCell(ChosenCellY, ChosenCellX);
        }

        private void IsPassableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (App.UsedMap == null)
                return;

            App.UsedMap[ChosenCellY, ChosenCellX].IsPassable = true;

            DrawCell(ChosenCellY, ChosenCellX);
        }
        #endregion

        private void InitFields()
        {
            if (App.UsedMap == null)
                return;

            CellMessage.Text = App.UsedMap[ChosenCellY, ChosenCellX].RussianMessage;
            CellMessageEn.Text = App.UsedMap[ChosenCellY, ChosenCellX].EnglishMessage;

            IsPassableCheckBox.IsChecked = App.UsedMap[ChosenCellY, ChosenCellX].IsPassable;

            if ((IsTaskableCheckbox.IsChecked = App.UsedMap[ChosenCellY, ChosenCellX].IsTaskable) == true)
            {
                TaskIdTextbox.IsEnabled = true;
                TaskIdTextbox.Text = App.UsedMap[ChosenCellY, ChosenCellX].TaskId.ToString();
            }
            else
            {
                TaskIdTextbox.IsEnabled = false;
            }

            switch (App.UsedMap[ChosenCellY, ChosenCellX].LandscapeId)
            {
                case MapEditor.Landscape.Field:
                    Landscape.SelectedIndex = 0;
                    break;
                case MapEditor.Landscape.Desert:
                    Landscape.SelectedIndex = 1;
                    break;
                case MapEditor.Landscape.Water:
                    Landscape.SelectedIndex = 2;
                    break;
                case MapEditor.Landscape.Bricks:
                    Landscape.SelectedIndex = 4;
                    break;
                case MapEditor.Landscape.Forest:
                    Landscape.SelectedIndex = 3;
                    break;
                case MapEditor.Landscape.Lava:
                    Landscape.SelectedIndex = 5;
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;

            string menuitemname = menuitem.Header.ToString();

            if (menuitemname == "Save")
            {
                if (App.UsedMap == null)
                {
                    MessageBox.Show("Map does not opened!", "Error", MessageBoxButton.OK);
                    return;
                }


                SaveFileDialog savefiledialog = new SaveFileDialog();

                savefiledialog.InitialDirectory = "map";
                savefiledialog.Filter = "CTF RPG GAME maps|*.crgm";
                savefiledialog.FilterIndex = 1;
                savefiledialog.RestoreDirectory = false;

                Nullable<bool> result = savefiledialog.ShowDialog();

                if (result == true)
                {
                    string filename = savefiledialog.FileName;

                    App.UsedMap.Save(filename);
                }
            }

            if (menuitemname == "Open")
            {
                OpenFileDialog openfiledialog = new OpenFileDialog();

                if (!Directory.Exists("map"))
                    Directory.CreateDirectory("map");

                openfiledialog.InitialDirectory = "map";
                openfiledialog.Filter = "CTF RPG GAME maps|*.crgm";
                openfiledialog.FilterIndex = 1;
                openfiledialog.RestoreDirectory = false;

                Progressbar.Visibility = Visibility.Visible;

                if (openfiledialog.ShowDialog() != null)
                {
                    try
                    {
                        Stream myStream = null;
                        if ((myStream = openfiledialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                App.UsedMap = new Map(myStream, this);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }

                Progressbar.Visibility = Visibility.Hidden;

                InitFields();
                DrowMap();
            }

            if (menuitemname == "New")
            {
                MapCreate MapCreation = new MapCreate(this);
                MapCreation.Show();
                InitFields();
            }
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "MapUp":
                    if (Y > 0)
                        Y--;
                    break;
                case "MapLeft":
                    if (X > 0)
                        X--;
                    break;
                case "MapRight":
                    if (X < App.UsedMap.Width - 11)
                        X++;
                    break;
                case "MapDown":
                    if (Y < App.UsedMap.Height - 11)
                        Y++;
                    break;
            }
            DrowMap();
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            ChosenCellX = X + Grid.GetColumn(btn);
            ChosenCellY = Y + Grid.GetRow(btn);

            CurrentCoordsLabel.Content = ChosenCellX.ToString() + ", " + ChosenCellY.ToString();

            InitFields();
        }

        public void Clear()
        {
            foreach(var b in MapButtonList)
            {
                MapDisplay.Children.Remove(b);
            }
            MapButtonList = new List<Button>();
        }

        private void UpdateProgress(double value)
        {
            Progressbar.SetValue(ProgressBar.ValueProperty, value);
        }

        private void DrawCell(int i, int j)
        {
            Button btn = new Button();
            Image image = new Image();

            Uri uri;


            switch (App.UsedMap[i, j].LandscapeId)
            {
                case MapEditor.Landscape.Field:
                    uri = new Uri(dir + @"\Textures\Field.png");
                    break;
                case MapEditor.Landscape.Desert:
                    uri = new Uri(dir + @"\Textures\Desert.png");
                    break;
                case MapEditor.Landscape.Bricks:
                    uri = new Uri(dir + @"\Textures\Bricks.png");
                    break;
                case MapEditor.Landscape.Water:
                    uri = new Uri(dir + @"\Textures\Water.png");
                    break;
                case MapEditor.Landscape.Forest:
                    uri = new Uri(dir + @"\Textures\Forest.png");
                    break;
                case MapEditor.Landscape.Lava:
                    uri = new Uri(dir + @"\Textures\Lava.png");
                    break;
                default:
                    uri = new Uri(dir + @"\Textures\Lava.png");
                    break;
            }

            BitmapImage bi = new BitmapImage(uri);


            btn.Height = 50;
            btn.Width = 50;
            
            image.Stretch = Stretch.Fill;
            image.Source = bi;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;

            Grid Buttongrid = new Grid();

            Buttongrid.Children.Add(image);

            if (!App.UsedMap[i, j].IsPassable)
            {
                Image nopass = new Image();
                nopass.Source = new BitmapImage(new Uri(dir + @"\Textures\NoPass.png"));

                nopass.Stretch = Stretch.None;

                nopass.HorizontalAlignment = HorizontalAlignment.Left;
                nopass.VerticalAlignment = VerticalAlignment.Top;

                Buttongrid.Children.Add(nopass);
            }

            btn.Content = Buttongrid;
            btn.Click += MapButton_Click;

            Grid.SetColumn(btn, j - X);
            Grid.SetRow(btn, i - Y);

            MapButtonList.Add(btn);
            MapDisplay.Children.Add(btn);
        }

        public void DrowMap()
        {
            if (App.UsedMap == null)
                return;

            Clear();

            for (int i = Y; i < Y + 10; i++)
            {
                for (int j = X; j < X + 10; j++)
                {
                    DrawCell(i, j);
                    continue;
                }
            }
        }
    }
}
