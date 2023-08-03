using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [Header("PlayerAim to stop raycast")]
        [SerializeField] private PlayerAim _playerAim;

        [Header("Menu")]
        [SerializeField] private GameObject _pauseMenu;

        public void ShowPauseMenu()
        {
            Time.timeScale = (Time.timeScale == 1f) ? 0f : 1f;

            if (_playerAim != null)
                _playerAim.enabled = !_playerAim.enabled;

            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
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
    }
}