using Core;
using Player;
using UnityEngine;

namespace UI
{
    public class StageUI : MonoBehaviour
    {
        [Header("StageScreen")]
        [SerializeField] private GameObject _stageClearedScreen;
        [SerializeField] private GameObject _stageFailedScreen;

        [Header("Player Aim to Pause")]
        [SerializeField] private PlayerAim _playerAim;

        private void Awake()
        {
            StageManagerScriptable.stageClearedEvent.AddListener(StageCleared);
            PlayerHealth.deathEvent.AddListener(StageFailed);
        }

        private void StageCleared()
        {
            Time.timeScale = 0f;

            _playerAim.enabled = false;

            _stageClearedScreen.SetActive(true);
        }

        private void StageFailed()
        {
            Time.timeScale = (Time.timeScale == 1f) ? 0f : 1f;

            if (_playerAim != null)
                _playerAim.enabled = !_playerAim.enabled;

            _stageFailedScreen.SetActive(true);
        }

        private void ResetUI()
        {
            Time.timeScale = 1f;

            _playerAim.enabled = true;

            _stageClearedScreen.SetActive(false);
        }
    }
}