using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gallery
{
    /// <summary>
    /// Interaction logic for PictureView.xaml
    /// </summary>
    public partial class PictureView : Window
    {
        public PictureView()
        {
            InitializeComponent();
            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;
        }

        private void viewer_MouseEnter(object sender, MouseEventArgs e)
        {
            tools.Visibility = Visibility.Visible;
        }

        private void viewer_MouseLeave(object sender, MouseEventArgs e)
        {
            tools.Visibility = Visibility.Collapsed;
        }

        private void viewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                this.Close();
            }
        }

    }
}
