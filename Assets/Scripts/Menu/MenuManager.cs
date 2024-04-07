using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private float _currentFuel;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textSlider;

    private void OnValidate()
    {
        if (_slider.value <= 500)
        {
            _slider.value = 500;
        } 
    }

    private void Awake()
    {
        _slider.maxValue = 10000;
    }

    private void Update()
    {
        _currentFuel = _slider.value;
        _textSlider.text = _currentFuel.ToString();
        PlayerPrefs.SetFloat("Fuels",_currentFuel);
    }

    public void SelectLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
