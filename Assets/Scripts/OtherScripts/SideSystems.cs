using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class SideSystems : MonoBehaviour
{
    [Space(10)] 
    [Header("Weather - Settings")] 
    public bool isFrontWind;
    
    public bool isCrosswind;
    
    public bool isBackWind;
    
    [Range(0, 3)] [SerializeField] private float _windForce = 0.1f;

    public float wind;

    private void Update()
    {
        wind = _windForce;
        if (isCrosswind == true)
        {
            StartCoroutine(Crosswind());
        }

        if (isBackWind == true)
        {
            StartCoroutine(BackWind());
        }

        if (isFrontWind == true)
        {
            StartCoroutine(FrontWind());
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
}
