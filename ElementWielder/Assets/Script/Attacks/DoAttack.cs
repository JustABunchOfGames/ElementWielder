using Core;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class DoAttack: MonoBehaviour
    {
        [Header("Player for mana and CastPoint")]
        [SerializeField] private PlayerData _player;
        [SerializeField] private GameObject _castPoint;

        [Header("List of Attacks")]
        [SerializeField] private AttackListScriptable _listOfAttacks;

        private Dictionary<ElementType, AttackData> _attacks;

        private Dictionary<ElementType, AttackCooldown> _attacksCooldown;

        private void Awake()
        {
            _attacks = new Dictionary<ElementType, AttackData>();
            _attacksCooldown = new Dictionary<ElementType, AttackCooldown>();

            foreach(AttackData data in _listOfAttacks.attackData)
            {
                _attacks.Add(data.attackElement, data);

                _attacksCooldown.Add(data.attackElement, new AttackCooldown(data.attackCooldown));
            }
        }

        private void Start()
        {
            AttackInputManager.attackEvent.AddListener(CreateAttack);
        }

        private void CreateAttack(ElementType damageElement)
        {
            // Testing cooldown
            if (!_attacksCooldown[damageElement].canCast)
                return;

            // Testing mana cost
            if (!_player.mana.UseMana(damageElement, _attacks[damageElement].attackManaCost))
                return;

            // reset timer for cooldown
            _attacksCooldown[damageElement].HasCast();

            // getting data
            AttackData data = _attacks[damageElement];

            // Creating attack
            GameObject attack = Instantiate(data.attackPrefab, _castPoint.transform.position, _castPoint.transform.rotation);
            Attack.CreateAttack(attack, data.attackElement, data.attackDamage);
        }

        private void Update()
        {
            foreach(AttackCooldown cooldown in _attacksCooldown.Values)
            {
                if (!cooldown.canCast)
                {
                    cooldown.PassTime();
                }
            }
        }

        public class AttackCooldown
        {
            public float cooldown { get; private set; }

            private float timer;

            public bool canCast { get; private set; }

            public AttackCooldown(float cooldown)
            {
                this.cooldown = cooldown;
                timer = 0;
                canCast = true;
            }

            public void HasCast()
            {
                timer = cooldown;
                canCast = false;
            }

            public void PassTime()
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = 0;
                    canCast = true;
                }
            }
        }
    }
}