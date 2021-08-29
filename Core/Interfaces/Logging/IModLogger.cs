#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using log4net;

namespace TomatoLib.Core.Interfaces.Logging
{
    /// <summary>
    ///     Interface providing extended capabilities from <see cref="ILog"/>.
    /// </summary>
    public interface IModLogger : ILog
    {
        /// <summary>
        ///     Logs an IL patch failure.
        /// </summary>
        void PatchFailure(string type, string method, string opCode, string value = null);
    }
}