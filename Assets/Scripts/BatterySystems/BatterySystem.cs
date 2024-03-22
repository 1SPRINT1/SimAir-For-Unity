using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BatterySystem : MonoBehaviour
{
    [Space(10)]
    [Header("Battery-Info")]
    [Range(0,7000)]
    [SerializeField] private float _maxBatteryTank;
    [SerializeField] private float _batteryTank;
    [SerializeField] private float _chargeConsumption;
    public bool isTankEmpty = false;
    public float refillingTank;

    [Space(10)] 
    [Header("UI")] 
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textBattery;

    [Space(10)] 
    [Header("Other-Scipts")] 
    [SerializeField] private SideSystems _sideSystems;
    private void OnValidate()
    {
        if (_maxBatteryTank <= 0)
        {
            _maxBatteryTank = 0;
        } 
    }

    private void Start()
    {
        _slider.maxValue = _maxBatteryTank;
        _batteryTank = _maxBatteryTank;
        refillingTank = _maxBatteryTank;
        _sideSystems = FindObjectOfType<SideSystems>();
    }

    private void Update()
    {
        _chargeConsumption = _sideSystems.wind;
        float batteryPercentent = (_batteryTank / _maxBatteryTank) * 100f;
        _textBattery.text = batteryPercentent.ToString("#");
        (_slider.value = _batteryTank).ToString();
        // Расчет расхода заряда
        if (isTankEmpty == false)
        {
            if (_sideSystems.isCrosswind == true || _sideSystems.isBackWind == true || _sideSystems.isFrontWind == true)
            {
                _batteryTank -= (_chargeConsumption * Time.deltaTime) * 20f;
            }
        }
            

        if (_batteryTank <= 50f)
        {
            isTankEmpty = true;
        }
        else
        {
            isTankEmpty = false;
        }
    }

    public void ReffilingDroneTank()
    {
        _batteryTank = refillingTank;
    }
}
