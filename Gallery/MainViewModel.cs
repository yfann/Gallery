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
        private INavigateManager navigator;
        private ObservableCollection<string> _images;

        public MainViewModel()
        {
            navigator = new NavigateManager();
            _openFileCommand = new DelegateCommand<object>(OpenFile);
            //Images = new ObservableCollection<string>() { "test1", "test12" };
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand;
            }
        }

        public ObservableCollection<string> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                NotifyPropertyChanged<ObservableCollection<string>>(() => this.Images);
            }
        }

        public void OpenFile(object parameter)
        {
            IEnumerable<string> files = navigator.OpenFiles();
            if (Images == null)
            {
                Images = new ObservableCollection<string>(files);
            }
            else
            {
                foreach (string name in files)
                {
                    Images.Add(name);
                }
            }
        }
    }
}