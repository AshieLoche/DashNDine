using System.Collections.Generic;
using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class BaseListSO<T> : ScriptableObject where T : BaseSO
    {
        public List<T> SOList = new List<T>();

        public T GetSOByID(int id)
            => SOList.Find(e => e.ID == id);
    }
}