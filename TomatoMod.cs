using System;
using Terraria.ModLoader;
using TomatoLib.Core.Reflection;

namespace TomatoLib
{
    public class TomatoMod : Mod
    {
        public TomatoMod()
        {
            ExecutePrivately(() =>
            {
                ReflectionCache.Instance = new ReflectionCache();
            });
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