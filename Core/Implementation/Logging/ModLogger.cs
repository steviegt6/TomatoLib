#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using log4net;
using log4net.Core;
using TomatoLib.Core.Interfaces.Logging;

namespace TomatoLib.Core.Implementation.Logging
{
    /// <summary>
    ///     Basic implementation of <see cref="IModLogger"/>.
    /// </summary>
    public class ModLogger : IModLogger
    {
        private ILog RealLogger { get; }

        public ModLogger(ILog realLogger)
        {
            RealLogger = realLogger;
        }

        public ILogger Logger => RealLogger.Logger;

        public void Debug(object message) => RealLogger.Debug(message);

        public void Debug(object message, Exception exception) => RealLogger.Debug(message, exception);

        public void DebugFormat(string format, params object[] args) => RealLogger.DebugFormat(format, args);

        public void DebugFormat(string format, object arg0) => RealLogger.DebugFormat(format, arg0);

        public void DebugFormat(string format, object arg0, object arg1) => RealLogger.DebugFormat(format, arg0, arg1);

        public void DebugFormat(string format, object arg0, object arg1, object arg2) =>
            RealLogger.DebugFormat(format, arg0, arg1, arg2);

        public void DebugFormat(IFormatProvider provider, string format, params object[] args) =>
            RealLogger.DebugFormat(provider, format, args);

        public void Info(object message) => RealLogger.Info(message);

        public void Info(object message, Exception exception) => RealLogger.Info(message, exception);

        public void InfoFormat(string format, params object[] args) => RealLogger.InfoFormat(format, args);

        public void InfoFormat(string format, object arg0) => RealLogger.InfoFormat(format, arg0);

        public void InfoFormat(string format, object arg0, object arg1) => RealLogger.InfoFormat(format, arg0, arg1);

        public void InfoFormat(string format, object arg0, object arg1, object arg2) =>
            RealLogger.InfoFormat(format, arg0, arg1, arg2);

        public void InfoFormat(IFormatProvider provider, string format, params object[] args) =>
            RealLogger.InfoFormat(provider, format, args);

        public void Warn(object message) => RealLogger.Warn(message);

        public void Warn(object message, Exception exception) => RealLogger.Warn(message, exception);

        public void WarnFormat(string format, params object[] args) => RealLogger.WarnFormat(format, args);

        public void WarnFormat(string format, object arg0) => RealLogger.WarnFormat(format, arg0);

        public void WarnFormat(string format, object arg0, object arg1) => RealLogger.WarnFormat(format, arg0, arg1);

        public void WarnFormat(string format, object arg0, object arg1, object arg2) =>
            RealLogger.WarnFormat(format, arg0, arg1, arg2);

        public void WarnFormat(IFormatProvider provider, string format, params object[] args) =>
            RealLogger.WarnFormat(provider, format, args);

        public void Error(object message) => RealLogger.Error(message);

        public void Error(object message, Exception exception) => RealLogger.Error(message, exception);

        public void ErrorFormat(string format, params object[] args) => RealLogger.ErrorFormat(format, args);

        public void ErrorFormat(string format, object arg0) => RealLogger.ErrorFormat(format, arg0);

        public void ErrorFormat(string format, object arg0, object arg1) => RealLogger.ErrorFormat(format, arg0, arg1);

        public void ErrorFormat(string format, object arg0, object arg1, object arg2) =>
            RealLogger.ErrorFormat(format, arg0, arg1, arg2);

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args) =>
            RealLogger.ErrorFormat(provider, format, args);

        public void Fatal(object message) => RealLogger.Fatal(message);

        public void Fatal(object message, Exception exception) => RealLogger.Fatal(message, exception);

        public void FatalFormat(string format, params object[] args) => RealLogger.FatalFormat(format, args);

        public void FatalFormat(string format, object arg0) => RealLogger.FatalFormat(format, arg0);

        public void FatalFormat(string format, object arg0, object arg1) => RealLogger.FatalFormat(format, arg0, arg1);

        public void FatalFormat(string format, object arg0, object arg1, object arg2) =>
            RealLogger.FatalFormat(format, arg0, arg1, arg2);

        public void FatalFormat(IFormatProvider provider, string format, params object[] args) =>
            RealLogger.FatalFormat(provider, format, args);

        public bool IsDebugEnabled => RealLogger.IsDebugEnabled;

        public bool IsInfoEnabled => RealLogger.IsInfoEnabled;

        public bool IsWarnEnabled => RealLogger.IsWarnEnabled;

        public bool IsErrorEnabled => RealLogger.IsErrorEnabled;

        public bool IsFatalEnabled => RealLogger.IsFatalEnabled;

        public void PatchFailure(string type, string method, string opCode, string value = null)
        {
            string log = $"[IL] Patch failure: {type}::{method} -> {opCode}";

            if (!string.IsNullOrEmpty(value))
                log += $" {value}";

            RealLogger.Warn(log);
        }
    }
}