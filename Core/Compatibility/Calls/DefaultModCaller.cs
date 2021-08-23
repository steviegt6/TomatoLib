#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Terraria.ModLoader;
using TomatoLib.Core.Utilities.Compatibility.Calls;

namespace TomatoLib.Core.Compatibility.Calls
{
    public class DefaultModCaller : IModCaller
    {
        protected Dictionary<string, ICallHandler> CallHandlers { get; } = new();

        public object HandleCall(Mod mod, params object[] args)
        {
            List<object> arguments = args.ToList();

            if (arguments[0] is not string call)
                throw new Exception("Unexpected starting call parameter. Must be a string.");

            arguments.RemoveAt(0);

            return CallHandlers[call.ToLower(CultureInfo.InvariantCulture)].Action(mod, arguments.ToArray());
        }

        public void AddCaller(ICallHandler handler) =>
            CallHandlers[handler.Accessor.ToLower(CultureInfo.InvariantCulture)] = handler;

        public static void AssertArguments(IEnumerable<object> objects, params Type[] types)
        {
            List<object> objectsList = objects.ToList();

            if (types.Where((t, i) => t != objectsList[i].GetType()).Any())
                throw new ArgumentException("Invalid type passed.");
        }
    }
}