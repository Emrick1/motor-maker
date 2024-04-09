﻿using Mechanix;
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
    /// Instance d'un moteur V10.
    /// </summary>
        private static Engine engine3 = new Engine("v10");
    /// <summary>
    /// Instance d'un moteur V12.
    /// </summary>
        private static Engine engine4 = new Engine("v12");
    /// <summary>
    /// Boutton pour la sélection du moteur V6.
    /// </summary>
        public Button buttonEngine1;
    /// <summary>
    /// Boutton pour la sélection du moteur V8.
    /// </summary>
    public Button buttonEngine2;
    /// <summary>
    /// Boutton pour la sélection du moteur V10.
    /// </summary>
    public Button buttonEngine3;
    /// <summary>
    /// Boutton pour la sélection du moteur V12.
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
    /// GameObject du moteur V10 dans le moteur graphique.
    /// </summary>
    public GameObject V10;
    /// <summary>
    /// GameObject du moteur V12 dans le moteur graphique.
    /// </summary>
    public GameObject V12;


    private void Start()
    {

        buttonEngine1.onClick.AddListener(delegate { SwitchEngineTo(0); });
        buttonEngine2.onClick.AddListener(delegate { SwitchEngineTo(1); });
        buttonEngine3.onClick.AddListener(delegate { SwitchEngineTo(2); });
        buttonEngine4.onClick.AddListener(delegate { SwitchEngineTo(3); });
        addEngineToList();
    }

    /// <summary>
    /// Choisi le moteur à afficher.
    /// </summary>
    /// <param name="engineIndex">Indice du moteur voulu dans engineList.</param>
    public void SwitchEngineTo(int engineIndex)
    {
        currentEngine = enginesList[engineIndex];
        engineSelected.text = enginesList[engineIndex].Name;
    }

    /// <summary>
    /// Ajoute une instance de moteur dans engineList.
    /// </summary>
    public static void addEngineToList()
    {
        enginesList.Add(engine1);
        enginesList.Add(engine2);
        enginesList.Add(engine3);
        enginesList.Add(engine4);
    }
}

