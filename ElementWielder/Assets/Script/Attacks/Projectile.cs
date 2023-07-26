using Core;
using Enemy;
using System.Collections;
using UnityEngine;

namespace Attacks
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private int _damage = 2;
        [SerializeField] private ElementType _elementType;

        private void Awake()
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;

            StartCoroutine(DestroyAfterTime());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyData enemy = collision.GetComponent<EnemyData>();

            if (enemy != null)
            {
                enemy.GetDamaged(_elementType, _damage);
            }

            StopCoroutine(DestroyAfterTime());
            Destroy(gameObject);
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(3f);

            Destroy(gameObject);
        }
    }
}