using Core;
using UnityEngine;

namespace Attacks
{
    public class Attack : MonoBehaviour
    {
        private ElementType _attackElement;

        private int _attackDamage;

        public static void CreateAttack(GameObject where, ElementType attackElement, int attackDamage)
        {
            Attack attack = where.AddComponent<Attack>();

            attack._attackElement = attackElement;
            attack._attackDamage = attackDamage;
        }

        public void DoDamage(IDamageable target)
        {
            target.GetDamaged(_attackElement, _attackDamage);
        }
    }
}