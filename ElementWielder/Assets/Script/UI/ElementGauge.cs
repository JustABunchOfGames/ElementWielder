using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ElementGauge : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        public void UpdateValue(int value, int maxValue)
        {
            _text.text = value.ToString();

            _image.fillAmount = (float)value / (float)maxValue;
        }
    }
}