using Core;
using Player;
using UnityEngine;

namespace Enemy {
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyPrefab;
        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;

        [SerializeField] private Vector3 _spawnPosition;

        [SerializeField] private PlayerData _player;

        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                EnemyData enemy = Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity);

                RandomizeEnemy(enemy);
            }
        }

        private void RandomizeEnemy(EnemyData enemy)
        {
            ElementType element = (ElementType)Random.Range(0, 3);

            enemy.SetData(element, _elementTypeScriptable.GetMaterialByElement(element), _player);
        }
    }
}