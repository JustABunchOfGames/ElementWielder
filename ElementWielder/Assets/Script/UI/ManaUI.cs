using Core;
using System.Collections.Generic;
using UnityEngine;

namespace UI 
{
    public class ManaUI : MonoBehaviour
    {
        private Dictionary<ElementType, ElementGauge> _elementUI;

        [SerializeField] private List<ElementGauge> _elementGauges = new List<ElementGauge>();
        [SerializeField] private List<ElementType> _elementTypes = new List<ElementType>();

        private void Awake()
        {
            _elementUI = new Dictionary<ElementType, ElementGauge>();

            for(int i = 0; i < _elementGauges.Count; i++)
            {
                _elementUI.Add(_elementTypes[i], _elementGauges[i]);
            }
        }

        public void UpdateManaUI(ElementType type, int value, int maxValue)
        {
            Debug.Log("UpdateManaUI " + value + "/" + maxValue);
            _elementUI[type].UpdateValue(value, maxValue);
        }
    }
}