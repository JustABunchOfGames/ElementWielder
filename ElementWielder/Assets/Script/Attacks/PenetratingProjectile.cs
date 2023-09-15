using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class PenetratingProjectile: AttackShape
    {
        [SerializeField] private float _speed;

        [Header("OnHit effect")]
        [SerializeField] private List<ElementEffect> _onHitEffects;

        private new void Awake()
        {
            base.Awake();
            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;
        }

        private new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D (collision);

            foreach (ElementEffect onHitEffect in _onHitEffects)
            {
                if (onHitEffect.element == _element)
                {
                    Instantiate(onHitEffect.effect, transform.position, transform.rotation);
                }
            }
        }

        public override void AddEffects(ElementType element)
        {
            base.AddEffects(element);
        }
    }
}