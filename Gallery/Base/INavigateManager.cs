using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gallery.Base
{
    public interface INavigateManager
    {
        IEnumerable<string> OpenFiles();
        void OpenPictureViewer(string fileName,IList<string> PicList);
    }
}