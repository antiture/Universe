using System;
using System.ComponentModel;
using System.Reflection;

namespace Universe.Concepts
{
    // 基础概念类
    public abstract class Concept
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
    }

    // 级联事件参数类
    public class CascadeEventArgs : EventArgs
    {
        public string PropertyName { get; }
        public object Source { get; }
        public object Data { get; }

        public CascadeEventArgs(string propertyName, object source = null, object data = null)
        {
            PropertyName = propertyName;
            Source = source;
            Data = data;
        }
    }

    // 泛型级联概念类，处理级联事件传递
    public abstract class CascadeConcept : Concept, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CascadeEventArgs> CascadeEvent;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnCascadeEvent(new CascadeEventArgs(propertyName, this));
        }

        protected virtual void OnCascadeEvent(CascadeEventArgs e)
        {
            CascadeEvent?.Invoke(this, e);
        }

        // 自动订阅子对象的 PropertyChanged 事件
        protected void SubscribeToChildProperties()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var propType = property.PropertyType;
                if (typeof(CascadeConcept).IsAssignableFrom(propType))
                {
                    var child = property.GetValue(this) as CascadeConcept;
                    if (child != null)
                    {
                        child.CascadeEvent += (sender, e) =>
                        {
                            // 当子对象触发级联事件时，当前对象也触发级联事件
                            var newArgs = new CascadeEventArgs(e.PropertyName, e.Source ?? sender, e.Data);
                            OnCascadeEvent(newArgs);
                        };
                    }
                }
            }
        }
    }
}
