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
        public Slider RpmSlider;
        private int gearSelected = 0;//gearSelected
        public TextMeshProUGUI ValueText;
        private static int RPMmax = 9000;
        private static double RPM = 0;


        public PerfCalc(Car car)
        {
            this.car = car;
        }

        public double getSpeed(double rpm)
        {
            return 0;
        }

        public double getAcceleration(double input)
        {
            return 0;
        }

        void Start()
        {
          
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.W) )
            {
                if (RPM <= RPMmax)
                {
                    RPM += 8;
                }
            }
            else if (RPM >= 0)
            {
                //RPM -= 1;
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
                + "\nSlider:" + RpmSlider.value.ToString();

        }
    }
}