using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gallery.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged<T>(Expression<Func<T>> propertyName)
        {
            if (this.PropertyChanged != null)
            {
                var member = propertyName.Body as MemberExpression;
                if (member != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(member.Member.Name));
                }
            }
        }
    }
}