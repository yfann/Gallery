using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> _propertyValues;

        public BaseModel()
        {
            _propertyValues = new Dictionary<string, object>();
        }

        public void SetValue<T>(Expression<Func<T>> propertyName, object value)
        {
            string property = GetPropertyName<T>(propertyName);
            object temp;
            if (_propertyValues.TryGetValue(property, out temp))
            {
                _propertyValues[property] = value;
            }
            else
            {
                _propertyValues.Add(property, value);
            }
            NotifyPropertyChanged(property);
        }

        public T GetValue<T>(Expression<Func<T>> propertyName)
        {
            string property = GetPropertyName<T>(propertyName);
            object o;
            if (_propertyValues.TryGetValue(property, out o) && o != null)
            {
                return (T)o;
            }
            else
            {
                return default(T);
            }
        }

        public string GetPropertyName<T>(Expression<Func<T>> propertyName)
        {
            var member = propertyName.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentNullException("属性名不能为空");
            }
            return member.Member.Name;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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