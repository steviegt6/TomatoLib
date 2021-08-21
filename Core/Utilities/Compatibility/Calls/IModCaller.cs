#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria.ModLoader;

namespace TomatoLib.Core.Utilities.Compatibility.Calls
{
    public interface IModCaller
    {
        object HandleCall(Mod mod, params object[] args);

        void AddCaller(ICallHandler handler);
    }
}