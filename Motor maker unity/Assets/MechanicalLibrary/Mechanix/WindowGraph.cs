using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>Classe faisant la gestion visuelle d'un graphique de performances.</c>
/// </summary>
public class WindowGraph : MonoBehaviour
{
    /// <summary>
    /// Visuel des points utilisés pour le graphique
    /// </summary>
    [SerializeField] private Sprite circleSprite;
    /// <summary>
    /// Conteneur contenant les points du graphique.
    /// </summary>
    public RectTransform graphContainer;
    /// <summary>
    /// Boutton pour sélectionner le moteur V6.
    /// </summary>
    public Button boutonV6;
    /// <summary>
    /// Boutton pour sélectionner le moteur V8.
    /// </summary>
    public Button boutonV8;
    /// <summary>
    /// Boutton pour sélectionner le moteur V10.
    /// </summary>
    public Button boutonV10;
    /// <summary>
    /// Boutton pour sélectionner le moteur V12.
    /// </summary>
    public Button boutonV12;
    /// <summary>
    /// Liste contenant les points visibles.
    /// </summary>
    private List<RectTransform> listCircleVisible;
    /// <summary>
    /// Graduation du graphique pour le moteur V6.
    /// </summary>
    public GameObject imageV6;
    /// <summary>
    /// Graduation du graphique pour le moteur V8.
    /// </summary>
    public GameObject imageV8;
    /// <summary>
    /// Graduation du graphique pour le moteur V10.
    /// </summary>
    public GameObject imageV10;
    /// <summary>
    /// Graduation du graphique pour le moteur électrique.
    /// </summary>
    public GameObject imageElectrique;
    


    void Start()
    {

        descativer();       
        boutonV6.onClick.AddListener(delegate { ClearGraph(); ShowGraph(EquationV6()); descativer(); showGraphDecalV6(); }) ;
        boutonV8.onClick.AddListener(delegate { ClearGraph(); ShowGraph(EquationV8()); descativer(); showGraphDecalV8(); });
        boutonV12.onClick.AddListener(delegate { ClearGraph(); ShowGraph(EquationElectrique()); descativer(); showGraphDecalElectric(); });
    }

    public void descativer()
    {
        imageV6.SetActive(false);
        imageElectrique.SetActive(false);
        imageV8.SetActive(false);
    }
    void Update()
    {
      
    }

    /// <summary>
    /// Ajoute un point dans le graphique en fonction d'un vecteur de position.
    /// </summary>
    /// <param name="anchoredPosition">Vecteur de position.</param>
    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        
      
    }

    /// <summary>
    /// Ajoute des points dans le graphique en fonction d'une liste contenant les valeurs voulues.
    /// </summary>
    /// <param name="values">Liste de valeurs.</param>
    public void ShowGraph(List<float> values)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float xSize = graphContainer.sizeDelta.x / values.Count;
        float yMaximum = 1000f;

        for (int x = 0; x < values.Count; x++)
        {
            float xPosition = x * xSize;
            float yPosition = (values[x] / yMaximum) * graphHeight;
            CreateCircle(new Vector2( (float) xPosition,  (float) yPosition));
        }
    }

    /// <summary>
    /// Retourne l'équation de performances du moteur V6.
    /// </summary>
    /// <returns>Liste des valeurs en y en fonction de valeurs en x préderterminés.</returns>
    public List<float> EquationV6()
    {
        List<float> values = new List<float>();
        for (int i = 0; i < 4000; i+= 200)
        {
            float value = (float)(0.0000194375 * (Math.Pow(i, 2) + 200));
            values.Add(value);
            
        }
        for (int i = 4000; i < 7100; i += 200) {
            float value = (float)((-0.00005 * Math.Pow(i - 6000, 2)) + 500);
            values.Add(value);
        }
        return values;
    }

    /// <summary>
    /// Retourne l'équation de performances du moteur V8.
    /// </summary>
    /// <returns>>Liste des valeurs en y en fonction de valeurs en x préderterminés.</returns>
    public List<float> EquationV8()
    {
        
        List<float> values = new List<float>();
        for (int i = 0; i < 4000; i += 200)
        {
            float value = (float)(0.000028 * (Math.Pow(i, 2) + 200));
            values.Add(value);

        }
        for (int i = 4000; i < 7500; i += 200)
        {
            float value = (float)((-0.000065 * Math.Pow(i - 6000, 2)) + 707);
            values.Add(value);
        }
        return values;
    }

    /// <summary>
    /// Retourne l'équation de performances du moteur électrique.
    /// </summary>
    /// <returns>>Liste des valeurs en y en fonction de valeurs en x préderterminés.</returns>
    public List<float> EquationElectrique()
                //Tesla Model S
    {
        List<float> values = new List<float>();
        for (int i = 0; i < 12000; i += 200)
        {
            float value = (float)(800);
            values.Add(value);

        }
        for (int i = 12000; i < 15000; i += 200)
        {
            float value = (float)((0.000042 * Math.Pow(i - 15000, 2) + 400));
            values.Add(value);
        }
        return values;
    }

    /// <summary>
    /// Ajuste la graduation du graphique en fenction du moteur V6.
    /// </summary>
    public void showGraphDecalV6()
    {
        imageV6.SetActive(true);

    }

    /// <summary>
    /// Ajuste la graduation du graphique en fenction du moteur V8.
    /// </summary>
    public void showGraphDecalV8()
    {
        imageV8.SetActive(true);

    }

    /// <summary>
    /// Ajuste la graduation du graphique en fenction du moteur V10.
    /// </summary>
    public void ShowGraphDecalV10()
    {
        imageV10.SetActive(true);

    }

    /// <summary>
    /// Ajuste la graduation du graphique en fenction du moteur électrique.
    /// </summary>
    public void showGraphDecalElectric()
    {
        imageElectrique.SetActive(true);

    }

    /// <summary>
    /// Efface tous les points présents dans le graphique.
    /// </summary>
    public void ClearGraph()
    {
       foreach (RectTransform cercle in graphContainer)
        {
            if (cercle != null && cercle.name.Substring(0, 6).Equals("circle"))
            {
                Destroy(cercle.gameObject);
            }
        }
    }


}
