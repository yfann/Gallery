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

        public PictureViewModel ViewModel
        {
            get
            {
                if (DataContext != null)
                {
                    return DataContext as PictureViewModel;
                }
                else
                {
                    throw new NullReferenceException("PictureViewModel 不能为null");
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                this.Close();
            }
        }

        private void viewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (e.Delta < 0)
            //{
            //    ViewModel.NextPicture();
            //}
            //else
            //{
            //    ViewModel.PreviousPicture();
            //}
            Point mouse=Mouse.GetPosition(this);
            Point imageOrigin = GetImageOriginPoint();
            this.scaleTrs.CenterX = mouse.X - imageOrigin.X;
            this.scaleTrs.CenterY = mouse.Y - imageOrigin.Y;
            scaleTrs.ScaleX += (double)e.Delta / 1000;
            scaleTrs.ScaleY += (double)e.Delta / 1000;
        }


        protected Point GetImageOriginPoint()
        {
            Point imageOrigin = new Point();
            double imgWidth=viewer.Source.Width;
            double imgHeight = viewer.Source.Height;
            if (imgHeight > imgWidth)
            {
                imageOrigin.X = (this.Width-imgWidth / imgHeight * this.Height)/2;
                imageOrigin.Y = 0;
            }
            else
            {
                imageOrigin.X = 0;
                imageOrigin.Y = (this.Height-imgHeight / imgWidth * this.Width)/2;
            }
            return imageOrigin;
        }
    }
}
