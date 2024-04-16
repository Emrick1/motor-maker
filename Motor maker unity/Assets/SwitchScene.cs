using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>Classe faisant la gestion de changement de scène.</c>
/// </summary>
public class SwitchScene : MonoBehaviour
{
    /// <summary>
    /// Change la scène actuelle.
    /// </summary>
    /// <param name="sceneIndex">Indice de la scène voulue.</param>
   public void SwitchTo(int sceneIndex)
   {
      SceneManager.LoadSceneAsync(sceneIndex);
   }

    /// <summary>
    /// Sert à quitter l'application.
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
