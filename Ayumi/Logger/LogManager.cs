using System;

namespace Ayumi.Logger {
    public class LogManager {
        readonly Object logServiceLock = new Object();
        ILogService internalLogService;

        public ILogService CurrentLogService {
            get {
                if (internalLogService == null)
                    lock (logServiceLock) {
                        internalLogService = GetCurrentConfiguredLogService();
                    }

                return internalLogService;
            }

            set {
                if (value == null)
                    throw new ArgumentNullException("CurrentLogService");

                lock (logServiceLock) {
                    internalLogService = value;
                }
            }
        }

        public ILogService GetCurrentConfiguredLogService() {
            // TODO: read app.config

            throw new NotImplementedException();
        }
    }
}