using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace eBayPublisher
{
    public abstract class FileProcessor
    {
        #region Fields

        private readonly Queue<string> _files = new Queue<string>();
        private Thread _thread;
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(true);
        private static readonly object _lockObject = new object();
        private readonly int _maximumProcessRetries;
        private readonly int _delayBeforeRetry;

        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        // http://msdn.microsoft.com/en-us/library/7a2f3ay4(VS.80).aspx
        private volatile bool _shouldStop = false;

        #endregion

        #region Constructor(s)

        protected FileProcessor(int maximumRetries, int delay)
        {
            this._maximumProcessRetries = maximumRetries;
            this._delayBeforeRetry = delay;
        }

        protected FileProcessor()
            : this(5, 5000)
        { }

        #endregion

        #region Helper methods

        private static bool IsFileUploadComplete(string path)
        {
            try
            {
                using (File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        private void ProcessFile(string path)
        {
            // Check if the file has been completely created / uploaded
            int attempts = 0;
            while (true)
            {
                if (IsFileUploadComplete(path))
                {
                    // Process the file
                    ParseFile(path);
                    break;
                }
                attempts += 1;
                if (attempts >= this._maximumProcessRetries)
                {
                    // Log the error and send out notifications
                    /// ...etc.
                    LogFailedParseAttempt(path);
                    break;
                }
                Thread.Sleep(this._delayBeforeRetry);
            }

            // Delete the file after it has been processed
            File.Delete(path);
        }

        private void Work()
        {
            while (!_shouldStop)
            {
                string path = String.Empty;
                lock (_lockObject)
                {
                    if (_files.Count > 0)
                    {
                        path = _files.Dequeue();
                    }
                }

                if (!String.IsNullOrEmpty(path))
                {
                    // Process the file
                    ProcessFile(path);
                }
                else
                {
                    // If no files are left to process then wait
                    _waitHandle.WaitOne();
                }
            }
        }

        #endregion

        #region Methods

        public void EnqueueFile(string path)
        {
            // Queue the file
            lock (_lockObject)
            {
                _files.Enqueue(path);
            }

            // Initialize and start the worker thread when the first file is queued
            // or when it has been stopped and thus terminated.
            if (_thread == null || _shouldStop)
            {
                _thread = new Thread(new ThreadStart(Work));
                _thread.Start();
            }
                // If the thread is waiting then start it
            else if (_thread.ThreadState == ThreadState.WaitSleepJoin)
            {
                _waitHandle.Set();
            }
        }

        public void StopProcessing()
        {
            _shouldStop = true;
        }

        public abstract void ParseFile(string path);

        public abstract void LogFailedParseAttempt(string path);

        #endregion
    }
}