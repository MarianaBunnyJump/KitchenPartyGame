using System;

namespace DefaultNamespace
{
    public interface IHasProgress
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
    }
}