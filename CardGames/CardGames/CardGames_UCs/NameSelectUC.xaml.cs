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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardGames.CardGames_UCs
{
    /// <summary>
    /// Interaction logic for NameSelectUC.xaml
    /// </summary>
    public partial class NameSelectUC : UserControl, IButtonClicked
    {
        public event ButtonClicked GameButtonClicked;
        public event GameStarting GameStarting;
        public NameSelectUC(int min, int max)
        {
            InitializeComponent();
            sNumPlay.Minimum = min;
            sNumPlay.Maximum = max;
        }
        private void Go_Click(object sender, RoutedEventArgs e)
        {
            List<string> names = null;

            names = spTextboxes.Children.OfType<TextBox>()
                   .Where(texbox => texbox.Name.StartsWith("tbxP"))
                   .Select(texbox => texbox.Text).ToList<string>();

            GameStarting?.Invoke(names);

        }

        private void NumberOfPlayers_Slider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spLabels.Children.Clear();
            spTextboxes.Children.Clear();

            for (int i = 0; i < sNumPlay.Value; i++)
            {
                spLabels.Children.Add(new Label()
                {
                    Content = $"Player{i + 1} Name:",
                    FontSize = 30,
                    FontFamily = new FontFamily("SimSun")
                });

                spTextboxes.Children.Add(new TextBox()
                {
                    Name = $"tbxP{i + 1}Name",
                    Text = $"Player{i + 1}",
                    FontSize = 24,
                    FontFamily = new FontFamily("SimSun"),
                    Background = new SolidColorBrush(Color.FromRgb(76, 127, 127)),
                    Margin = new Thickness(8)
                });
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            GameButtonClicked?.Invoke(((Button)sender).Name);
        }
    }
}
