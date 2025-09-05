using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _waveBreak;

    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveNumberText;

    [SerializeField] private Wave _wave;

    private void OnEnable()
    {
        _wave.TimerCounted += OnTimerCounted;

        _wave.Started += OnStarted;

        _wave.Ended += OnEnded;
    }

    private void OnStarted(int value)
    {
        _waveNumberText.text = value.ToString();
        _waveBreak.SetActive(false);
    }

    private void OnEnded(int value)
    {
        _waveBreak.SetActive(true);
    }

    private void OnTimerCounted(long value)
    {
        _waveText.text = value.ToString();
    }

    private void OnDisable()
    {
        _wave.TimerCounted -= OnTimerCounted;

        _wave.Started += OnStarted;

        _wave.Ended -= OnEnded;
    }
}