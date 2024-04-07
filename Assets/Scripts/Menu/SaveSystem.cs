using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
  [SerializeField] private float _fuels;

   private void Start()
   {
       _fuels = PlayerPrefs.GetFloat("Fuels");
   }

   private void Update()
   {
       _fuels = PlayerPrefs.GetFloat("Fuels");
   }
}
