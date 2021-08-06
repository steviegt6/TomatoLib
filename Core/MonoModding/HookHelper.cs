using System;
using System.Reflection;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;

namespace TomatoLib.Core.MonoModding
{
    /// <summary>
    ///     Provides tools for aiding MonoMod hook creation.
    /// </summary>
    public static class HookHelper
    {
        /// <summary>
        ///     Creates a <see cref="Delegate"/> that gets hooked into the <see cref="HookEndpointManager"/> through <see cref="HookEndpointManager.Modify"/>. <br />
        ///     Used for IL editing.
        /// </summary>
        /// <param name="method">The method to modify.</param>
        /// <param name="modifyingType">The type that contains the modifying method.</param>
        /// <param name="methodName">The name of the modifying method.</param>
        /// <param name="mod">The <see cref="TomatoMod"/> instance to add to.</param>
        public static void CreateEdit(MethodInfo method, Type modifyingType, string methodName, TomatoMod mod)
        {
            Delegate callback = Delegate.CreateDelegate(typeof(ILContext.Manipulator), modifyingType, methodName);
            HookEndpointManager.Modify(method, callback);
            mod.DelegatesToRemove.Add((method, callback));
        }

        /// <summary>
        ///     Creates a <see cref="Hook"/> that gets applied to the specified <paramref name="modifiedMethod"/>. <br />
        ///     Used for method detouring.
        /// </summary>
        /// <param name="modifiedMethod">The method to modify.</param>
        /// <param name="modifyingMethod">The method doing the modifying.</param>
        /// <param name="mod">The <see cref="TomatoMod"/> instance to add to.</param>
        public static void CreateDetour(MethodInfo modifiedMethod, MethodInfo modifyingMethod, TomatoMod mod)
        {
            Hook hook = new(modifiedMethod, modifyingMethod);
            hook.Apply();
            mod.HooksToRemove.Add(hook);
        }

        /// <summary>
        ///     Creates a <see cref="Delegate"/> that gets hooked into the <see cref="HookEndpointManager"/> through <see cref="HookEndpointManager.Modify"/>. <br />
        ///     Used for IL editing.
        /// </summary>
        /// <param name="method">The method to modify.</param>
        /// <param name="modifyingType">The type that contains the modifying method.</param>
        /// <param name="methodName">The name of the modifying method.</param>
        /// <param name="mod">The <see cref="TomatoMod"/> instance to add to.</param>
        public static void CreateEdit(this TomatoMod mod, MethodInfo method, Type modifyingType, string methodName) =>
            CreateEdit(method, modifyingType, methodName, mod);

        /// <summary>
        ///     Creates a <see cref="Hook"/> that gets applied to the specified <paramref name="modifiedMethod"/>. <br />
        ///     Used for method detouring..
        /// </summary>
        /// <param name="modifiedMethod">The method to modify.</param>
        /// <param name="modifyingMethod">The method doing the modifying.</param>
        /// <param name="mod">The <see cref="TomatoMod"/> instance to add to.</param>
        public static void CreateDetour(this TomatoMod mod, MethodInfo modifiedMethod, MethodInfo modifyingMethod) =>
            CreateDetour(modifiedMethod, modifyingMethod, mod);
    }
}