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
            /// Charge l'instance d'un objet sauvegardé dans un fichier.
            /// </summary>
            /// <param name="path">Nom du fichier à utiliser.</param>
            /// <returns>L'instance de la voiture sauvegardée.</returns>
            public static object Load(string path)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                object obj = null;
                FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read);

                try
                {
                    obj = formatter.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return obj;
            }

            /// <summary>
            /// Sauvegarde une instance d'un object dans un fichier.
            /// </summary>
            /// <param name="path">Nom du fichier à utiliser.</param>
            /// <param name="car">Instance de l'object à sauvegarder.</param>
            public static void Save(object obj, string path)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream writer = new FileStream(path, FileMode.Create, FileAccess.Write);

                try
                {
                    formatter.Serialize(writer, obj);
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
