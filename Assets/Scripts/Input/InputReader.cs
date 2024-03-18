using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions,GameInput.IMenusActions, GameInput.IDialoguesActions
{
        // Gameplay
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public event UnityAction InteractuateEvent = delegate { };
        public event UnityAction OpenInventoryEvent = delegate { };
        public event UnityAction CancelEvent = delegate { };
        public event UnityAction DiaryEvent = delegate { };
        public event UnityAction MP3Event = delegate { };
        public event UnityAction OptionsEvent = delegate { };


        //Menus
        public event UnityAction MoveSelectionEvent = delegate { };

        public event UnityAction MenuMouseMoveEvent = delegate { };
        public event UnityAction MenuClickButtonEvent = delegate { };
        public event UnityAction MenuUnpauseEvent = delegate { };
        public event UnityAction MenuPauseEvent = delegate { };

        public event UnityAction MenuCloseEvent = delegate { };

        //public event UnityAction OpenInventoryEvent = delegate { }; // Used to bring up the inventory
        public event UnityAction CloseInventoryEvent = delegate { }; // Used to bring up the inventory
        public event UnityAction<float> TabSwitched = delegate { };

        // Dialogues
        public event UnityAction AdvanceDialogueEvent = delegate { };

        private GameInput _gameInput;

        private void OnEnable()
        {
                if (_gameInput == null)
                {
                        _gameInput = new GameInput();
                        _gameInput.Menus.SetCallbacks(this);
                        _gameInput.Gameplay.SetCallbacks(this);
                        _gameInput.Dialogues.SetCallbacks(this);
                }

        }

        private void OnDisable()
        {
                DisableAllInput();
        }

        public void DisableAllInput()
        {
                _gameInput.Gameplay.Disable();
                _gameInput.Menus.Disable();
                _gameInput.Dialogues.Disable();
        }


        public void EnableGameplayInput()
        {
                _gameInput.Menus.Disable();
                _gameInput.Dialogues.Disable();
                _gameInput.Gameplay.Enable();
        }

        public void EnableMenuInput()
        {
                _gameInput.Dialogues.Disable();
                _gameInput.Gameplay.Disable();

                _gameInput.Menus.Enable();
        }

        public void EnableDialogueInput()
        {
                _gameInput.Menus.Enable();
                _gameInput.Gameplay.Disable();
                _gameInput.Dialogues.Enable();
        }

        #region GAMEPLAY

        public void OnMove(InputAction.CallbackContext context)
        {
                MoveEvent.Invoke(context.ReadValue<Vector2>());

        }

        public void OnInteractuate(InputAction.CallbackContext context)
        {
                if (context.performed) InteractuateEvent.Invoke();
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
                if (context.performed) OpenInventoryEvent.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
                if (context.performed) CancelEvent.Invoke();
        }

        public void OnDiary(InputAction.CallbackContext context)
        {
                if (context.performed) DiaryEvent.Invoke();
        }

        public void OnMP3(InputAction.CallbackContext context)
        {
                if (context.performed) MP3Event.Invoke();
        }

        public void OnOptions(InputAction.CallbackContext context)
        {
                if (context.performed) OptionsEvent.Invoke();
        }

        #endregion

        #region MENUS


        public void OnMoveSelection(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed) 
                        MoveSelectionEvent.Invoke();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public void OnConfirm(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed)
                        MenuClickButtonEvent.Invoke();
        }

        void GameInput.IMenusActions.OnCancel(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed)
                        MenuCloseEvent.Invoke();
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed)
                        MenuMouseMoveEvent.Invoke();
        }

        public void OnUnpause(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed)
                        MenuUnpauseEvent.Invoke();
        }

        public void OnChangeTab(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed)
                        TabSwitched.Invoke(context.ReadValue<float>());
        }

        public void OnInventoryActionButton(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnSaveActionButton(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnResetActionButton(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnPoint(InputAction.CallbackContext context)
        {

        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnCloseInventory(InputAction.CallbackContext context)
        {
                CloseInventoryEvent.Invoke();
        }

        #endregion

        #region DIALOGUES

        public void OnAdvanceDialogue(InputAction.CallbackContext context)
        {
                if (context.phase == InputActionPhase.Performed)
                        AdvanceDialogueEvent.Invoke();
        }

        #endregion
        
        public bool LeftMouseDown() => Mouse.current.leftButton.isPressed;









}
