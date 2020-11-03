using System;
using System.Threading;

namespace HandleLab
{
    public class Mutex : IMutex
    {
        private const int unlockedState = -1;
        private static int acquirerThreadId => Thread.CurrentThread.ManagedThreadId;

        private int ownerThreadId = -1;

        public void Lock()
        {
            SpinWait spinWait = new SpinWait();
            while (Interlocked.CompareExchange(ref ownerThreadId, acquirerThreadId, unlockedState) != unlockedState)
                spinWait.SpinOnce();
        }

        public void Unlock()
        {
            if (Interlocked.CompareExchange(ref ownerThreadId, unlockedState, acquirerThreadId) != acquirerThreadId)
                throw new Exception();
        }
    }
}
