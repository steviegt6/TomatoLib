#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Terraria.ModLoader;

namespace TomatoLib.Common.Systems
{
    /// <summary>
    ///     Indicates a singleton-based <see cref="ModSystem"/> with a provided <see cref="Instance"/> property.
    /// </summary>
    /// <typeparam name="TThis">The <see cref="Instance"/>'s type. Should be itself.</typeparam>
    public abstract class SingletonSystem<TThis> : ModSystem
    {
        public static TThis Instance { get; private set; }

        protected SingletonSystem()
        {
            if (this is TThis self)
                Instance = self;
        }
    }
}