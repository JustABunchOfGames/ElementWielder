using Core;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class AttackManager: MonoBehaviour
    {
        [Header("Player for mana and CastPoint")]
        [SerializeField] private PlayerData _player;
        [SerializeField] private GameObject _castPoint;

        [Header("List of Attacks")]
        [SerializeField] private AttackListScriptable _listOfAttacks;

        public Dictionary<ElementType, AttackData> attacks { get; private set; }

        public Dictionary<ElementType, AttackCooldown> attacksCooldowns { get; private set; }

        private void Awake()
        {
            attacks = new Dictionary<ElementType, AttackData>();
            attacksCooldowns = new Dictionary<ElementType, AttackCooldown>();

            // Init with starting attacks
            foreach(AttackData data in _listOfAttacks.attackData)
            {
                AttackData attackData = new AttackData(data);
                attacks.Add(attackData.attackElement, attackData);

                attacksCooldowns.Add(attackData.attackElement, new AttackCooldown(attackData.attackCooldown));
            }
        }

        private void Start()
        {
            InputManager.attackEvent.AddListener(DoAttack);
        }

        private void DoAttack(ElementType element)
        {
            // Testing cooldown
            if (!attacksCooldowns[element].canCast)
                return;

            // Testing mana cost
            if (!_player.mana.UseMana(element, attacks[element].attackManaCost))
                return;

            // reset timer for cooldown
            attacksCooldowns[element].HasCast();

            // getting data
            AttackData data = attacks[element];

            // Creating attack
            GameObject attack = Instantiate(data.attackPrefab, _castPoint.transform.position, _castPoint.transform.rotation);
            Attack.CreateAttack(attack, data.attackElement, data.attackDamage);
        }

        private void Update()
        {
            foreach(AttackCooldown cooldown in attacksCooldowns.Values)
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

            public void ApplyCooldownUpgrade(float percentBonus)
            {
                cooldown *= ((100 + percentBonus) / 100);
            }
        }
    }
}