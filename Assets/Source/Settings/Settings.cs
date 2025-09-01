using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    private PlayerBinds _binds;
    
    private void Start()
    {
        _binds = PlayerCharacter.Instance.Binds;
        _binds.Character.OpenPanel.started += OnPanelOpened;
    }

    private void OnPanelOpened(InputAction.CallbackContext obj)
    {
        _settingsCanvas.SetActive(!_settingsCanvas.activeSelf);
        if (_settingsCanvas.activeSelf)
        {
            Time.timeScale = 0;
            StopAllCoroutines();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnDisable()
    {
        _binds.Character.OpenPanel.started -= OnPanelOpened;
    }
}
