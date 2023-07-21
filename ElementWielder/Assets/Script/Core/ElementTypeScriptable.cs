using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core {
    [CreateAssetMenu(menuName = "Element/Material")]
    public class ElementTypeScriptable : ScriptableObject
    {
        [SerializeField] private List<ElementType> _elementList;
        [SerializeField] private List<Material> _materialList;

        private Dictionary<ElementType, Material> _elementMaterial = new Dictionary<ElementType, Material>();

        public Material GetMaterialByElement(ElementType element)
        {
            if (_elementMaterial.Count != _elementList.Count)
            {
                _elementMaterial.Clear();

                for(int i = 0;  i < _elementList.Count; i++)
                {
                    _elementMaterial.Add(_elementList[i], _materialList[i]);
                }
            }

            return _elementMaterial[element];
        }
    }
}