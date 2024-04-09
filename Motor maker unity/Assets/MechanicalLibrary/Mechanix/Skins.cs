using UnityEngine;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe spécifiant la couleur d,une voiture.</c>
    /// </summary>
    public class Skins : MonoBehaviour
    {
        /// <summary>
        /// Couleur de la voiture.
        /// </summary>
        private Color color;

        /// <summary>
        /// Construit un visuel de voiture de couleur grise.
        /// </summary>
        public Skins()
        {
            color = Color.grey;
        }

        /// <summary>
        ///Construit un visuel de voiture avec la couleur spécifiée.
        /// </summary>
        /// <param name="color">Couleur voulue pour la voiture.</param>
        public Skins(Color color)
        {
            this.color = color;
        }

        public Color Color
        {
            get => color;
            set => color = value;
        }
    }
}