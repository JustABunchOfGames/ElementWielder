using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        private void Awake()
        {
            PlayerHealth.healthChangedEvent.AddListener(UpdateValue);
        }

        public void UpdateValue(int value, int maxValue)
        {
            _text.text = value.ToString();

            _image.fillAmount = (float)value / (float)maxValue;
        }
    }
}