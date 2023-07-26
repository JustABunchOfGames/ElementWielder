using Core;
using Player;
using UnityEngine;

namespace Enemy {
    public class EnemySpawn : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private EnemyData _enemyPrefab;

        [Header("Infos to init enemies")]
        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;
        [SerializeField] private PlayerData _player;

        [Header("Position to Spawn")]
        [SerializeField] private Vector3 _spawnPosition;

        private void Awake()
        {
            _elementTypeScriptable.Init();
        }

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
            ElementType element = (ElementType)Random.Range(0, 4);

            enemy.SetData(element, _elementTypeScriptable.GetMaterialByElement(element), _player);
        }
    }
}