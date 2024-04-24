using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.UIElements;

namespace Mechanix
{
    public class StatWindow : EditorWindow
    {
        private VisualElement root;
        private UnityEngine.UIElements.Label text;
        private string texte;

        [MenuItem("Window/Statistiques")]
        static void Init()
        {
            GetWindow<StatWindow>("StatWindow");
        }

        void CreateGUI()
        {
            text = new UnityEngine.UIElements.Label("Statistiques");
            rootVisualElement.Add(text);
        }

        void Update()
        {
            text.schedule.Execute(() =>
            {
                text.text = texte;
            }).Every(100);
        }

        public void UpdateTexte(string newTexte)
        {
            texte = newTexte;
        }
    }
}
