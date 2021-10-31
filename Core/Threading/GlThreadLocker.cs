#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria;
using TomatoLib.Common.Systems;

namespace TomatoLib.Core.Threading
{
    public sealed class GlThreadLocker : SingletonSystem<GlThreadLocker>
    {
        private int? MainThreadId;
        [CanBeNull] private List<Action> Actions;
        [CanBeNull] private LockProvider Lock;

        public override void Load()
        {
            base.Load();

            Actions = new List<Action>();

            On.Terraria.Main.Update += CallActionsOnMainThread;
        }

        private void CallActionsOnMainThread(On.Terraria.Main.orig_Update orig, Main self, GameTime gameTime)
        {
            // Create lock on main thread
            Lock ??= new LockProvider();
            MainThreadId ??= Thread.CurrentThread.ManagedThreadId;

            if (Actions != null)
            {
                foreach (Action action in Actions)
                    action?.Invoke();

                Actions.Clear();
            }

            orig(self, gameTime);
        }

        public async Task<T> InvokeAsync<T>(Func<T> task)
        {
            TaskCompletionSource<T> completionSource = new();

            if (MainThreadId.HasValue && Thread.CurrentThread.ManagedThreadId == MainThreadId.Value)
            {
                completionSource.SetResult(task());
                return await completionSource.Task;
            }

            if (Lock == null)
                throw new Exception("Lock object was null when attempting to invoke function in GlThreadLocker.");

            object lockObject = Lock.LockObject;
            lock (lockObject)
            {
                Actions?.Add(() =>
                {
                    try
                    {
                        completionSource.SetResult(task());
                    }
                    catch (Exception e)
                    {
                        completionSource.SetException(e);
                    }
                });
            }

            return await completionSource.Task;
        }
    }
}