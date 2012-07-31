using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gallery.Base
{
    public class NavigateManager : INavigateManager
    {
        public IEnumerable<string> OpenFiles()
        {
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.Multiselect = true;
            //op.Filter = "*.*";
            op.ShowDialog();
            return op.FileNames;
        }

        public void OpenPictureViewer(string fileName,IList<string> PicList)
        {
            PictureView view = new PictureView();
            view.DataContext = new PictureViewModel(fileName,PicList);
            view.ShowActivated=false;
            view.Show();
          
        }
    }
}