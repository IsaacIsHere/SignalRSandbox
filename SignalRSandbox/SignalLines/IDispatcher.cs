using System;
using System.Windows;

namespace SignalLines
{
    public interface IDispatcher
    {
        void Dispatch(Action action);
    }
    public class WpfDispatcher : IDispatcher
    {
        public void Dispatch(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}