using System.Collections.Generic;
using DashNDine.StructSystem;

namespace DashNDine.ScriptableObjectSystem
{
    public class MonsterSO : BaseSO
    {
        public List<MonsterQTEStruct> MonsterQTEList = new List<MonsterQTEStruct>();
        public string Note;
    }
}