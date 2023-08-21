using Core;
using UnityEngine;

namespace Attacks
{
    [System.Serializable]
    public class AttackData
    {
        [field :SerializeField] public ElementType attackElement { get; private set; }

        [field: SerializeField] public int attackDamage { get; private set; }
        private float _attackRaw;
        private float _attackPercentMultiplier;

        [field: SerializeField] public GameObject attackPrefab { get; private set; }

        [field: SerializeField] public float attackCooldown { get; private set; }

        [field: SerializeField] public int attackManaCost { get; private set; }
        private float _manaCostRaw;
        private float _manaCostPercentMultiplier;

        public AttackData(AttackData data)
        {
            attackElement = data.attackElement;
            attackDamage = data.attackDamage;
            attackPrefab = data.attackPrefab;
            attackCooldown = data.attackCooldown;
            attackManaCost = data.attackManaCost;

            _attackRaw = attackDamage;
            _attackPercentMultiplier = 100f;

            _manaCostRaw = attackManaCost;
            _manaCostPercentMultiplier = 100f;
        }

        public void AddAttackBonus(int attackBonus, float attackPercentMultiplier)
        {
            _attackRaw += attackBonus;
            _attackPercentMultiplier += attackPercentMultiplier;

            attackDamage = (int)(_attackRaw * (_attackPercentMultiplier / 100f));
        }

        public void AddManaCostBonus(int manaCostBonus, float manaCostPercentMultiplier) 
        { 
            _manaCostRaw += manaCostBonus;
            _manaCostPercentMultiplier += manaCostPercentMultiplier;

            attackManaCost = (int)(_manaCostRaw * (_attackPercentMultiplier / 100f));
        }

        public void SetPrefab(GameObject prefab)
        {
            attackPrefab = prefab;
        }
    }
}