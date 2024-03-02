using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanix
{
    public class Gearbox : MonoBehaviour, IMechanicalPiece
    {
        private static List<Gear> gears = new List<Gear>();
        private PerfCalc perfCalc;
        private static Gear currentGear;
        public TextMeshProUGUI gearSelected;
        private static Gear gearMenante = new Gear(5, 1 , "Menante");
        private static Gear gear1 = new Gear(5, 1, "Gear1");
        private static Gear gear2 = new Gear(5, 1, "Gear2");
        private static Gear gear3 = new Gear(5, 1, "Gear3");
        private static Gear gear4 = new Gear(5, 1, "Gear4");
        private static Gear gear5 = new Gear(5, 1, "Gear5");
        private static Gear gear6 = new Gear(5, 1, "Gear6");
        private static Gear gearLimitante = new Gear(5, 1, "Limitante");
        public Button buttonGearMenante;
        public Button buttonGear1;
        public Button buttonGear2;
        public Button buttonGear3;
        public Button buttonGear4;
        public Button buttonGearLimitante;

        public Gearbox(List<Gear> gearsSet)
        {
            gears = gearsSet;


        }
        void Start()
        {
            addGearToList();
        }
        public void addGearToList()
        {
            gears.Add(gearMenante);
            gears.Add(gear1);
            gears.Add(gear2);
            gears.Add(gear3);
            gears.Add(gear4);
            gears.Add(gear5);
            gears.Add(gear6);
            gears.Add(gearLimitante);

            buttonGearMenante.onClick.AddListener(delegate { SwitchGearTo(0); });
            buttonGear1.onClick.AddListener(delegate { SwitchGearTo(1); });
            buttonGear2.onClick.AddListener(delegate { SwitchGearTo(2); });
            buttonGear3.onClick.AddListener(delegate { SwitchGearTo(3); });
            buttonGear4.onClick.AddListener(delegate { SwitchGearTo(4); });
        }
        public void SwitchGearTo(int gearIndex)
        {
            currentGear = gears[gearIndex];
            gearSelected.text = gears[gearIndex].Name;
        }

        public Gearbox()
        {
            gears = new List<Gear>();
        }

        public void removeGear(int index)
        {
            gears.RemoveAt(index);
        }

        public void removeGear(Gear gear)
        {
            gears.Remove(gear);
        }

        public void clearGearbox()
        {
            gears = new List<Gear>();
        }

        public static double calculateRatio(Gear menant, Gear menee, Boolean recule)
        {
            double ratio;

            ratio = (menant.NbDents/menee.NbDents);

            if(recule) {
                ratio *= (-1) ;}

            return ratio;
        }
        void Update()
        {
           


        }
    }
}