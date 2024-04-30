using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GameObject _canvasMenu;
        
        public bool Pause { get; set; }

        private void Awake()
        {
                Pause = false;
        }

        public void OpenMenu()
        {
                Pause = true;
                _canvasMenu.SetActive(true);
                _inputReader.EnableMenuInput();
        }

        public void CloseMenu()
        {
                Pause = false;
                _canvasMenu.SetActive(false);
                _inputReader.EnableGameplayInput();
        }
}
