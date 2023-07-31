using UnityEngine;

namespace Core
{
    public class CoreManager : MonoBehaviour
    {
        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;

        private void Awake()
        {
            _elementTypeScriptable.Init();
        }
    }
}