using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using TMPro;
using UnityEngine;

public class SideSystems : MonoBehaviour
{
    [Space(10)] 
    [Header("Weather - Settings")] 
    public bool isFrontWind;
    
    public bool isCrosswind;
    
    public bool isBackWind;
    
    [Range(0, 3)] [SerializeField] private float _windForce = 0.1f;

    [Space(10)] 
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _textFront;
    [SerializeField] private TextMeshProUGUI _textBack;
    [SerializeField] private TextMeshProUGUI _textCross;

    public float wind;
    private void Update()
    {
        wind = _windForce;
        if (isCrosswind == true)
        {
            StartCoroutine(Crosswind());
            _textCross.text = "CrossWind ON";
        }
        else
        {
            _textCross.text = "CrossWind OFF"; 
        }

        if (isBackWind == true)
        {
            StartCoroutine(BackWind());
            _textBack.text = "BackWind ON";
        }
        else
        {
            _textBack.text = "BackWind OFF"; 
        }

        if (isFrontWind == true)
        {
            StartCoroutine(FrontWind());
            _textFront.text = "FrontWind ON";
        }
        else
        {
            _textFront.text = "FrontWind OFF"; 
        }
    }

    IEnumerator Crosswind()
    {
        wind += 2;
        yield break;
    }
    IEnumerator FrontWind()
    {
        wind += 3;
        yield break;
    }
    IEnumerator BackWind()
    {
        wind += 4;
        yield break;
    }

    public void CrossWindEnabledes()
    {
        isCrosswind = !isCrosswind;
    }
    public void FrontWindEnabledes()
    {
        isFrontWind = !isFrontWind;
    }
    public void BackWindEnabledes()
    {
        isBackWind = !isBackWind;
    }
}
