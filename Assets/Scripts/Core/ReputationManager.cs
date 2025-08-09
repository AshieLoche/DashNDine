using System;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class ReputationManager : SingletonBehaviour<ReputationManager>
    {
        public Action<int> OnReputationUpdateAction;

        [SerializeField] PlayerSO _playerSO;

        public void Start()
        {
            OnReputationUpdateAction?.Invoke(_playerSO.ReputationAmount);
        }
    }
}