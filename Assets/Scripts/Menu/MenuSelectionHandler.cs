using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu
{
    public class MenuSelectionHandler : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField][ReadOnly] private GameObject _defaultSelection;
        [SerializeField][ReadOnly] private GameObject _currentSelection;
        [SerializeField][ReadOnly] private GameObject _mouseSelection;

        private void OnEnable()
        {
            _inputReader.MenuMouseMoveEvent += HandleMoveCursor;
            _inputReader.MoveSelectionEvent += HandleMoveSelection;

        
        }
        private void OnDisable()
        {
            _inputReader.MenuMouseMoveEvent -= HandleMoveCursor;
            _inputReader.MoveSelectionEvent -= HandleMoveSelection;
        }
        public void UpdateDefault(GameObject newDefault)
        {
            _defaultSelection = newDefault;
        }

   
        private IEnumerator SelectDefault()
        {
            yield return new WaitForSeconds(.03f); // Necessary wait otherwise the highlight won't show up

            if (_defaultSelection != null)
                UpdateSelection(_defaultSelection);
        }

        public void Unselect()
        {
            _currentSelection = null;
            if (EventSystem.current != null)
                EventSystem.current.SetSelectedGameObject(null);
        }
        private void HandleMoveSelection()
        {
            Cursor.visible = false;

            // Handle case where no UI element is selected because mouse left selectable bounds
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(_currentSelection);
        }
        private void HandleMoveCursor()
        {
            if (_mouseSelection != null)
            {
                EventSystem.current.SetSelectedGameObject(_mouseSelection);
            }

            Cursor.visible = true;
        }

        public void HandleMouseEnter(GameObject UIElement)
        {
            _mouseSelection = UIElement;
            EventSystem.current.SetSelectedGameObject(UIElement);
        }

        public void HandleMouseExit(GameObject UIElement)
        {
            if (EventSystem.current.currentSelectedGameObject != UIElement)
            {
                return;
            }

            // keep selecting the last thing the mouse has selected 
            _mouseSelection = null;
            EventSystem.current.SetSelectedGameObject(_currentSelection);
        }
    
        public bool AllowsSubmit()
        {
            // if LMB is not down, there is no edge case to handle, allow the event to continue
            return !_inputReader.LeftMouseDown()
                   // if we know mouse & keyboard are on different elements, do not allow interaction to continue
                   || _mouseSelection != null && _mouseSelection == _currentSelection;
        }


        public void UpdateSelection(GameObject UIElement)
        {
            if ((UIElement.GetComponent<MultiInputSelectableElement>() != null) || (UIElement.GetComponent<MultiInputButton>() != null))
            {
                _mouseSelection = UIElement;
                _currentSelection = UIElement;
            }
        }
    
        private void Update()
        {
            if ((EventSystem.current != null) && (EventSystem.current.currentSelectedGameObject == null) && (_currentSelection != null))
            {

                EventSystem.current.SetSelectedGameObject(_currentSelection);
            }
        }
    }
}
