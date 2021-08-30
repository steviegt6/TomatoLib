#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

namespace TomatoLib.Core.Threading
{
    public sealed class LockProvider
    {
        public readonly object LockObject = new();
    }
}