using Mechanix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Mechanix.Engine;

    public class engineList : MonoBehaviour
    {

        private static List<Engine> enginesList = new List<Engine>();

        private static Engine engine1 = new Engine("v6");
        private static Engine engine2 = new Engine("v8");
        private static Engine engine3 = new Engine("v10");
        private static Engine engine4 = new Engine("v12");


    private void Start()
    {

        addEngineToList();
    }


    public static void addEngineToList()
    {
        enginesList.Add(engine1);
        enginesList.Add(engine2);
        enginesList.Add(engine3);
        enginesList.Add(engine4);


    }


}

