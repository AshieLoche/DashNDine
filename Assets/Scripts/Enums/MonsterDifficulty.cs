using System;

namespace DashNDine.EnumSystem
{
    [Flags]
    public enum MonsterDifficulty
    {
        None = 0,
        Everything = 1 << 0,
        Easy = 1 << 1,
        Medium = 1 << 2,
        Hard = 1 << 3,
    }
}