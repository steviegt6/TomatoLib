using System;
using System.Collections.Generic;
using System.Reflection;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using Terraria.ModLoader;
using TomatoLib.Core.Compatibility.Calls;
using TomatoLib.Core.Drawing;
using TomatoLib.Core.Logging;
using TomatoLib.Core.Reflection;
using TomatoLib.Core.Utilities.Compatibility.Calls;
using TomatoLib.Core.Utilities.Logging;

namespace TomatoLib
{
    public class TomatoMod : Mod
    {
        public IModLogger ModLogger { get; protected set; }

        public virtual IModCaller CallHandler { get; protected set; } = new DefaultModCaller();

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

            // This is a temporary fix for some methods in HookHelper.
            ExecutePrivately(MonoModHooks.RequestNativeAccess);
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

        public override object Call(params object[] args) => CallHandler.HandleCall(this, args);

        private void ExecutePrivately(Action action)
        {
            if (GetType().Assembly.FullName?.StartsWith("TomatoLib, ") ?? false)
                action?.Invoke();
        }
    }
}