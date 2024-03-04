using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanix
{
    public class Wheels : MonoBehaviour, IMechanicalPiece
    {
        private static double mass;
        private static double asphaltAdherence; //coefficient entre 0 et 1
        private static double dirtAdherence; //coefficient entre 0 et 1
        private static double sandAdherence; //coefficient entre 0 et 1
        private static double generalAdherence; //coefficient entre 0 et 1
        private static double pressure = 100;
        private static double contactArea;
        private static double radialRigidity;
        private static double radialTyreDeflexion;
        private static double radius;
        private static double width;
        private static double frictionForce;
        private static double normalForce;
        private static double carLoad;
        public Slider PressureSlider;
        public TextMeshProUGUI WheelsStats;
        public Button WheelType1;
        public Button WheelType2;
        public Button WheelType3;
        public Button WheelType4;
        public Button WheelType5;

        public static void WheelsSetValues(double asphaltAdherenceSet,
                      double dirtAdherenceSet,
                      double sandAdherenceSet,
                      double generalAdherenceSet,
                      double radialRigiditySet,
                      double radiusSet,
                      double widthSet,
                      double massSet)
        {
            radialRigidity = radialRigiditySet;
            radius = radiusSet;
            width = widthSet;
            mass = massSet;
            carLoad = mass * 9.81;
            asphaltAdherence = asphaltAdherenceSet;
            dirtAdherence = dirtAdherenceSet;
            sandAdherence = sandAdherenceSet;
            generalAdherence = generalAdherenceSet;
            UpdateGeneralAdherence();
            CalculateTyreFriction();
        }

        public static double Mass
        {
            get => mass;
            set => mass = value;
        }

        public static double AsphaltAdherence
        {
            get => asphaltAdherence;
            set
            {
                asphaltAdherence = value;
                UpdateGeneralAdherence();
            }
        }

        public static double DirtAdherence
        {
            get => dirtAdherence;
            set
            {
                dirtAdherence = value;
                UpdateGeneralAdherence();
            }
        }

        public static double SandAdherence
        {
            get => sandAdherence;
            set
            {
                sandAdherence = value;
                UpdateGeneralAdherence();
            }
        }

        public static double GeneralAdherence
        {
            get => generalAdherence;
            set => generalAdherence = value;
        }

        public static double Pressure
        {
            get => pressure;
            set => pressure = value;
        }

        public static Dictionary<string, double> GetInfosWheels
        {
            get => new Dictionary<string, double> {
                { "Pressure", pressure },
                { "Mass", mass },
                { "Car Load", carLoad },
                { "Contact Area", contactArea },
                { "Radial Rigidity", radialRigidity },
                { "RadialTyreDeflexion", radialTyreDeflexion },
                { "Radius", radius },
                { "Width", width },
                { "Normal Force", normalForce },
                { "Friction Force", frictionForce }};
        }

        public static void UpdateGeneralAdherence()
        {
            if (asphaltAdherence != null && dirtAdherence != null && sandAdherence != null)
            {
                generalAdherence = (asphaltAdherence + dirtAdherence + sandAdherence) / 3;
            }
            else
            {
                generalAdherence = 1;
            }
        }

        public static void CalculateTyreFriction()
        {
            radialTyreDeflexion = ((carLoad / 4) / radialRigidity);
            contactArea = (1 / Wheels.Pressure) * (width * (1.4 * Math.Sqrt(radialTyreDeflexion * ((2 * radius) - radialTyreDeflexion)))) / 10000;
            normalForce = contactArea * (carLoad / 4);
            frictionForce = normalForce * 0.7; // TODO : general adherance? mu
        }

        void Start()
        {
            Wheels.WheelsSetValues(1, 1, 1, 1, 180, 300, 200, 3000);

            WheelType1.onClick.AddListener(delegate { Wheels.WheelsSetValues(1, 1, 1, 1, 180, 300, 200, 3000); });
            WheelType2.onClick.AddListener(delegate { Wheels.WheelsSetValues(1, 1, 1, 1, 200, 310, 200, 3000 + 15); });
            WheelType3.onClick.AddListener(delegate { Wheels.WheelsSetValues(1, 1, 1, 1, 250, 350, 220, 3000 + 30); });
            WheelType4.onClick.AddListener(delegate { Wheels.WheelsSetValues(1, 1, 1, 1, 160, 250, 230, 3000 - 10); });
            WheelType5.onClick.AddListener(delegate { Wheels.WheelsSetValues(1, 1, 1, 1, 150, 220, 250, 3000 - 20); });
        }

        private void Update()
        {
            Wheels.Pressure = PressureSlider.value;
            Wheels.CalculateTyreFriction();
            WheelsStats.text = PerfCalc.DictionnaryToString(Wheels.GetInfosWheels);
        }
    }
}