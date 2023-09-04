using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Core
{
    public class InputManager : MonoBehaviour
    {
        public static AttackEvent attackEvent = new AttackEvent();

        // Overwrite PlayerInput event because it doesn't work
        public static OnControlChanged onControlChanged = new OnControlChanged();

        private PlayerInputClass playerInputClass;

        public static PlayerInput playerInputObject { get; private set; }

        private Dictionary<ElementType, bool> _attackHeld;

        private Dictionary<String, ElementType> _elementString;

        [Header("For mouse aiming")]
        [SerializeField] private Camera _camera;

        [Header("Player to rotate for aiming")]
        [SerializeField] private GameObject _player;

        private void Update()
        {
            // Old Input Manager
            /*
            if (Input.GetButton("BasicAttack"))
                attackEvent.Invoke(ElementType.Basic);

            if (Input.GetButton("FireAttack"))
                attackEvent.Invoke(ElementType.Fire);

            if (Input.GetButton("WindAttack"))
                attackEvent.Invoke(ElementType.Wind);

            if (Input.GetButton("EarthAttack"))
                attackEvent.Invoke(ElementType.Earth);

            if (Input.GetButton("WaterAttack"))
                attackEvent.Invoke(ElementType.Water);
            */

            Aim();
        }

        

        private void Aim()
        {
            InputControl inputControl = playerInputClass.Player.Aim.activeControl;

            Vector2 inputPos = playerInputClass.Player.Aim.ReadValue<Vector2>();

            if (inputControl != null)
            {

                if (inputControl.displayName == "Left Stick")
                {
                    if (inputPos != Vector2.zero)
                    {
                        // Get Angle in Radians
                        float AngleRad = Mathf.Atan2(inputPos.y - transform.position.y, inputPos.x - transform.position.x);

                        // Get Angle in Degrees
                        float AngleDeg = (180 / Mathf.PI) * AngleRad;

                        // Rotate Player
                        _player.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
                    }
                }
                else if (inputControl.displayName == "Position")
                {
                    inputPos = _camera.ScreenToWorldPoint(inputPos);

                    // Get Angle in Radians
                    float AngleRad = Mathf.Atan2(inputPos.y - transform.position.y, inputPos.x - transform.position.x);

                    // Get Angle in Degrees
                    float AngleDeg = (180 / Mathf.PI) * AngleRad;

                    // Rotate Player
                    _player.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
                }
            }
        }

        private void Awake()
        {
            playerInputClass = new PlayerInputClass();
            playerInputObject = GetComponent<PlayerInput>();

            _attackHeld = new Dictionary<ElementType, bool>();
            _elementString = new Dictionary<string, ElementType>();
            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                _attackHeld[element] = false;
                _elementString[element.ToString()] = element;
            }

            playerInputClass.Player.Basic.performed += StartAttack;
            playerInputClass.Player.Basic.canceled += StopAttack;

            playerInputClass.Player.Fire.performed += StartAttack;
            playerInputClass.Player.Fire.canceled += StopAttack;

            playerInputClass.Player.Wind.performed += StartAttack;
            playerInputClass.Player.Wind.canceled += StopAttack;

            playerInputClass.Player.Earth.performed += StartAttack;
            playerInputClass.Player.Earth.canceled += StopAttack;

            playerInputClass.Player.Water.performed += StartAttack;
            playerInputClass.Player.Water.canceled += StopAttack;

            playerInputClass.Enable();
        }

        private void OnEnable()
        {
            playerInputClass.Enable();
        }

        private void OnDisable()
        {
            playerInputClass.Disable();
        }

        private void StartAttack(InputAction.CallbackContext context)
        {
            // ElementType element = (ElementType)System.Enum.Parse(typeof(ElementType),context.action.name);
            ElementType element = _elementString[context.action.name];

            // Coroutine while button held
            _attackHeld[element] = true;
            StartCoroutine(HeldAttack(element));
        }

        private IEnumerator HeldAttack(ElementType element)
        {
            while (_attackHeld[element])
            {
                attackEvent.Invoke(element);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void StopAttack(InputAction.CallbackContext context)
        {
            // ElementType element = (ElementType)System.Enum.Parse(typeof(ElementType), context.action.name);
            ElementType element = _elementString[context.action.name];
            _attackHeld[element] = false;
        }

        public void ControlChanged(PlayerInput input)
        {
            onControlChanged.Invoke(input);
        }
    }

    public class AttackEvent : UnityEvent<ElementType> { }

    public class OnControlChanged : UnityEvent<PlayerInput> { }
}