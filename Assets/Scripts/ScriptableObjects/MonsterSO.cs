using System.Collections.Generic;
using DashNDine.StructSystem;

namespace DashNDine.ScriptableObjectSystem
{
    public class MonsterSO : BaseSO
    {
        public List<MonsterQTE> MonsterQTEList = new List<MonsterQTE>();
        public string Note;
    }
}