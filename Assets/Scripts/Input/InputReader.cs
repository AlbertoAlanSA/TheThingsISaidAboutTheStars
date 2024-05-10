using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenusActions
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
        public event UnityAction<Vector2> MoveSelectionEvent = delegate { };

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
                        _gameInput.Gameplay.SetCallbacks(this);
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
                //_gameInput.Menus.Disable();
               // _gameInput.Dialogues.Disable();
                _gameInput.Gameplay.Enable();
        }

        public void EnableMenuInput()
        {
               // _gameInput.Dialogues.Disable();
               // _gameInput.Gameplay.Disable();

                //_gameInput.Menus.Enable();
        }

        public void EnableDialogueInput()
        {
                //_gameInput.Menus.Enable();
               // _gameInput.Gameplay.Disable();
               // _gameInput.Dialogues.Enable();
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
                if (context.performed) OptionsEvent.Invoke();
                
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

        #region menus
        
        public void OnMoveSelection(InputAction.CallbackContext context)
        {
                MoveSelectionEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnConfirm(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        void GameInput.IMenusActions.OnCancel(InputAction.CallbackContext context)
        {
                //if (context.performed) OptionsEvent.Invoke();
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnUnpause(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnChangeTab(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

        public void OnCloseInventory(InputAction.CallbackContext context)
        {
                throw new NotImplementedException();
        }

     

        

        #endregion

        public bool LeftMouseDown() => Mouse.current.leftButton.isPressed;









}
