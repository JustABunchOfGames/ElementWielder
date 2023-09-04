using Core;
using System.Collections;
using UnityEngine;

namespace Attacks
{
    public class AttackShape : MonoBehaviour
    {
        [SerializeField] private float _attackTimer;

        protected void Awake()
        {
            StartCoroutine(DestroyAfterTime());
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            IDamageable target = collision.GetComponent<IDamageable>();

            if (target != null)
            {
                GetComponent<Attack>().DoDamage(target);
            }
        }

        protected IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(_attackTimer);

            Destroy(gameObject);
        }
    }
}