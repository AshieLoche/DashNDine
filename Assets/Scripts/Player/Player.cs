using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerSO _playerSO;

        public PlayerSO GetPlayerSO()
            => _playerSO;
    }
}