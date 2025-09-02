using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;

    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private CinemachinePOV _cinemachinePOV;
    [SerializeField] private Slider _sensetivitySlider;

    private PlayerBinds _binds;

    private void Start()
    {
        _binds = PlayerCharacter.Instance.Binds;
        _binds.Character.OpenPanel.started += OnPanelOpened;

        _cinemachinePOV = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sensetivity", 0.1f);
        _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sensetivity", 0.1f);
        _sensetivitySlider.value = PlayerPrefs.GetFloat("Sensetivity", 0.1f);
    }

    public void SetSensetivity(float value)
    {
        _cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = value;
        _cinemachinePOV.m_VerticalAxis.m_MaxSpeed = value;
        PlayerPrefs.SetFloat("Sensetivity", value);
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