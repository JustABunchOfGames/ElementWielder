using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ResolutionUI : MonoBehaviour
    {
        [System.Serializable]
        public struct ScreenSize
        {
            [SerializeField] private int _width;
            public int width { get { return _width; } private set { } }
            [SerializeField] private int _height;
            public int height { get { return _height; } private set { } }
        }

        [Header("General Canvas group")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Resolution screen")]
        [SerializeField] private GameObject _resolutionScreen;

        [Header("UI element for Resolution")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _resolutionText;
        [SerializeField] private Button _resolutionButton;
        [SerializeField] private Text _resolutionButtonText;

        [Header("All possible resolution")]
        [SerializeField] private List<ScreenSize> _resolutions = new List<ScreenSize>();
        private int _sliderIndex;

        private void Awake()
        {
            _slider.onValueChanged.AddListener(SetScreenSize);

            // Search initial screen size
            _sliderIndex = _resolutions.Count - 1;

            int width = Screen.width;
            int height = Screen.height;
            
            for(int i = 0; i < _resolutions.Count; i++)
            {
                _sliderIndex = i;
                if (_resolutions[i].width == width && _resolutions[i].height == height)
                    break;
            }
            _slider.value = _sliderIndex;

            // Set text for initial screen size
            _resolutionText.text = _resolutions[_sliderIndex].width + " x " + _resolutions[_sliderIndex].height;
            _resolutionButtonText.text = "Resolution : " + _resolutionText.text;

            _resolutionScreen.SetActive(false);
        }

        public void ShowResolutionScreen()
        {
            _canvasGroup.interactable = false;
            _resolutionButton.interactable = false;

            _resolutionScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_slider.gameObject);
        }

        public void HideResolutionScreen()
        {
            _canvasGroup.interactable = true;
            _resolutionButton.interactable = true;

            _resolutionScreen.SetActive(false);
            EventSystem.current.SetSelectedGameObject(_resolutionButton.gameObject);
        }

        public void SetScreenSize(float sliderValue)
        {
            _sliderIndex = (int)sliderValue;

            _resolutionText.text = _resolutions[_sliderIndex].width + " x " + _resolutions[_sliderIndex].height;
            _resolutionButtonText.text = "Resolution : " + _resolutionText.text;
        }

        public void Apply()
        {
            Screen.SetResolution(_resolutions[_sliderIndex].width, _resolutions[_sliderIndex].height, true);
        }
    }
}