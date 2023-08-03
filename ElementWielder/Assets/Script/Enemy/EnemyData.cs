using Core;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyData : MonoBehaviour, IDamageable
    {
        [Header("Element & Material")]
        [SerializeField] private ElementType _elementType;
        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        // Player to pursue and give mana to
        private PlayerData _player;

        [Header("Stats")]
        [SerializeField] private int _hp;
        [SerializeField] private int _mana;
        [SerializeField] private int _atk;
        [SerializeField] private float _speed;

        [Header("Scriptable for events")]
        [SerializeField] private EnemyEventScriptable _enemyEvents;

        [Header("Scriptable for stage management")]
        [SerializeField] private StageManagerScriptable _stageManager;

        public void SetData(ElementType elementType, PlayerData player, int bonusHp, float bonusSpeed)
        {
            // Init Enemy stats / Material
            _elementType = elementType;
            _spriteRenderer.material = _elementTypeScriptable.GetMaterialByElement(_elementType);

            _hp += bonusHp;
            _speed *= bonusSpeed;

            // Save player
            _player = player;

            // Look at player and move towards it
            LookAtPlayer();

            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;
        }

        private void LookAtPlayer()
        {
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(_player.transform.position.y - transform.position.y, _player.transform.position.x - transform.position.x);

            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            // Rotate Enemy
            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        }

        public void GetDamaged(ElementType damageElement, int damage)
        {
            damage = (int)(damage * _elementTypeScriptable.GetElementBonus(damageElement, _elementType));

            _hp -= damage;

            _enemyEvents.InvokeEnemyGetDamaged(damageElement, damage, transform.position);

            if (_hp <= 0)
            {
                _player.mana.AddMana(_elementType, _mana);

                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamageable target = collision.GetComponent<IDamageable>();

            if (target != null)
            {
                target.GetDamaged(_elementType, _atk);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _stageManager.AddEnemyKilled();
        }
    }
}