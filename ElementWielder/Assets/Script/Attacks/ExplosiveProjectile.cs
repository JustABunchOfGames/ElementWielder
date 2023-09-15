using Core;
using UnityEngine;

namespace Attacks
{
    public class ExplosiveProjectile : AttackShape
    {
        [SerializeField] private float _speed;

        [Header("Explosion")]
        [SerializeField] private Explosion _explosionPrefab;

        private new void Awake()
        {
            base.Awake();
            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;
        }

        private new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            AttackShape attack = Instantiate(_explosionPrefab, transform.position, transform.rotation);
            Attack.CreateAttack(attack, GetComponent<Attack>());

            StopCoroutine(DestroyAfterTime());
        }

        public override void AddEffects(ElementType element)
        {
            base.AddEffects(element);
        }
    }
}