using System;
using Terraria.ModLoader;
using TomatoLib.Core.Logging;
using TomatoLib.Core.Reflection;
using TomatoLib.Core.Utilities.Logging;

namespace TomatoLib
{
    public class TomatoMod : Mod
    {
        public IModLogger ModLogger { get; protected set; }

        public TomatoMod()
        {
            ExecutePrivately(() =>
            {
                ReflectionCache.Instance = new ReflectionCache();
            });
        }

        public override void Load()
        {
            base.Load();

            ModLogger = new ModLogger(Logger);
        }

        public override void Unload()
        {
            base.Unload();

            ExecutePrivately(() =>
            {
                ReflectionCache.Instance = null;
            });
        }

        private void ExecutePrivately(Action action)
        {
            if (GetType().Assembly.FullName?.StartsWith("TomatoLib, ") ?? false)
                action?.Invoke();
        }
    }
}