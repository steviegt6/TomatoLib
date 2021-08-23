using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;
using TomatoLib.Core.Compatibility.Calls;
using TomatoLib.Core.Drawing;
using TomatoLib.Core.Localization;
using TomatoLib.Core.Logging;
using TomatoLib.Core.MonoModding;
using TomatoLib.Core.Reflection;
using TomatoLib.Core.Utilities.Compatibility.Calls;
using TomatoLib.Core.Utilities.Localization;
using TomatoLib.Core.Utilities.Logging;

namespace TomatoLib
{
    /// <summary>
    ///     Extends <see cref="Mod"/>'s capabilities.
    /// </summary>
    public class TomatoMod : Mod
    {
        /// <summary>
        ///     Extended-capability <see cref="ILog"/>.
        /// </summary>
        public virtual IModLogger ModLogger { get; protected set; }

        /// <summary>
        ///     Handles localization loading for your mod.
        /// </summary>
        public virtual ILocalizationLoader LocalizationLoader { get; } = new DefaultLocalizationLoader();

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

            ExecutePrivately(() =>
            {
                this.CreateDetour(typeof(LocalizationLoader).GetCachedMethod("Autoload"),
                    GetType().GetCachedMethod(nameof(AutoLoadLocalization)));
            });
        }

        public override void Unload()
        {
            base.Unload();

            LocalizationLoader.Unload();

            foreach ((MethodInfo method, Delegate callback) in DelegatesToRemove)
                HookEndpointManager.Unmodify(method, callback);

            foreach (Hook hook in HooksToRemove.Where(hook => hook.IsApplied))
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

        private static void AutoLoadLocalization(Action<Mod> orig, Mod mod)
        {
            orig(mod);

            if (mod is TomatoMod tomatoMod)
                tomatoMod.LocalizationLoader?.Load(mod);
        }

        private void ExecutePrivately(Action action)
        {
            if (GetType().Assembly.FullName?.StartsWith("TomatoLib, ") ?? false)
                action?.Invoke();
        }
    }
}