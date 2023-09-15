using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class AttackShape : MonoBehaviour
    {
        [Serializable]
        public struct ElementEffect
        {
            public ElementType element;
            public GameObject effect;
        }

        [Header("Cast effect")]
        [SerializeField] protected List<ElementEffect> _castEffects;
        protected ElementType _element;

        [Header("Lifetime of attack")]
        [SerializeField] private float _lifetime;

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
            yield return new WaitForSeconds(_lifetime);

            Destroy(gameObject);
        }

        public virtual void AddEffects(ElementType element)
        {
            _element = element;

            foreach (ElementEffect castEffect in _castEffects)
            {
                if (castEffect.element == element)
                {
                    Instantiate(castEffect.effect, transform);
                }
            }
        }
    }
}