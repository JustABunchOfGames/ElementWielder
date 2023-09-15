using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Core
{
    public class KeepInputWhileLoading : MonoBehaviour
    {
        public static bool inputExist = false;

        public static PlayerInput playerInputObject { get; private set; }

        // Overwrite PlayerInput event because it doesn't work
        public class OnControlChanged : UnityEvent<PlayerInput> { }
        public static OnControlChanged onControlChanged = new OnControlChanged();

        private void Awake()
        {
            if (inputExist)
            {
                Destroy(gameObject);
            }
            else
            {
                inputExist = true;
                playerInputObject = GetComponent<PlayerInput>();
                DontDestroyOnLoad(gameObject);
            }
        }

        public void ControlChanged(PlayerInput input)
        {
            onControlChanged.Invoke(input);
        }
    }
}