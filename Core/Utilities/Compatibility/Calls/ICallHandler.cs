#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria.ModLoader;

namespace TomatoLib.Core.Utilities.Compatibility.Calls
{
    public interface ICallHandler
    {
        string Accessor { get; }

        object Action(Mod mod, params object[] args);
    }
}