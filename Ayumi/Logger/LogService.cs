using System;

namespace Ayumi.Logger {
    public abstract class LogService : ILogService {
        public void Debug(Object message) {
            throw new NotImplementedException();
        }

        public void Debug(Object message, Exception exception) {
            throw new NotImplementedException();
        }

        public void Info(Object message) {
            throw new NotImplementedException();
        }

        public void Info(Object message, Exception exception) {
            throw new NotImplementedException();
        }

        public void Error(Object message) {
            throw new NotImplementedException();
        }

        public void Error(Object message, Exception exception) {
            throw new NotImplementedException();
        }
    }
}