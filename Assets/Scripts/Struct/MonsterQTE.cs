using System;
using DashNDine.EnumSystem;

namespace DashNDine.StructSystem
{
    [Serializable]
    public struct MonsterQTEStruct
    {
        public MonsterDifficulty MonsterDifficulty;
        public int Count;
        public int Range;
        public int Reps;

        public MonsterQTEStruct(MonsterDifficulty monsterDifficulty, int count, int range, int reps)
        {
            MonsterDifficulty = monsterDifficulty;
            Count = count;
            Range = range;
            Reps = reps;
        }
    }
}