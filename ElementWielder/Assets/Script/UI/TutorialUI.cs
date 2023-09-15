using Core;
using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class TutorialUI : MonoBehaviour
    {
        [System.Serializable]
        public class TutorialText
        {
            [TextArea]
            public string text;
            public bool instantiateEnemy;
        }

        [SerializeField] private StageManagerScriptable _stageManager;

        [SerializeField] private List<TutorialText> _tutorialText = new List<TutorialText>();
        private int _tutorialIndex = -1;

        [SerializeField] private GameObject _tutorialButton;
        [SerializeField] private Text _textArea;

        [SerializeField] private GameObject _spawner;
        private TutorialEnemySpawn _tutorialEnemySpawn;
        private EnemySpawn _enemySpawn;

        public void NextText()
        {
            _tutorialIndex++;
            if (_tutorialIndex < _tutorialText.Count)
            {
                _textArea.text = _tutorialText[_tutorialIndex].text;
                if (_tutorialText[_tutorialIndex].instantiateEnemy)
                    _tutorialEnemySpawn.SpawnNext();
            }
            // Return to Main Menu
            else
                SceneManager.LoadScene(0);
        }

        private void Awake()
        {
            if (_stageManager == null || !_stageManager.isTutorial)
            {
                gameObject.SetActive(false);
                return;
            }

            _enemySpawn = _spawner.GetComponent<EnemySpawn>();
            _enemySpawn.enabled = false;

            _tutorialEnemySpawn = _spawner.GetComponent<TutorialEnemySpawn>();

            NextText();

            EventSystem.current.SetSelectedGameObject(_tutorialButton);
        }
    }
}