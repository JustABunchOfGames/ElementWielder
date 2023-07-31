using Core;
using UnityEngine;

namespace Player
{
    public class PlayerHitbox : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerData _player;

        public void GetDamaged(ElementType damageElement, int damage)
        {
            _player.health.ReduceHealth(damage);
        }
    }
}