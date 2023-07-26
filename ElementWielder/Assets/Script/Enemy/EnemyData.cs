using Core;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyData : MonoBehaviour
    {
        // Enemy Element Type
        [SerializeField] private ElementType _elementType;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        // Player to pursue and give mana to
        private PlayerData _player;

        [Header("Stats")]
        [SerializeField] private int _hp;
        [SerializeField] private int _mana;

        // Events
        [SerializeField] private EnemyEventScriptable _enemyEvents;

        public void SetData(ElementType elementType, Material material, PlayerData player)
        {
            _elementType = elementType;
            _spriteRenderer.material = material;
            _player = player;
        }

        public void GetDamaged(ElementType damageElement, int damage)
        {
            _hp -= damage;

            _enemyEvents.InvokeEnemyGetDamaged(damageElement, damage, transform.position);

            if (_hp <= 0)
            {
                _player.ReceiveMana(_elementType, _mana);

                Destroy(gameObject);
            }
        }
    }
}