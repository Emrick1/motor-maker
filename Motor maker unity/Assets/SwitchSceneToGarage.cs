using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchSceneToGarage : MonoBehaviour
{
   public void SwitchToGarage()
   {
      SceneManager.LoadSceneAsync(1);
   }
}
