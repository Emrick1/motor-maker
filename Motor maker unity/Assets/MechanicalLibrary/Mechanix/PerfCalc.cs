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
        private int gearSelected = 0;//gearSelected
        public TextMeshProUGUI ValueText;
        private static int RPMmax = 9000;
        private static double RPM = 0;

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
        /*
        public double RPMOutput() {
         double RpmOutPut;
         RpmOutPut = RPM * calculateRatio(Gear 1, Gear 2, false);
            return RpmOutPut;
        }
        */

        void Start()
        {
            Wheels.WheelsSetValues(1, 1, 1, 1, 180, 300, 200, 3000);
        }

        void Update()
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                if (RPM < RPMmax)
                {
                    RPM += 8;
                }
            }
            else if (RPM > 0)
            {
                RPM -= 1;
                if (Input.GetKey(KeyCode.S))
                {
                    RPM -= 8;
                }
            }
            else if (RPM < 0)
            {
                RPM = 0;
            }

            Wheels.Pressure = PressureSlider.value;
            Wheels.CalculateTyreFriction();

            ValueText.text = "Stats :"
                + "\nRPM:" + RPM.ToString()
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