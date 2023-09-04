using Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DeviceDisplay;

namespace UI
{
    public class RebindActionUI : MonoBehaviour
    {
        //Input System Stored Data
        private InputAction _inputAction;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;

        [Header("Action name")]
        [SerializeField] private string _actionName;

        [Header("Text area")]
        [SerializeField] private Text _actionText;
        [SerializeField] private Text _bindingText;
        [SerializeField] private Image _bindingImage;

        [Header("Buttons")]
        [SerializeField] private GameObject _bindButton;
        [SerializeField] private GameObject _resetButton;

        [Header("Listening text")]
        [SerializeField] private GameObject _listeningText;

        [Header("Device Display Settings, for icons")]
        public DeviceDisplayConfigurator deviceDisplaySettings;

        private void Awake()
        {
            _inputAction = InputManager.playerInputObject.actions.FindAction(_actionName);

            _actionText.text = _actionName;
            UpdateBindingDisplayUI();
            ShowAction(true);

            InputManager.onControlChanged.AddListener(UpdateBindingOnDeviceChange);
        }

        private void ShowAction(bool show)
        {
            _bindButton.SetActive(show);
            _resetButton.SetActive(show);
            _listeningText.SetActive(!show);
        }

        public void ButtonStartRebind()
        {
            StartRebindProcess();
        }

        void StartRebindProcess()
        {

            ShowAction(false);


            rebindOperation = _inputAction.PerformInteractiveRebinding()
                .WithControlsExcluding("<Mouse>/position")
                .WithControlsExcluding("<Mouse>/delta")
                .WithControlsExcluding("<Gamepad>/Start")
                .WithControlsExcluding("<Keyboard>/escape")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindCompleted());

            rebindOperation.Start();
        }

        void RebindCompleted()
        {
            rebindOperation.Dispose();
            rebindOperation = null;

            ShowAction(true);

            UpdateBindingDisplayUI();
        }

        public void ButtonResetBinding()
        {
            InputActionRebindingExtensions.RemoveAllBindingOverrides(_inputAction);
            UpdateBindingDisplayUI();
        }

        private void UpdateBindingOnDeviceChange(PlayerInput input)
        {
            Debug.Log("Device Changed");
            _inputAction = input.actions.FindAction(_actionName);
            UpdateBindingDisplayUI();
        }

        private void UpdateBindingDisplayUI()
        {
            int controlBindingIndex = _inputAction.GetBindingIndexForControl(_inputAction.controls[0]);
            string currentBindingInput = InputControlPath.ToHumanReadableString(
                _inputAction.bindings[controlBindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            Sprite currentDisplayIcon = deviceDisplaySettings.GetDeviceBindingIcon(InputManager.playerInputObject, currentBindingInput);

            if (currentDisplayIcon != null)
            {
                _bindingText.gameObject.SetActive(false);
                _bindingImage.gameObject.SetActive(true);

                _bindingImage.sprite = currentDisplayIcon;
            }
            else
            {
                _bindingText.gameObject.SetActive(true);
                _bindingImage.gameObject.SetActive(false);

                _bindingText.text = currentBindingInput;
            }
        }
    }
}