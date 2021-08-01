using log4net;

namespace TomatoLib.Core.Utilities.Logging
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