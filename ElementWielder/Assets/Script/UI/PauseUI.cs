using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [Header("Stop aiming & attacks")]
        [SerializeField] private InputManager _inputManager;

        [Header("Menu")]
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _firstObjectSelected;

        [Header("Canvas Group to deactivate it")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("OptionsScene")]
        [SerializeField] private string _optionsSceneName;
        private bool _optionsActive;

        private void Start()
        {
            InputManager.pauseEvent.AddListener(ShowPauseMenu);
            _optionsActive = false;
        }

        // Can also be called from PauseButton
        public void ShowPauseMenu()
        {
            // OptionsScene loaded, unloading it
            if (_optionsActive)
            {
                SceneManager.UnloadSceneAsync(_optionsSceneName);
                return;
            }
            // OptionsScene not loaded, usual pause function

            if (_pauseMenu.activeSelf)
            {
                Time.timeScale = 1f;

                if (_inputManager != null)
                    _inputManager.PauseInput(false);

                ResetUINavigation();

                _pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;

                if (_inputManager != null)
                    _inputManager.PauseInput(true);

                ResetUINavigation();

                _pauseMenu.SetActive(true);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Options()
        {
            LoadOptionsScene();
        }

        private void LoadOptionsScene()
        {
            _optionsActive = true;
            _canvasGroup.interactable = false;

            SceneManager.LoadScene(_optionsSceneName, LoadSceneMode.Additive);

            SceneManager.sceneUnloaded += UnLoadOptionsScene;
        }

        private void UnLoadOptionsScene(Scene current)
        {
            _optionsActive = false;
            _canvasGroup.interactable = true;

            ResetUINavigation();
            SceneManager.sceneUnloaded -= UnLoadOptionsScene;
        }

        private void ResetUINavigation()
        {
            EventSystem.current.SetSelectedGameObject(_firstObjectSelected);
        }
    }
}