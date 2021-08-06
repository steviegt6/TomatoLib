using System;
using System.Collections.Generic;
using System.Reflection;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using Terraria.ModLoader;
using TomatoLib.Core.Drawing;
using TomatoLib.Core.Logging;
using TomatoLib.Core.Reflection;
using TomatoLib.Core.Utilities.Logging;

namespace TomatoLib
{
    public class TomatoMod : Mod
    {
        public IModLogger ModLogger { get; protected set; }

        public List<(MethodInfo, Delegate)> DelegatesToRemove = new();
        public List<Hook> HooksToRemove = new();

        public TomatoMod()
        {
            ExecutePrivately(() =>
            {
                ReflectionCache.Instance = new ReflectionCache();
                GlowMaskRepository.Instance = new GlowMaskRepository();
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

            foreach ((MethodInfo method, Delegate callback) in DelegatesToRemove)
                HookEndpointManager.Unmodify(method, callback);

            foreach (Hook hook in HooksToRemove)
                hook.Undo();

            DelegatesToRemove.Clear();
            HooksToRemove.Clear();

            ExecutePrivately(() =>
            {
                ReflectionCache.Instance = null;

                GlowMaskRepository.Instance.RemoveGlowMasks();
                GlowMaskRepository.Instance = null;
            });
        }

        private void ExecutePrivately(Action action)
        {
            if (GetType().Assembly.FullName?.StartsWith("TomatoLib, ") ?? false)
                action?.Invoke();
        }
    }
}