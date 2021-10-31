#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

namespace TomatoLib.Common.Utilities.Matching
{
    public interface IMatchCondition<in TMatcher>
    {
        bool Match(TMatcher matcher);
    }
}