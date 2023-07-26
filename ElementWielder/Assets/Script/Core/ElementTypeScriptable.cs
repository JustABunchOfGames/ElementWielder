using System.Collections.Generic;
using UnityEngine;

namespace Core {
    [CreateAssetMenu(menuName = "Element/Material")]
    public class ElementTypeScriptable : ScriptableObject
    {
        [Header("ElementScriptable")]
        [SerializeField] private List<ElementScriptable> _elementList;

        private Dictionary<ElementType, ElementScriptable> _elementDictionary;

        [Header("ElementBonuses")]
        [SerializeField] private List<ElementType> _attackElement;
        [SerializeField] private List<ElementType> _targetElement;

        private Dictionary<ElementType, ElementType> _elementBonusDictionary;

        public Material GetMaterialByElement(ElementType element)
        {
            return _elementDictionary[element].material;
        }

        public float GetElementBonus(ElementType attackElement,  ElementType targetElement)
        {
            // Base multiplier
            float result = 1f;

            // if the attack has a bonus on the target, better multiplier
            if (_elementBonusDictionary[attackElement] == targetElement)
                result = 2f;

            // if the target has a bonus on the attack, worse multiplier
            if (_elementBonusDictionary[targetElement] == attackElement)
                result = 0.5f;

            return result;
        }

        public void Init()
        {
            // Scriptable dictionary
            _elementDictionary = new Dictionary<ElementType, ElementScriptable>();

            for (int i = 0; i < _elementList.Count; i++)
                _elementDictionary.Add(_elementList[i].type, _elementList[i]);


            // Bonus dictionary
            _elementBonusDictionary = new Dictionary<ElementType, ElementType>();

            for(int i = 0; i < _attackElement.Count;i++)
                _elementBonusDictionary.Add(_attackElement[i], _targetElement[i]);
        }
    }
}