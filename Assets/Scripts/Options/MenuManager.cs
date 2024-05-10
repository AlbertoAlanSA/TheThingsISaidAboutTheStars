using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using WaitForFixedUpdate = UnityEngine.WaitForFixedUpdate;

public class MenuManager : MonoBehaviour
{
        [SerializeField] private GameObject _canvasMenu;
        private GenericCanvasManager _genericCanvasManager;
        public bool Pause { get; set; }

        private void Awake()
        {
                Pause = false;
                _genericCanvasManager = _canvasMenu.GetComponent<GenericCanvasManager>();
        }

        public void OpenMenu()
        {
                Pause = true;
                _canvasMenu.SetActive(true);
                _genericCanvasManager.CreateButtons();
               // StartCoroutine(wait());
                // _inputReader.EnableMenuInput();
        }

        public void CloseMenu()
        {
                Pause = false;
                _canvasMenu.SetActive(false);
                _genericCanvasManager.DeleteButtons();
                //StartCoroutine(wait());
                // _inputReader.EnableGameplayInput();
        }

        IEnumerator wait()
        {
                yield return new WaitForFixedUpdate();
                
        }
        
}
