using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Scene to load")]
        [SerializeField] private int _gameSceneIndex;
        [SerializeField] private int _optionsSceneIndex;

        public void NewGame()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }

        public void Options()
        {
            SceneManager.LoadScene(_optionsSceneIndex, LoadSceneMode.Additive);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}