using UnityEngine;

namespace Attacks
{
    public class Projectile : AttackShape
    {
        [SerializeField] private float _speed;

        private new void Awake()
        {
            base.Awake();
            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;
        }

        private new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D (collision);

            StopCoroutine(DestroyAfterTime());
            Destroy(gameObject);
        }
    }
}