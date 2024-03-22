using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EngineChooserScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameObject v6Engine;
    public static GameObject v8Engine;
    public static GameObject v10Engine;
    public static GameObject v12Engine;
    public static float positionX = 172;
    private static float positionY = 347;
    private static float positionZ = 4;
    private static float rotationX = 0;
    private static float rotationY = 256;
    private static float rotationZ = 0;
    private static double scale = 0.62871;
    void Start()
    {
        v6Engine = GetComponent<GameObject>();
        v8Engine = GetComponent<GameObject>();
        v10Engine = GetComponent<GameObject>();
        v12Engine = GetComponent<GameObject>();
        Instantiate(v6Engine);
        Instantiate(v8Engine);
        Instantiate(v10Engine);
        Instantiate(v12Engine);
        v6Engine.SetActive(true);
        v8Engine.SetActive(false);
        v10Engine.SetActive(false);
        v12Engine.SetActive(false);
        Vector3 position = new Vector3(positionX, positionY, positionZ);
        v6Engine.transform.position = position;
        v6Engine.transform.Rotate(rotationX, rotationY, rotationZ);
        v8Engine.transform.position = position;
        v8Engine.transform.Rotate(rotationX, rotationY, rotationZ);
        v10Engine.transform.position = position;
        v10Engine.transform.Rotate(rotationZ, rotationY, rotationZ);    
        v12Engine.transform.position = position;
        v12Engine.transform.Rotate(rotationX, rotationY, rotationZ);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ChooseV6()
    {
        v6Engine.SetActive(true);
        v8Engine.SetActive(false);
        v10Engine.SetActive(false);
        v12Engine.SetActive(false);
    }

    public static void ChooseV8()
    {
        v6Engine.SetActive(false);
        v8Engine.SetActive(true);
        v10Engine.SetActive(false);
        v12Engine.SetActive(false);
    }

    public static void ChooseV10()
    {
        v6Engine.SetActive(false);
        v8Engine.SetActive(false);
        v10Engine.SetActive(true);
        v12Engine.SetActive(false);
    }

    public static void ChooseV12()
    {
        v6Engine.SetActive(false);
        v8Engine.SetActive(false);
        v10Engine.SetActive(false);
        v12Engine.SetActive(true);
    }
}
