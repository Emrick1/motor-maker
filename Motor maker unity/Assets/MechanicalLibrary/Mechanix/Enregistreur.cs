using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe permettant la sauvegarde d'une voiture.</c>
    /// </summary>
    public class Enregistreur : MonoBehaviour
    {

        /// <summary>
        /// Charge l'instance d'une voiture sauvegardée dans un fichier.
        /// </summary>
        /// <param name="path">Nom du fichier à utiliser.</param>
        /// <returns>L'instance de la voiture sauvegardée.</returns>
        public static Car LoadSettings(string path)
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

        /// <summary>
        /// Sauvegarde une instance de voiture dans un fichier.
        /// </summary>
        /// <param name="path">Nom du fichier à utiliser.</param>
        /// <param name="car">Instance de voiture à sauvegarder.</param>
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