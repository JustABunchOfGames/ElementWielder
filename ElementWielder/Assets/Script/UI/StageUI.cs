using Core;
using Player;
using UnityEngine;
using Upgrades;

namespace UI
{
    public class StageUI : MonoBehaviour
    {
        [Header("StageScreen")]
        [SerializeField] private GameObject _stageClearedScreen;
        [SerializeField] private GameObject _stageFailedScreen;

        [Header("Stop aiming and attacks")]
        [SerializeField] private InputManager _inputManager;

        [Header("StageManager")]
        [SerializeField] private StageManagerScriptable _stageManager;

        private void Awake()
        {
            StageManagerScriptable.stageClearedEvent.AddListener(StageCleared);
            PlayerHealth.deathEvent.AddListener(StageFailed);
            UpgradeManager.upgradeDoneEvent.AddListener(NextStage);
        }

        private void StageCleared()
        {
            Time.timeScale = 0f;

            _inputManager.enabled = false;

            _stageClearedScreen.SetActive(true);
        }

        private void StageFailed()
        {
            Time.timeScale = 0f;

            if (_inputManager != null)
                _inputManager.enabled = !_inputManager.enabled;

            _stageFailedScreen.SetActive(true);
        }

        private void NextStage()
        {
            _stageManager.NextStage();

            ResetUI();
        }

        private void ResetUI()
        {
            Time.timeScale = 1f;

            _inputManager.enabled = true;

            _stageClearedScreen.SetActive(false);
        }
    }
}