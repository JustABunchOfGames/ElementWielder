using Core;
using Player;
using System.Collections;
using UnityEngine;

namespace Enemy {
    public class EnemySpawn : MonoBehaviour
    {
        [Header("Scriptable for stage management")]
        [SerializeField] private StageManagerScriptable _stageManager;

        [Header("Prefab")]
        [SerializeField] private EnemyData _enemyPrefab;

        [Header("Infos to init enemies")]
        [SerializeField] private PlayerData _player;

        [Header("Position to Spawn")]
        [SerializeField] private float _spawnDistance;
        [SerializeField] private float _spawnBaseTimer;
        [SerializeField] private int _baseNumberOfEnemyToSpawn;

        private IEnumerator Start()
        {
            // Getting stage
            int stage = _stageManager.stage;

            // Calculate number of enemy to spawn + setting it in the stage manager
            int numberOfEnemyToSpawn = _baseNumberOfEnemyToSpawn + ((stage - 1) * ((int)stage / 10));
            _stageManager.SetNumberOfEnemyToKill(numberOfEnemyToSpawn);

            // Calculate the timer between each spawn
            float spawnTotalTimer = _spawnBaseTimer + stage;
            float spawnTimer = spawnTotalTimer / numberOfEnemyToSpawn;

            // Calculate bonus hp & speed for enemy
            int bonusHp = stage * (1 + (int)(stage / 10));
            float bonusSpeed = 1f + (stage * 0.005f);

            // Waiting a bit before starting stage spawn
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < numberOfEnemyToSpawn; i++)
            {
                // Wait between each spawn
                yield return new WaitForSeconds(spawnTimer);

                // Spawn at a certain distance from player
                Vector2 spawnPosition = Random.insideUnitCircle.normalized * _spawnDistance;
                EnemyData enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

                // Randomize Element
                ElementType element = (ElementType)Random.Range(0, 4);

                enemy.SetData(element, _player, bonusHp, bonusSpeed);
            }

            yield return null;
        }

        private void RandomizeEnemy(EnemyData enemy)
        {
            

            
        }
    }
}