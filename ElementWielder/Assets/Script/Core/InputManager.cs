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

        public static PauseEvent pauseEvent = new PauseEvent();
        private bool isPaused;

        private PlayerInputClass playerInputClass;

        private Dictionary<ElementType, bool> _attackHeld;

        private Dictionary<String, ElementType> _elementString;

        [Header("For mouse aiming")]
        [SerializeField] private Camera _camera;

        [Header("Player to rotate for aiming")]
        [SerializeField] private GameObject _player;

        private void Update()
        {
            if (isPaused)
                return;

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

            _attackHeld = new Dictionary<ElementType, bool>();
            _elementString = new Dictionary<string, ElementType>();
            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                _attackHeld[element] = false;
                _elementString[element.ToString()] = element;
            }

            PlayerInput playerInput = GetComponent<PlayerInput>();
            
            /*
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

            playerInputClass.Player.TogglePause.performed += TogglePause;
            */

            // Rebinding don't apply on PlayerInputClass, so juste use PlayerInput
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Basic").performed += StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Basic").canceled += StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Fire").performed += StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Fire").canceled += StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Wind").performed += StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Wind").canceled += StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Earth").performed += StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Earth").canceled += StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Water").performed += StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Water").canceled += StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("TogglePause").performed += TogglePause;

            playerInputClass.Enable();

            isPaused = false;
        }

        private void OnDestroy()
        {
            if (KeepInputWhileLoading.playerInputObject == null)
                return;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Basic").performed -= StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Basic").canceled -= StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Fire").performed -= StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Fire").canceled -= StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Wind").performed -= StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Wind").canceled -= StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Earth").performed -= StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Earth").canceled -= StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("Water").performed -= StartAttack;
            KeepInputWhileLoading.playerInputObject.actions.FindAction("Water").canceled -= StopAttack;

            KeepInputWhileLoading.playerInputObject.actions.FindAction("TogglePause").performed -= TogglePause;
        }

        public void PauseInput(bool pause)
        {
            isPaused = pause;
        }

        private void StartAttack(InputAction.CallbackContext context)
        {
            if (isPaused)
                return;

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

        private void TogglePause(InputAction.CallbackContext context)
        {
            pauseEvent.Invoke();
        }
    }

    public class AttackEvent : UnityEvent<ElementType> { }

    public class PauseEvent : UnityEvent { }
}