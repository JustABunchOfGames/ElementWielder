using UnityEngine;

namespace Core
{
    public class CoreManager : MonoBehaviour
    {
        [Header("Scriptable ElementType List")]
        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;

        private void Awake()
        {
            // Create a dictionary to use it faster
            _elementTypeScriptable.Init();
        }
    }
}