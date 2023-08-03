using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    [CreateAssetMenu(menuName ="Core/StageManager")]
    public class StageManagerScriptable : ScriptableObject
    {
        public int stage { get; private set; } = 1;

        [SerializeField] private int _numberOfEnemyToKill;
        [SerializeField] private int _numberOfEnemyKilled;

        public static StageClearedEvent stageClearedEvent = new StageClearedEvent();

        public void SetNumberOfEnemyToKill(int value)
        {
            _numberOfEnemyToKill = value;
            _numberOfEnemyKilled = 0;
        }

        public void AddEnemyKilled()
        {
            _numberOfEnemyKilled++;

            if (_numberOfEnemyKilled == _numberOfEnemyToKill)
                stageClearedEvent.Invoke();
        }

        public void NextStage()
        {
            stage++;
        }

        public class StageClearedEvent : UnityEvent { }
    }
}