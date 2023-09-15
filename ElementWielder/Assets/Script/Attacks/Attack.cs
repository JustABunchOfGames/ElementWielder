using Core;
using UnityEngine;

namespace Attacks
{
    public class Attack : MonoBehaviour
    {
        private ElementType _attackElement;

        private int _attackDamage;

        public static void CreateAttack(AttackShape attackShape, ElementType element, int attackDamage)
        {
            Attack attack = attackShape.gameObject.AddComponent<Attack>();

            attack._attackElement = element;
            attack._attackDamage = attackDamage;

            attackShape.AddEffects(element);
        }

        public static void CreateAttack(AttackShape attackShape, Attack existingAttack)
        {
            Attack attack = attackShape.gameObject.AddComponent<Attack>();

            attack._attackElement = existingAttack._attackElement;
            attack._attackDamage = existingAttack._attackDamage;

            attackShape.AddEffects(existingAttack._attackElement);
        }

        public void DoDamage(IDamageable target)
        {
            target.GetDamaged(_attackElement, _attackDamage);
        }
    }
}