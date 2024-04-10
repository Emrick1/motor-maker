using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>Classe faisant la gestion de changement de sc�ne.</c>
/// </summary>
public class SwitchScene : MonoBehaviour
{
    /// <summary>
    /// Change la sc�ne actuelle.
    /// </summary>
    /// <param name="sceneIndex">Indice de la sc�ne voulue.</param>
   public void SwitchTo(int sceneIndex)
   {
      SceneManager.LoadSceneAsync(sceneIndex);
   }

    /// <summary>
    /// Sert � quitter l'application.
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
