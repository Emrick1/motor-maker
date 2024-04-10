using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanix
{
    public class Gearbox : MonoBehaviour
    {
        private static List<Gear> gears = new List<Gear>();
        private static Gear currentGear;
        public TextMeshProUGUI gearSelected;
        public TextMeshProUGUI TextDents;
        private static Gear gearReculons = new Gear(30, 1, "Reculons");
        private static Gear gearMenante = new Gear(10, 1, "Menante");
        private static Gear gear1 = new Gear(30, 1, "Engrenage1");
        private static Gear gear2 = new Gear(26, 1, "Engrenage2");
        private static Gear gear3 = new Gear(22, 1, "Engrenage3");
        private static Gear gear4 = new Gear(18, 1, "Engrenage4");
        private static Gear gear5 = new Gear(14, 1, "Engrenage5");
        private static Gear gearLimitante = new Gear(5, 1, "Limitante");
        public Button buttonReculons;
        public Button buttonGearMenante;
        public Button buttonGear1;
        public Button buttonGear2;
        public Button buttonGear3;
        public Button buttonGear4;
        public Button buttonGearLimitante;
        public Slider dentsSlider;
        public GameObject gear10;
        public GameObject gear11;
        public GameObject gear12;
        public GameObject gear13;
        public GameObject gear14;
        public GameObject gear15;
        public GameObject gear16;
        public GameObject gear17;
        public GameObject gear18;
        public GameObject gear19;
        public GameObject gear20;
        public GameObject gear21;
        public GameObject gear22;
        public GameObject gear23;
        public GameObject gear24;
        public GameObject gear25;
        public GameObject gear26;
        public GameObject gear27;
        public GameObject gear28;
        public GameObject gear29;
        public GameObject gear30;
        private static List<GameObject> affichageGears = new List<GameObject>();


        public Gearbox(List<Gear> gearsSet)
        {
            gears = gearsSet;


        }

        void Start()
        {
            buttonReculons.onClick.AddListener(delegate { SwitchGearTo(0); setActiveGear();});
            buttonGearMenante.onClick.AddListener(delegate { SwitchGearTo(1); setActiveGear();});
            buttonGear1.onClick.AddListener(delegate { SwitchGearTo(2); setActiveGear(); });
            buttonGear2.onClick.AddListener(delegate { SwitchGearTo(3); setActiveGear(); });
            buttonGear3.onClick.AddListener(delegate { SwitchGearTo(4); setActiveGear(); });
            buttonGear4.onClick.AddListener(delegate { SwitchGearTo(5); setActiveGear(); });

            dentsSlider.onValueChanged.AddListener(delegate { setTextDents(); setActiveGear(); });
            initiationAffichageGears();
            addGearToList();
        }

        private void initiationAffichageGears()
        {
            affichageGears.Clear();
            for (int i = 10; i <= 30; i++)
            {
                string fieldName = "gear" + i;
                FieldInfo field = GetType().GetField(fieldName);
                affichageGears.Add((GameObject)field.GetValue(this));
            }
        }

        public static void addGearToList()
        {
            gears.Add(gearReculons);
            gears.Add(gearMenante);
            gears.Add(gear1);
            gears.Add(gear2);
            gears.Add(gear3);
            gears.Add(gear4);
            gears.Add(gear5);
            gears.Add(gearLimitante);

        }
        public void setActiveGear()
        {

            foreach (GameObject g in affichageGears) {
                if(currentGear != null && int.Parse(g.name.Substring(4)) == currentGear.NbDents)
                {
                    g.SetActive(true);
                } else
                {
                    g.SetActive(false);
                }
            }


        }
        public void SwitchGearTo(int gearIndex)
        {
            currentGear = gears[gearIndex];
            gearSelected.text = gears[gearIndex].Name;
            initialiserSlider(currentGear);
        }

        public void setTextDents()
        {

            if (currentGear != null)
            {
                gears.Find((x) => x.Name == currentGear.Name).NbDents = (int)dentsSlider.value;
            }
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

        public static Gear Gears(int index)
        {
           return gears[index];
        }

        public static List<Gear> GearsList()
        {
            return gears;
        }

        public static double calculateRatio(Gear menant, Gear menee, Boolean recule)
        {
            double ratio = ((double)(menant.NbDents)/(double)(menee.NbDents));

            if(recule) 
            {
                ratio *= (-1) ;
            }

            return ratio;
        }

        public void initialiserSlider(Gear current)
        {
           
                dentsSlider.SetValueWithoutNotify(current.NbDents);
            
        }
        

        void Update()
        {
            if (currentGear != null)
            {
                TextDents.text = currentGear.NbDents.ToString() + " dents";
                foreach (GameObject g in affichageGears)
                {
                    if (g != null)
                    {
                        g.transform.Rotate(new Vector3(0.25f, 1, 0.5f), 100 * Time.deltaTime);
                    }
                }
            }
        }
    }
}