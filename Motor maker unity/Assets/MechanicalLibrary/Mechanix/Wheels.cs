﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanix
{
    public class Wheels : MonoBehaviour
    {
        private static double mass;
        private static double roadAdherence; //coefficient entre 0 et 1
        private static double dirtAdherence; //coefficient entre 0 et 1
        private static double waterAndSnowAdherence; //coefficient entre 0 et 1
        private static double pressure = 70;
        private static double minPressure = 69;
        private static double contactArea;
        private static double radialRigidity;
        private static double radialTyreDeflexion;
        private static double radius;
        private static double width;
        private static double frictionForce;
        private static double normalForce;
        private static double carLoad;
        private static int selectedWheelType = 0;
        public Slider PressureSlider;
        public TextMeshProUGUI WheelsStats;
        public Button WheelType1;
        public Button WheelType2;
        public Button WheelType3;
        public Button WheelType4;
        public Button WheelType5;
        public GameObject PneuSport;
        public GameObject PneuToutTerrain;
        public GameObject PneuEte;
       // public GameObject PneuCourse;

        public static void WheelsSetValues(double roadAdherenceSet,
                      double dirtAdherenceSet,
                      double waterAndSnowAdherenceSet,
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
            roadAdherence = roadAdherenceSet;
            dirtAdherence = dirtAdherenceSet;
            waterAndSnowAdherence = waterAndSnowAdherenceSet;
            CalculateTyreFriction();
        }

        public static double Mass
        {
            get => mass;
            set => mass = value;
        }

        public static double Pressure
        {
            get => pressure;
            set => pressure = value;
        }

        public static double FrictionForce
        {
            get => frictionForce;
            set => frictionForce = value;
        }

        public static int SelectedWheelType
        {
            get => selectedWheelType;
            set => selectedWheelType = value;
        }

        public static double Radius
        {
            get => radius;
            set => radius = value;
        }

        public static Dictionary<string, double> GetInfosWheels
        {
            get => new Dictionary<string, double> {
                { "Selected Wheel Type", (double) selectedWheelType},
                { "Pressure (psi)", pressure },
                { "Mass (kg)", mass },
                { "Car Load (N)", carLoad },
                { "Contact Area (cm^2)", contactArea },
                { "Radial Rigidity", radialRigidity },
                { "RadialTyreDeflexion (mm)", radialTyreDeflexion },
                { "Radius (mm)", radius },
                { "Width (mm)", width },
                { "Normal Force (N)", normalForce },
                { "Friction Force (N)", frictionForce }};
        }

        public static void CalculateTyreFriction()
        {
            radialTyreDeflexion = ((carLoad / 4) / radialRigidity);
            contactArea = ((1 / Wheels.Pressure) + (1 / minPressure)) * (width * (1.4 * Math.Sqrt(radialTyreDeflexion * ((2 * radius) - radialTyreDeflexion)))) / 100000;
            normalForce = contactArea * carLoad;
            frictionForce = normalForce * roadAdherence; // TODO : general adherance? mu
        }

        public void desactiverModelPneus()
        {
            PneuSport.SetActive(false);
            PneuToutTerrain.SetActive(false);
            PneuEte.SetActive(false);
           // PneuCourse.SetActive(false);
        }

        void Start()
        {
            if (selectedWheelType == 0) 
            {
                minPressure = PressureSlider.minValue - 1;
                Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, PerfCalc.Mass);
                selectedWheelType = 1;
            }

            WheelType1.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.65, 0.55, 0.5, 1, 140, 300, 200, 3000);
                selectedWheelType = 1;
                desactiverModelPneus();
                PneuEte.SetActive(true);
            });
            WheelType2.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.7, 0.6, 0.5, 1, 100, 310, 200, 3000 + 15);
                selectedWheelType = 2;
                desactiverModelPneus();
                PneuSport.SetActive(true);
            });
            WheelType3.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.8, 0.7, 0.6, 1, 80, 350, 220, 3000 + 30);
                selectedWheelType = 3;
                desactiverModelPneus();
                PneuToutTerrain.SetActive(true);
            });
            WheelType4.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.6, 0.5, 0.4, 1, 165, 250, 230, 3000 - 10);
                selectedWheelType = 4;
            });
            WheelType5.onClick.AddListener(delegate { 
                Wheels.WheelsSetValues(0.5, 0.4, 0.3, 1, 180, 220, 250, 3000 - 20);
                selectedWheelType = 5;
                desactiverModelPneus();
              //  PneuCourse.SetActive(true);
            });
        }

        private void Update()
        {
            Wheels.Pressure = PressureSlider.value;
            Wheels.CalculateTyreFriction();
            WheelsStats.text = "Wheels' Stats:" +
                PerfCalc.DictionnaryToString(Wheels.GetInfosWheels)
                + getAdherenceString();

           foreach(GameObject pneu in FindObjectsOfType<GameObject>())
            {
                if (pneu != null && pneu.name.Substring(0,4).Equals("Pneu") && pneu.activeSelf == true)
                {
                   pneu.transform.Rotate(new Vector3(0.25f, 1, 0.5f), 100 * Time.deltaTime);
                }
            }
        }

        public static string getAdherenceString()
        {
            string adherance = "\nAdherence: Road " + roadAdherence + " Dirt " + dirtAdherence + " \nWet Road/Snow " + waterAndSnowAdherence;
            return adherance;
        }
    }
}