using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

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
        private static Wheels wheels = new Wheels(1, 1, 1, 1, 200, 300, 300, 3000);


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
            
        }

        void Update()
        {
            wheels.Pressure = PressureSlider.value;
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

            ValueText.text = "Stats :"
                + "\nRPM:" + RPM.ToString()
                + "\nSlider:" + PressureSlider.value.ToString()
                + "\nPressure:" + wheels.Pressure.ToString();
        }
    }
}