using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    [CreateAssetMenu(menuName ="Core/StageManager")]
    public class StageManagerScriptable : ScriptableObject
    {
        public int stage { get; private set; } = 1;

        private int _numberOfEnemyToKill;
        private int _numberOfEnemyKilled;

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

        public void Reset()
        {
            stage = 0;
        }

        public class StageClearedEvent : UnityEvent { }
    }
}