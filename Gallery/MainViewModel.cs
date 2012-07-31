using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BLL;
using Gallery.Base;
using Infrastructure;
using Models;

namespace Gallery
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly DelegateCommand<object> _openFileCommand;
        private readonly DelegateCommand<object> _removeCommand;
        private readonly DelegateCommand<object> _uploadCommand;
        private readonly DelegateCommand<object> _searchCommand;
        private INavigateManager navigator;
        private GalleryBLL _galleryBLL;

        public MainViewModel()
        {
            _openFileCommand = new DelegateCommand<object>(OpenFile);
            _removeCommand = new DelegateCommand<object>(CanRemove, RemoveFile);
            _uploadCommand = new DelegateCommand<object>(CanUpload, UploadFile);
            _searchCommand = new DelegateCommand<object>(Search);
            Images = new ObservableCollection<string>();
            _galleryBLL = GalleryBLL.GetBLL();
            Images.CollectionChanged += new NotifyCollectionChangedEventHandler(Images_CollectionChanged);
            //RemoveLists = new ObservableCollection<string>();
            navigator = new NavigateManager();
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand;
            }
        }

        public ICommand UploadCommand
        {
            get
            {
                return _uploadCommand;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand;
            }
        }

        public void Images_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _uploadCommand.NotifyCanExecuteChanged();
        }

        public ObservableCollection<GalleryModel> GalleryList
        {
            get
            {
                return GetValue(() => GalleryList);
            }
            set
            {
                SetValue(() => GalleryList, value);
            }
        }

        public ObservableCollection<string> Images
        {
            get
            {
                return GetValue(() => Images);
            }
            set
            {
                SetValue(() => Images, value);
            }
        }

        public IEnumerable<string> RemoveLists
        {
            get
            {
                return GetValue(() => RemoveLists);
            }
            set
            {
                SetValue(() => RemoveLists, value);
                _removeCommand.NotifyCanExecuteChanged();
            }
        }

        public void OpenFile(object parameter)
        {
            IEnumerable<string> files = navigator.OpenFiles();

            foreach (string name in files)
            {
                if (!Images.Contains(name))
                    Images.Add(name);
            }
        }

        public bool CanRemove(object parameter)
        {
            return RemoveLists != null && RemoveLists.Count() > 0 ? true : false;
        }

        public void RemoveFile(object parameter)
        {
            List<string> temp = new List<string>(RemoveLists);//直接遍历RemoveLists会因remove产生的索引更改而产生错误
            foreach (var item in temp)
            {
                Images.Remove(item);
            }
        }

        public bool CanUpload(object parameter)
        {
            return Images.Count > 0 ? true : false;
        }

        public void UploadFile(object parameter)
        {
            foreach (var path in Images)
            {
                GalleryModel gallery = new GalleryModel();
                string name, extName;
                ImageHelper.GetImageName(path, out name, out extName);
                gallery.ImageName = name;
                gallery.ExtName = extName;
                gallery.Pic = ImageHelper.GetThumbnail(new Bitmap(path));
                gallery.CreateTime = DateTime.Now;
                gallery.path = path;
                _galleryBLL.Save(gallery);
            }

            Images.Clear();
        }

        public void Search(object parameter)
        {
            IList<GalleryModel> list = _galleryBLL.FindAll();
            GalleryList = new ObservableCollection<GalleryModel>(list);
        }

        public void OpenPictureView(string fileName)
        {
            navigator.OpenPictureViewer(fileName, GalleryList.Select(o=>o.ImageName).ToList());
        }

    }
}