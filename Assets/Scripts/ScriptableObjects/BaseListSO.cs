using System.Collections.Generic;
using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class BaseListSO<TBaseSO> : ScriptableObject where TBaseSO : BaseSO
    {
        public List<TBaseSO> SOList = new List<TBaseSO>();

        public TBaseSO this[int index]
        {
            get { return SOList[index]; }
            set { SOList[index] = value; }
        }

        public int Count
            => SOList.Count;

        public void Add(TBaseSO baseSO)
        {
            if (SOList.Contains(baseSO))
            {
                int index = GetIndex(baseSO);
                SOList[index] = baseSO;
            }
            else
                SOList.Add(baseSO);
        }

        public int GetIndex(TBaseSO baseSO)
            => SOList.IndexOf(baseSO);

        public TBaseSO GetSOByID(int id)
            => SOList.Find(e => e.ID == id);
    }
}