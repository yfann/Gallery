using System;
using System.Collections;
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
        private readonly DelegateCommand<object> _setTagCommand;
        private INavigateManager navigator;
        private GalleryBLL _galleryBLL;

        public MainViewModel()
        {
            _openFileCommand = new DelegateCommand<object>(OpenFile);
            _removeCommand = new DelegateCommand<object>(CanRemove, RemoveFile);
            _uploadCommand = new DelegateCommand<object>(CanUpload, UploadFile);
            _searchCommand = new DelegateCommand<object>(Search);
            _setTagCommand = new DelegateCommand<object>(SetTag);
            UploadList = new ObservableCollection<GalleryModel>();
            _galleryBLL = GalleryBLL.GetBLL();
            UploadList.CollectionChanged += new NotifyCollectionChangedEventHandler(UploadList_CollectionChanged);
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

        public ICommand SetTagCommand
        {
            get
            {
                return _setTagCommand;
            }
        }

        public void UploadList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        public ObservableCollection<GalleryModel> UploadList
        {
            get
            {
                return GetValue(() => UploadList);
            }
            set
            {
                SetValue(() => UploadList, value);
            }
        }

        public string Tags
        {
            get
            {
                return GetValue(() => Tags);
            }
            set
            {
                SetValue(() => Tags, value);
            }
        }

        public IList RemoveLists
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
                if (!UploadList.Where(o => Object.Equals(o.path, name)).Any())
                {
                    UploadList.Add(new GalleryModel { path = name });
                }
            }
        }

        public bool CanRemove(object parameter)
        {
            return RemoveLists != null && RemoveLists.Count > 0 ? true : false;
        }

        public void RemoveFile(object parameter)
        {
            List<string> list = new List<string>();
            foreach (var item in RemoveLists)
            {
                GalleryModel gm = item as GalleryModel;
                if (gm != null)
                {
                    list.Add(gm.path);
                }
            }

            foreach (var item in list)
            {
                GalleryModel g = UploadList.Where(o => object.Equals(o.path, item)).First();
                UploadList.Remove(g);
            }
        }

        public bool CanUpload(object parameter)
        {
            return UploadList.Count > 0 ? true : false;
        }

        public void UploadFile(object parameter)
        {
            foreach (var gm in UploadList)
            {
                string name, extName;
                ImageHelper.GetImageName(gm.path, out name, out extName);
                gm.ImageName = name;
                gm.ExtName = extName;
                gm.Pic = ImageHelper.GetThumbnail(new Bitmap(gm.path));
                gm.CreateTime = DateTime.Now;
                _galleryBLL.Save(gm);
            }

            UploadList.Clear();
        }

        public void Search(object parameter)
        {
            IList<GalleryModel> list = _galleryBLL.FindAll();
            GalleryList = new ObservableCollection<GalleryModel>(list);
        }

        public void SetTag(object parameter)
        {
            if (!string.IsNullOrEmpty(Tags))
            {
                string[] temp = Tags.Split(' ');
                List<string> list = new List<string>(temp);
                foreach (var item in UploadList)
                {
                    item.Tags = list;
                }
            }
            UploadList = UploadList;
        }

        public void OpenPictureView(string fileName)
        {
            navigator.OpenPictureViewer(fileName, GalleryList.Select(o => o.ImageName).ToList());
        }
    }
}