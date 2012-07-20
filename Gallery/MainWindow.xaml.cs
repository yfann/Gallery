using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infrastructure;

namespace Gallery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private MainViewModel ViewModel
        {
            get
            {
                if (DataContext != null)
                {
                    return (MainViewModel)DataContext;
                }
                else
                {
                    return null;
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            if (ViewModel != null && listbox.SelectedItems != null)
            {
                ViewModel.RemoveLists = GetList(listbox.SelectedItems);
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (var file in filePaths)
                {
                    if (!ViewModel.Images.Contains(file))
                    {
                        ViewModel.Images.Add(file);
                    }
                }
            }
        }

        public IEnumerable<string> GetList(IList list)
        {
            foreach (var i in list)
            {
                yield return i.ToString();
            }
        }
    }
}