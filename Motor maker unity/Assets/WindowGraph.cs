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
    public GameObject cloneBackground;


    void Start()
    {
        listCircleVisible = new List<RectTransform>();
        boutonV6.onClick.AddListener(delegate { ShowV6(); }) ;
        boutonV8.onClick.AddListener(null);
        boutonV10.onClick.AddListener(null);
        boutonV12.onClick.AddListener(null);
    }


    void Update()
    {
      
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.tag = "Circle";
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.tag = "Circle";
        listCircleVisible.Add(rectTransform);
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
        for (int i = 4000; i < 9000; i += 200) {
            float value = (float)((-0.00005 * Math.Pow(i - 6000, 2)) + 500);
            values.Add(value);
        }
        return values;
    }

    public void ShowV6()
    {
        ClearGraph();
        ShowGraph(EquationV6());
    }

    public void ClearGraph()
    {
        foreach (Transform child in graphContainer) 
        { 
                Destroy(child.gameObject);
            }
        GameObject background = CloneBackGround();
        background.transform.SetParent(graphContainer.transform);
    }

    public GameObject CloneBackGround()
    {
        GameObject background = new GameObject();
        int indexChild = cloneBackground.transform.childCount;

        for (int i = 0; i < indexChild; i++)
        {
            GameObject childToClone = cloneBackground.transform.GetChild(i).gameObject;
            GameObject clonedChild = Instantiate(childToClone);
            clonedChild.transform.SetParent(cloneBackground.transform);
            clonedChild.transform.localPosition = childToClone.transform.localPosition;
            clonedChild.transform.localRotation = childToClone.transform.localRotation;
            clonedChild.name = "Cloned Child";
        }

        return background;
    }
}
