using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
using static Mechanix.Wheels;
using static Mechanix.Gearbox;
using static Mechanix.Gear;
using System.Collections.Generic;
using System;

namespace Mechanix
{
    public class PerfCalc : MonoBehaviour
    {
        private Car car;
        public Slider PressureSlider;
        public static Gear gearSelected;//gearSelected
        public TextMeshProUGUI ValueText;
        private static int RPMmax = 9000;
        private static int RPMmin = 600;
        private static double RPM = 0;
        private static double RPMOut = 0;
        private static float facteurAugmentation;
        private static float t;

        public PerfCalc(Car car)
        {
            this.car = car;
        }

        public double getRPM()
        {
            return RPM;
        }

        public double getAcceleration()
        {
            return 0;
        }

        void Start()
        {
            Wheels.WheelsSetValues(1, 1, 1, 1, 180, 300, 200, 3000);
            Gearbox.addGearToList();
            List<Gear> gears = Gearbox.GearsList();
            gearSelected = gears[1];
        }

        void Update()
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (RPM < RPMmax)
                {
                    facteurAugmentation = (19 / (1 + (Mathf.Exp((-0.1f * t) + 5))) + 1);
                    RPM += (int)facteurAugmentation;
                    t += 0.05f;
                }
                else
                {
                    RPM = RPMmax;
                }
            }
            else if (RPM > RPMmin)
            {
                t = 0;
                RPM -= 1;
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    RPM = (int)((RPM * 0.999) - 2);
                }
            }
            else if (RPM < RPMmin)
            {
                RPM = RPMmin;
            }

            
            for (int i = 1; i <= Gearbox.GearsList().Count - 1; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    gearSelected = Gearbox.Gears(i);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && gearSelected != Gearbox.Gears(1))
                {
                    gearSelected = Gearbox.Gears((Gearbox.GearsList().IndexOf(gearSelected)) - 1);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && gearSelected != Gearbox.Gears(Gearbox.GearsList().Count - 2));
                {
                    gearSelected = Gearbox.Gears((Gearbox.GearsList().IndexOf(gearSelected)) + 1);
                }
            }

            RPMOut = (int)(((double)RPM) * (calculateRatio(Gearbox.Gears(0), gearSelected, false)));
            

            Wheels.Pressure = PressureSlider.value;
            Wheels.CalculateTyreFriction();

            ValueText.text = "Stats :"
                + "\nRPM:" + RPM.ToString()
                + "\nRPM Output:" + RPMOut.ToString()
                + "\nGear:" + gearSelected.Name.ToString()
                + "\nSlider:" + PressureSlider.value.ToString()
                + DictionnaryToString(Wheels.GetInfosWheels); 
        }

        private string DictionnaryToString(Dictionary<string, double> dictionary)
        {
            string returnString = "\nWheels :";
            foreach (KeyValuePair<string, double> pair in dictionary)
            {
                returnString += "\n" + pair.Key + " : " + pair.Value;
            }
            return returnString;
        }
    }
}