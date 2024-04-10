using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    public RectTransform graphContainer;
    public Button boutonV6;
    public Button boutonV8;
    public Button boutonV10;
    public Button boutonV12;
    private List<RectTransform> listCircleVisible;
    public GameObject imageV6;
    public GameObject imageV8;
    public GameObject imageV10;
    public GameObject imageElectrique;
    


    void Start()
    {

        descativer();       
        boutonV6.onClick.AddListener(delegate { ClearGraph(); ShowGraph(EquationV6()); descativer(); showGraphDecalV6(); }) ;
        boutonV8.onClick.AddListener(delegate { ClearGraph(); ShowGraph(EquationV8()); descativer(); showGraphDecalV8(); });
        boutonV10.onClick.AddListener(null);
        boutonV12.onClick.AddListener(delegate { ClearGraph(); ShowGraph(EquationElectrique()); descativer(); showGraphDecalElectric(); });
    }

    public void descativer()
    {
        imageV6.SetActive(false);
        imageElectrique.SetActive(false);
        imageV8.SetActive(false);
        imageV10.SetActive(false);
    }
    void Update()
    {
      
    }

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
    public List<float> EquationElectrique()
                //Tesla Model S
    {
        List<float> values = new List<float>();
        for (int i = 0; i < 12000; i += 200)
        {
            float value = (float)(i+ 1000);
            values.Add(value);

        }
        for (int i = 12000; i < 15000; i += 200)
        {
            float value = (float)((-0.000065 * i + 12000));
            values.Add(value);
        }
        return values;
    }
    public void showGraphDecalV6()
    {
        imageV6.SetActive(true);

    }
    public void showGraphDecalV8()
    {
        imageV8.SetActive(true);

    }
    public void ShowGraphDecalV10()
    {
        imageV10.SetActive(true);

    }
    public void showGraphDecalElectric()
    {
        imageElectrique.SetActive(true);

    }

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
