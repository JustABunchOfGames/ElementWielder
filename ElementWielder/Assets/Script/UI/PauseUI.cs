using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
        private bool _optionsActive = false;

        // Called with from the PlayerInput
        public void ShowPauseMenu(InputAction.CallbackContext context)
        {
            if (context.started)
                ShowPauseMenu();
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
            Time.timeScale = (Time.timeScale == 1f) ? 0f : 1f;

            if (_inputManager != null)
                _inputManager.enabled = !_inputManager.enabled;

            ResetUINavigation();

            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
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