using Mechanix;
using System;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe qui d�crit le mouvement d'une voiture dans le moteur graphique</c>
    /// </summary>
    public class CarMovement : MonoBehaviour
    {
        /// <summary>
        /// Sp�cifie la pr�sence physique de la voiture simul�e.
        /// </summary>
        [SerializeField] private Rigidbody _rb;
        /// <summary>
        /// Texte affich� pour retourner la voiture.
        /// </summary>
        [SerializeField] public TextMeshProUGUI flippedText;
        /// <summary>
        /// Zone de texte affichant la vitesse.
        /// </summary>
        [SerializeField] public TextMeshProUGUI speedText;
        [SerializeField] public TextMeshProUGUI RPMText;

        /// <summary>
        /// Texte pour les controles du jeu
        /// </summary>
        [SerializeField] public TextMeshProUGUI wasdText;
        [SerializeField] public TextMeshProUGUI arrowkeysText;
        [SerializeField] public TextMeshProUGUI arrowkeysText2;
        [SerializeField] public TextMeshProUGUI currentGearText;
        /// <summary>
        /// Zone de texte affich� pour retourner la voiture.
        /// </summary>
        [SerializeField] public GameObject flippedpanel;
        /// <summary>
        /// Compteur de vitesse de la voiture.
        /// </summary>
        [SerializeField] public GameObject SpeedometerArrow;
        [SerializeField] public GameObject RPMArrow;
        /// <summary>
        /// Vecteur de v�locit� angulaire.
        /// </summary>
        [SerializeField] private Vector3 m_EulerAngleVelocity;
        /// <summary>
        /// Collider de la roue avant droite.
        /// </summary>
        [SerializeField] private WheelCollider frontRight;
        /// <summary>
        /// Collider de la rouye avant gauche.
        /// </summary>
        [SerializeField] private WheelCollider frontLeft;
        /// <summary>
        /// Collider de la roue arri�re droite.
        /// </summary>
        [SerializeField] private WheelCollider rearRight;
        /// <summary>
        /// Collider de la roue arri�re gauche.
        /// </summary>
        [SerializeField] private WheelCollider rearLeft;
        /// <summary>
        /// Collider de la voiture.
        /// </summary>
        [SerializeField] private BoxCollider carCollider;
        /// <summary>
        /// �tat de la rotation de la roue avant droite.
        /// </summary>
        [SerializeField] private Transform frontRightTransform;
        /// <summary>
        /// �tat de la rotation de la roue avant gauche.
        /// </summary>
        [SerializeField] private Transform frontLeftTransform;
        /// <summary>
        /// �tat de la roation de la roue arri�re droite.
        /// </summary>
        [SerializeField] private Transform rearRightTransform;
        /// <summary>
        /// �tat de la rotation de la roue arri�wre gauche.
        /// </summary>
        [SerializeField] private Transform rearLeftTransform;


        /// <summary>
        /// Vecteur utilis�e dans l'actualisationde la voiture.
        /// </summary>
        [SerializeField] private Vector3 rbVector = new Vector3(0, 0, 1);

        void Start()
        {
            flippedText.enabled = false;
            flippedpanel.SetActive(false);
            PerfCalc.Speed = 0;
            PerfCalc.GetRPM = 0;
        }

        void Update()
        {
           
            Quaternion deltaRotationLeft = Quaternion.Euler(new Vector3(0, 2, 0) * Time.fixedDeltaTime);
            Quaternion deltaRotationRight = Quaternion.Euler(new Vector3(0, -2, 0) * Time.fixedDeltaTime);
            setRbVector();

            var vel = rbVector * (float)PerfCalc.Speed;
            vel.y = _rb.velocity.y;
            _rb.velocity = vel;
            int axis = 0;
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                if (PerfCalc.Speed > 0.5 || PerfCalc.Speed < -0.5)
                {
                    _rb.MoveRotation(_rb.rotation * deltaRotationRight);
                }
                axis = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (PerfCalc.Speed > 0.5 || PerfCalc.Speed < -0.5)
                {
                    _rb.MoveRotation(_rb.rotation * deltaRotationLeft);
                }
                axis = 1;
            }

            if (Input.GetKey(KeyCode.Tab))
            {
                AfficherControles(true);
            }
            else AfficherControles(false);

            if (_rb.rotation.z * 360 >= 120 || _rb.rotation.z * 360 <= -120 || _rb.rotation.x * 360 >= 120 || _rb.rotation.x * 360 <= -120)
            {
                flippedText.enabled = true;
                flippedpanel.SetActive(true);
                if (Input.GetKey(KeyCode.Space))
                {
                    _rb.rotation = Quaternion.Euler(new Vector3(0, _rb.rotation.y * 360, 0));
                    _rb.angularVelocity = new Vector3(0, 0, 0);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.Tab))
                {
                    flippedText.enabled = false;
                    flippedpanel.SetActive(false);
                }
            }



            if (!PerfCalc.VolantToggleBool)
            {
                if (PerfCalc.Speed >= 0)
                {
                    frontLeft.steerAngle = 5f * axis;
                    frontRight.steerAngle = 5f * axis;
                }
                else
                {
                    frontLeft.steerAngle = -5f * axis;
                    frontRight.steerAngle = -5f * axis;
                }

            }
            else
            {
                LogitechGSDK.DIJOYSTATE2ENGINES rec;
                rec = LogitechGSDK.LogiGetStateUnity(0);
                float x = rec.lX;
                frontLeft.steerAngle = 1f * (x / 32764);
                frontRight.steerAngle = 1f * (x / 32764);
                if (PerfCalc.Speed > 1 || PerfCalc.Speed < -1)
                {
                    _rb.MoveRotation(_rb.rotation * Quaternion.Euler(new Vector3(0, x / 3000, 0) * Time.fixedDeltaTime));
                }
            }

            UpdateWheel(frontLeft, frontLeftTransform);
            UpdateWheel(frontRight, frontRightTransform);
            UpdateWheel(rearLeft, rearLeftTransform);
            UpdateWheel(rearRight, rearRightTransform);

            UpdateSpeedometer();
        }
     

        /// <summary>
        /// Actualise le compteur de vitesse de la voiture.
        /// </summary>
        private void UpdateSpeedometer()
        {
            currentGearText.text = "Vitesse Selectionnée:" + "\n " + PerfCalc.gearSelected.Name.ToString();
            SpeedometerArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, ((float) (PerfCalc.Speed * -2.8) + 8)));
            double speed = (PerfCalc.Speed * 3.6);
            speedText.text = "Vitesse" + $"\n{speed:F0}";
            RPMArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, ((float)(PerfCalc.GetRPM / -62) + 8)));
            double RPM = PerfCalc.GetRPM;
            RPMText.text = "RPM x1000" + $"\n{RPM:F0}";
        }

        /// <summary>
        /// Actualise le compteur de vitesse de la voiture.
        /// </summary>
        private void AfficherControles(bool afficher)
        {
            wasdText.enabled = afficher;
            arrowkeysText.enabled = afficher;
            arrowkeysText2.enabled = afficher;
            flippedpanel.SetActive(afficher);
        }

        /// <summary>
        /// Ajuste le vecteur de d�placement de la voiture.
        /// </summary>
        private void setRbVector()
        {
            Vector3 position;
            Quaternion rotation;
            frontLeft.GetWorldPose(out position, out rotation);
            Vector3 position2;
            Quaternion rotation2;
            rearLeft.GetWorldPose(out position2, out rotation2);
            rbVector = position - position2 - new Vector3(0,0.5f,0);
        }

        /// <summary>
        /// Actualise la rotation des roues de la voiture.
        /// </summary>
        /// <param name="col">Collider � utiliser.</param>
        /// <param name="trans">Transformateur � utiliser.</param>
        private void UpdateWheel(WheelCollider col, Transform trans)
        {
            Vector3 position;
            Quaternion rotation;
            col.GetWorldPose(out position, out rotation);

            trans.position = position;
            trans.rotation = rotation;
        }
    }
}
