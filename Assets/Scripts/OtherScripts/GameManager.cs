using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private void Start()
   {
       Time.timeScale = 0f;
   }

   public void SelectMenu()
   {
      SceneManager.LoadScene(0);
   }

   public void SelectedImages()
   {
       Time.timeScale = 1f;
   }
}
