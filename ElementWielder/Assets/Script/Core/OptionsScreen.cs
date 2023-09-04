using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Core
{
    public class OptionsScreen : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        [SerializeField] private GameObject _firstObjectSelected;

        private void Awake()
        {
            ResetUINavigation();
        }

        private void ResetUINavigation()
        {
            EventSystem.current.SetSelectedGameObject(_firstObjectSelected);
        }

        public void Return()
        {
            SceneManager.UnloadSceneAsync(_sceneName);
        }
    }
}