#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;
using TomatoLib.Core.Implementation.Compatibility.Calls;
using TomatoLib.Core.Implementation.Drawing;
using TomatoLib.Core.Implementation.Localization;
using TomatoLib.Core.Implementation.Logging;
using TomatoLib.Core.Implementation.Reflection;
using TomatoLib.Core.Interfaces.Compatibility.Calls;
using TomatoLib.Core.Interfaces.Localization;
using TomatoLib.Core.Interfaces.Logging;

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

        private static bool DetouredAutoLoad;

        public override void Load()
        {
            base.Load();

            ModLogger = new ModLogger(Logger);

            // This is a temporary fix for some methods in HookHelper.
            ExecutePrivately(MonoModHooks.RequestNativeAccess);

            // Don't execute this privately to ensure the method is modified
            // if TomatoLib is referenced as a dependency but not loaded as a mod
            if (!DetouredAutoLoad)
                // ReSharper disable once StringLiteralTypo
                this.CreateDetour(typeof(LocalizationLoader).GetCachedMethod("Autoload"),
                    GetType().GetCachedMethod(nameof(AutoLoadLocalization)));
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
                // If a mod isn't depending on TomatoLib, it's their job to take care of this.
                ReflectionCache.Instance = null;

                GlowMaskRepository.Instance.RemoveGlowMasks();
                GlowMaskRepository.Instance = null;
            });
        }

        public override object Call(params object[] args) => CallHandler.HandleCall(this, args);

        private static void AutoLoadLocalization(Action<Mod> orig, Mod mod)
        {
            DetouredAutoLoad = true;

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