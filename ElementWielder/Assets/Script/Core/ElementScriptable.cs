using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName ="Element/New")]
    public class ElementScriptable : ScriptableObject
    {
        [SerializeField] private ElementType _type;
        public ElementType type { get { return _type; } private set { } }

        [SerializeField] private Material _material;
        public Material material { get { return _material; } private set { } }
    }
}