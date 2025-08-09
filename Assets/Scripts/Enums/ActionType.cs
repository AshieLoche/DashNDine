using System;

namespace DashNDine.EnumSystem
{
    [Flags]
    public enum ActionType
    {
        None = 0,
        Everything = 1 << 0,
        Accept = 1 << 1,
        Leave = 1 << 2,
        Give = 1 << 3,
        Cook = 1 << 4
    }
}