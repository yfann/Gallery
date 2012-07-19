using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gallery.Base;

namespace Gallery
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly DelegateCommand<object> _openFileCommand;
        private readonly DelegateCommand<object> _removeCommand;
        private INavigateManager navigator;

        public MainViewModel()
        {
            Images = new ObservableCollection<string>();
            navigator = new NavigateManager();
            _openFileCommand = new DelegateCommand<object>(OpenFile);
            _removeCommand = new DelegateCommand<object>(CanRemove, RemoveFile);
            RemoveLists = new ObservableCollection<string>();
            //Images = new ObservableCollection<string>() { "test1", "test12" };
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
    }
}