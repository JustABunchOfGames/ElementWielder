using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Attacks
{
    public class AttackInputManager : MonoBehaviour
    {
        public static AttackEvent attackEvent;

        private void Awake()
        {
            attackEvent = new AttackEvent();
        }

        private void Update()
        {
            if (Input.GetButton("BasicAttack"))
                attackEvent.Invoke(ElementType.None);

            if (Input.GetButton("FireAttack"))
                attackEvent.Invoke(ElementType.Fire);

            if (Input.GetButton("WindAttack"))
                attackEvent.Invoke(ElementType.Wind);

            if (Input.GetButton("EarthAttack"))
                attackEvent.Invoke(ElementType.Earth);

            if (Input.GetButton("WaterAttack"))
                attackEvent.Invoke(ElementType.Water);
        }
    }

    public class AttackEvent : UnityEvent<ElementType> { }
}