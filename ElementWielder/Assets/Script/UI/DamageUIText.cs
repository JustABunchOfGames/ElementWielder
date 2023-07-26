using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageUIText : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetText(int damage)
        {
            _text.text = damage.ToString();
        }
    }
}