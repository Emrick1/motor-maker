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

    public class engineList : MonoBehaviour
    {

        private static List<Engine> enginesList = new List<Engine>();
        private static Engine currentEngine;
        public TextMeshProUGUI engineSelected;

        private static Engine engine1 = new Engine("v6");
        private static Engine engine2 = new Engine("v8");
        private static Engine engine3 = new Engine("v10");
        private static Engine engine4 = new Engine("v12");
        public Button buttonEngine1;
        public Button buttonEngine2;
        public Button buttonEngine3;
        public Button buttonEngine4;
        public GameObject V6;
        public GameObject V8;
        public GameObject V10;
        public GameObject V12;
        public LineRenderer graphs;


    private void Start()
    {

        buttonEngine1.onClick.AddListener(delegate { SwitchEngineTo(0); });
        buttonEngine2.onClick.AddListener(delegate { SwitchEngineTo(1); });
        buttonEngine3.onClick.AddListener(delegate { SwitchEngineTo(2); });
        buttonEngine4.onClick.AddListener(delegate { SwitchEngineTo(3); });
        addEngineToList();
    }
    public void dessinerEnginGraph(int rpmMin, int rpmMax, int torqueMin, int torqueMax, int benchmarkDescente, double a1, double a2 )
    {
        int nbPoints = (rpmMax - rpmMin)/100;
        graphs.positionCount = nbPoints;

        for (int i = rpmMin; i < rpmMax; i++)
        {
            float x = i / (float)(nbPoints - 1);
            if (i <= benchmarkDescente)
            {
                float y = 0f;
            } 
            else
            {
                float y = 0f;
            }
        }

    }
    public void SwitchEngineTo(int engineIndex)
    {
        currentEngine = enginesList[engineIndex];
        engineSelected.text = enginesList[engineIndex].Name;
    }


    public static void addEngineToList()
    {
        enginesList.Add(engine1);
        enginesList.Add(engine2);
        enginesList.Add(engine3);
        enginesList.Add(engine4);


    }


}

