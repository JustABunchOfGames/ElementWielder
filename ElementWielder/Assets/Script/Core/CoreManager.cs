using UnityEngine;

namespace Core
{
    public class CoreManager : MonoBehaviour
    {
        [Header("Scriptable ElementType List")]
        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;

        [Header("Scriptable Stage Manager")]
        [SerializeField] private StageManagerScriptable _stageManager;

        private void Awake()
        {
            // Create a dictionary to use it faster
            _elementTypeScriptable.Init();

            // Reset stage number
            _stageManager.Reset();
        }
    }
}