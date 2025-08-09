using DashNDine.EnumSystem;

namespace DashNDine.StructSystem
{
    public struct MonsterQTE
    {
        public MonsterDifficulty MonsterDifficulty;
        public int Count;
        public int Range;
        public int Reps;

        public MonsterQTE(MonsterDifficulty monsterDifficulty, int count, int range, int reps)
        {
            MonsterDifficulty = monsterDifficulty;
            Count = count;
            Range = range;
            Reps = reps;
        }
    }
}