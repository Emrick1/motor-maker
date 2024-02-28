using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Mechanix
{
    public class Enregistreur : MonoBehaviour
    {

        /*
        private void LoadSettingsV1()
        {
            ArrayList _settings = new ArrayList();
            string line;
            Dictionary<string, double> settingsEngine = new Dictionary<string, double>();
            Dictionary<string, double> settingsGearbox = new Dictionary<string, double>();
            ArrayList _gearsList = new ArrayList();
            Dictionary<string, double> settingsSkins = new Dictionary<string, double>();
            Dictionary<string, double> settingsTransmission = new Dictionary<string, double>();
            Dictionary<string, double> settingsWheels = new Dictionary<string, double>();
            _settings.Add(settingsEngine);
            _settings.Add(settingsGearbox);
            _settings.Add(settingsTransmission);
            _settings.Add(settingsSkins);
            _settings.Add(settingsWheels);
            try
            {
                StreamReader sr = new StreamReader("Mechanix/Settings.txt");
                line = sr.ReadLine();
                if (line == "Engine")
                {
                    while (line != "Gears")
                {
                    line = sr.ReadLine();
                    string[] tabSetting = line.Split('=');
                    settingsEngine.Add(tabSetting[0], Convert.ToDouble(tabSetting[1]));
                }

                    sr.ReadLine();
                    while (line != "Gearbox")
                    {
                        line = sr.ReadLine();
                        ArrayList GearList = new ArrayList {line.Split(',')};
                        _settings.Add(GearList);
                    }
                    while (line != "Transmission")
                    {
                        line = sr.ReadLine();
                        string[] tabSetting = line.Split('=');
                        settingsEngine.Add(tabSetting[0], Convert.ToDouble(tabSetting[1]));
                    }
                    sr.ReadLine();
                    while (line != "Wheels")
                    {
                        line = sr.ReadLine();
                        string[] tabSetting = line.Split('=');
                        settingsEngine.Add(tabSetting[0], Convert.ToDouble(tabSetting[1]));
                    }
                    sr.ReadLine();
                    while (line != "Skins")
                    {
                        line = sr.ReadLine();
                        string[] tabSetting = line.Split('=');
                        settingsEngine.Add(tabSetting[0], Convert.ToDouble(tabSetting[1]));
                    }
                    sr.ReadLine();
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        string[] tabSetting = line.Split('=');
                        settingsEngine.Add(tabSetting[0], Convert.ToDouble(tabSetting[1]));
                    }
                }
                
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        */

        public static Car LoadSettingsV2(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Car car = new Car();
            FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read);

            try
            {
                car = (Car)formatter.Deserialize(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return car;
        }

        public static void SaveCar(string path, Car car)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream writer = new FileStream(path, FileMode.Create, FileAccess.Write);

            try
            { 
                formatter.Serialize(writer, car); 
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}