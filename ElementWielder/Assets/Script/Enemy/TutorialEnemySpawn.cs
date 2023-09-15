using Core;
using Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemy
{
    public class TutorialEnemySpawn : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyPrefab;
        [SerializeField] private PlayerData _player;

        [SerializeField] private List<ElementType> _elementOrder;
        [SerializeField] private List<Vector3> _positionOrder;
        private int _index = -1;

        public void SpawnNext()
        {
            _index++;

            EnemyData enemy = Instantiate(_enemyPrefab, _positionOrder[_index], Quaternion.identity);

            enemy.SetData(_elementOrder[_index], _player, 0, 0f);
        }
    }
}