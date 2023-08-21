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

        [Header("Player Aim to Pause")]
        [SerializeField] private PlayerAim _playerAim;

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

            _playerAim.enabled = false;

            _stageClearedScreen.SetActive(true);
        }

        private void StageFailed()
        {
            Time.timeScale = 0f;

            if (_playerAim != null)
                _playerAim.enabled = !_playerAim.enabled;

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

            _playerAim.enabled = true;

            _stageClearedScreen.SetActive(false);
        }
    }
}