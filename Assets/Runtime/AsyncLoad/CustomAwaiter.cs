using System;
using System.Runtime.CompilerServices;

namespace Runtime.AsyncLoad
{
    //taken from: https://gist.github.com/Krumelur/39c44b45dfc658bb633c
    public class CustomAwaiter : INotifyCompletion
    {
        private Action _continuation;

        public bool IsCompleted
        {
            get;
            private set;
        }

        public void Complete()
        {
            IsCompleted = true;
            _continuation?.Invoke();
        }

        public void OnCompleted(Action continuation) => _continuation = continuation;
        public void GetResult() => Console.WriteLine("Done!");
        public CustomAwaiter GetAwaiter() => this;
        public void Dispose() => _continuation = null;
    }
}
