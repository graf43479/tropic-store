using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebUI.Models
{
    public class StopWatchSimpleModel
    {
        private Stopwatch timer;
        private ConcurrentQueue<string> messages;

        public StopWatchSimpleModel()
        {
            timer = Stopwatch.StartNew();
            messages = new ConcurrentQueue<string>();
        }

        public long ElapsedTime
        {
            get { return timer.ElapsedMilliseconds; }
        }

        public IEnumerable<string> Messages
        {
            get { return messages; }
        }

        public void AddMessage(string message)
        {

            messages.Enqueue(string.Format("{0} в потоке {1}", message, Thread.CurrentThread.ManagedThreadId));
        }

    }


}