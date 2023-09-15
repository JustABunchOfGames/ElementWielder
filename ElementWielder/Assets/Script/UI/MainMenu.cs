using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Scene to load")]
        [SerializeField] private int _gameSceneIndex;
        [SerializeField] private int _optionsSceneIndex;

        [Header("For Tutorial")]
        [SerializeField] private StageManagerScriptable _stageManager;

        [Header("UINavigation")]
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }

        public void Tutorial()
        {
            _stageManager.isTutorial = true;
            SceneManager.LoadScene(_gameSceneIndex);
        }

        public void NewGame()
        {
            _stageManager.isTutorial = false;
            SceneManager.LoadScene(_gameSceneIndex);
        }

        public void Options()
        {
            _canvasGroup.interactable = false;

            SceneManager.LoadScene(_optionsSceneIndex, LoadSceneMode.Additive);

            SceneManager.sceneUnloaded += UnLoadOptionsScene;
        }

        private void UnLoadOptionsScene(Scene current)
        {
            _canvasGroup.interactable = true;

            EventSystem.current.SetSelectedGameObject(_firstSelected);

            SceneManager.sceneUnloaded -= UnLoadOptionsScene;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}