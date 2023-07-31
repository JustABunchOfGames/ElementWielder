using Core;
using UnityEngine;

namespace Attacks
{
    [System.Serializable]
    public class AttackData
    {
        [field :SerializeField] public ElementType attackElement { get; private set; }

        [field: SerializeField] public int attackDamage { get; private set; }

        [field: SerializeField] public GameObject attackPrefab { get; private set; }

        [field: SerializeField] public float attackCooldown { get; private set; }

        [field: SerializeField] public int attackManaCost { get; private set; }
    }
}