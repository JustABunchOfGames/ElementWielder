using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [CreateAssetMenu(menuName ="Enemy/Event")] 
    public class EnemyEventScriptable : ScriptableObject
    {
        public static EnemyGetDamagedEvent enemyGetDamaged = new EnemyGetDamagedEvent();

        public void InvokeEnemyGetDamaged(ElementType damageElement, int damage, Vector3 position)
        {
            if (enemyGetDamaged != null)
                enemyGetDamaged.Invoke(damageElement, damage, position);
        }
    }

    [System.Serializable]
    public class EnemyGetDamagedEvent : UnityEvent<ElementType, int, Vector3> { }
}