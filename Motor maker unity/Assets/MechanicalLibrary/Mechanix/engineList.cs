using Mechanix;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Mechanix.Engine;

/// <summary>
/// <c>Classe contenant les configurations possibles de moteur et faisant la gestion visuelle de ceux-ci dans le moteur graphique.</c>
/// </summary>
public class engineList : MonoBehaviour
{

    /// <summary>
    /// Liste contenat les instances de moteur possibles à utiliser.
    /// </summary>
    private static List<Engine> enginesList = new List<Engine>();
    /// <summary>
    /// Instance du moteur présentement affichée.
    /// </summary>
    private static Engine currentEngine;
    /// <summary>
    /// Instance du moteur présentement selectionné.
    /// </summary>
    public TextMeshProUGUI engineSelected;
    /// <summary>
    /// Instance d'un moteur V6.
    /// </summary>
    private static Engine engine1 = new Engine("v6");
    /// <summary>
    /// Instance d'un moteur V8.
    /// </summary>
    private static Engine engine2 = new Engine("v8");
    /// <summary>
    /// Instance d'un moteur électrique.
    /// </summary>
    private static Engine engineElectrique = new Engine("Électrique");
    /// <summary>
    /// Boutton pour la sélection du moteur V6.
    /// </summary>
    public Button buttonEngine1;
    /// <summary>
    /// Boutton pour la sélection du moteur V8.
    /// </summary>
    public Button buttonEngine2;

    /// <summary>
    /// Boutton pour la sélection du moteur électrique.
    /// </summary>
    public Button buttonEngine4;
    /// <summary>
    /// GameObject du moteur V6 dans le moteur graphique.
    /// </summary>
    public GameObject V6;
    /// <summary>
    /// GameObject du moteur V8 dans le moteur graphique.
    /// </summary>
    public GameObject V8;
    /// <summary>
    /// GameObject du moteur V12 dans le moteur graphique.
    /// </summary>
    public GameObject electrique;

    public GameObject moteurChoisie;

    public static int moteurSelected = 0;

    private void Start()
    {
        switch (moteurSelected)
        {
            case 0:
                moteurSelected = 1;
                moteurChoisie = V6;
                break;
            case 1:
                moteurChoisie = V6;
                break;
            case 2:
                moteurChoisie = V8;
                break;
            case 3:
                moteurChoisie = electrique;
                break;
        }
        hideEngine();
        SwitchEngineTo();
        moteurChoisie.SetActive(true);

        buttonEngine1.onClick.AddListener(delegate {
            moteurSelected = 1;
            moteurChoisie = V6;
            SwitchEngineTo(); hideEngine(); showEngineV6();
        });
        buttonEngine2.onClick.AddListener(delegate {
            moteurSelected = 2;
            moteurChoisie = V8;
            SwitchEngineTo(); hideEngine(); showEngineV8();
        });
        buttonEngine4.onClick.AddListener(delegate {
            moteurSelected = 3;
            moteurChoisie = electrique;
            SwitchEngineTo(); hideEngine(); showEngineElectirque();
        });
        addEngineToList();
    }

    /// <summary>
    /// Choisi le moteur à afficher.
    /// </summary>
    /// <param name="engineIndex">Indice du moteur voulu dans engineList.</param>
    public void SwitchEngineTo()
    {
        engineSelected.text = moteurChoisie.name;
    }

    /// <summary>
    /// Ajoute une instance de moteur dans engineList.
    /// </summary>
    public static void addEngineToList()
    {
        // ajoute les moteurs a la liste
        enginesList.Add(engine1);
        enginesList.Add(engine2);
        enginesList.Add(engineElectrique);
    }
    public void showEngineV6()
    {
        V6.SetActive(true);
    }
    public void showEngineV8()
    {
        V8.SetActive(true);
    }
    public void showEngineElectirque()
    {
        electrique.SetActive(true);
    }
    public void hideEngine()
    {
        V6.SetActive(false);
        V8.SetActive(false);
        electrique.SetActive(false);
    }
}

