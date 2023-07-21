using Core;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyData : MonoBehaviour
    {
        [SerializeField] private ElementType _elementType;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private PlayerData _player;

        [SerializeField] private int _hp;
        [SerializeField] private int _mana;

        public void SetData(ElementType elementType, Material material, PlayerData player)
        {
            _elementType = elementType;
            _spriteRenderer.material = material;
            _player = player;
        }

        public void Damage(ElementType elementDamage, int damage)
        {
            _hp -= damage;

            if (_hp <= 0) 
            {
                _player.ReceiveMana(_elementType, _mana);

                Destroy(gameObject);
            }
        }
    }
}