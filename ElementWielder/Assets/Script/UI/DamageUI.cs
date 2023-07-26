using System.Collections;
using UnityEngine;
using Enemy;
using Core;

namespace UI
{
    public class DamageUI : MonoBehaviour
    {
        [SerializeField] private DamageUIText _textPrefab;

        [SerializeField] private Vector3 _offset;

        [SerializeField] private float _timerForDestroy;

        private void Awake()
        {
            Enemy.EnemyEventScriptable.enemyGetDamaged.AddListener(ShowDamage);
        }

        public void ShowDamage(ElementType damageElement, int damage, Vector3 position)
        {
            DamageUIText damageText = Instantiate(_textPrefab, this.transform);
            damageText.transform.position = position + _offset;
            damageText.SetText(damage);

            StartCoroutine(DestroyAfterTimer(damageText));
        }

        private IEnumerator DestroyAfterTimer(DamageUIText text)
        {
            yield return new WaitForSeconds(_timerForDestroy);
            Destroy(text.gameObject);
        }
    }
}