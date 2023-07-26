using Core;
using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {

        [SerializeField] private int _hp;
        private PlayerMana _mana;

        private void Awake()
        {
            _mana = new PlayerMana();
        }

        private void Start()
        {
            _mana.InitMana();
        }

        public void ReceiveMana(ElementType type, int value)
        {
            _mana.AddMana(type, value);
        }
    }
}