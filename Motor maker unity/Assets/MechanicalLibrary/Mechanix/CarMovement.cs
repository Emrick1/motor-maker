using Mechanix;
using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Mechanix
{
    /// <summary>
    /// <c>Classe qui décrit le mouvement d'une voiture dans le moteur graphique</c>
    /// </summary>
     public class CarMovement : MonoBehaviour
    {
        /// <summary>
        /// Spécifie la présence physique de la voiture simulée.
        /// </summary>
        [SerializeField] private Rigidbody _rb;
        /// <summary>
        /// Texte affiché pour retourner la voiture.
        /// </summary>
        [SerializeField] public TextMeshProUGUI flippedText;
        /// <summary>
        /// Zone de texte affichant la vitesse.
        /// </summary>
        [SerializeField] public TextMeshProUGUI speedText;
        /// <summary>
        /// Zone de texte affiché pour retourner la voiture.
        /// </summary>
        [SerializeField] public GameObject flippedpanel;
        /// <summary>
        /// Compteur de vitesse de la voiture.
        /// </summary>
        [SerializeField] public GameObject SpeedometerArrow;
        /// <summary>
        /// Vecteur de vélocité angulaire.
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
        /// Collider de la roue arrière droite.
        /// </summary>
        [SerializeField] private WheelCollider rearRight;
        /// <summary>
        /// Collider de la roue arrière gauche.
        /// </summary>
        [SerializeField] private WheelCollider rearLeft;
        /// <summary>
        /// Collider de la voiture.
        /// </summary>
        [SerializeField] private BoxCollider carCollider;
        /// <summary>
        /// État de la rotation de la roue avant droite.
        /// </summary>
        [SerializeField] private Transform frontRightTransform;
        /// <summary>
        /// État de la rotation de la roue avant gauche.
        /// </summary>
        [SerializeField] private Transform frontLeftTransform;
        /// <summary>
        /// État de la roation de la roue arrière droite.
        /// </summary>
        [SerializeField] private Transform rearRightTransform;
        /// <summary>
        /// État de la rotation de la roue arrièwre gauche.
        /// </summary>
        [SerializeField] private Transform rearLeftTransform;
        /// <summary>
        /// Vecteur utilisée dans l'actualisationde la voiture.
        /// </summary>
        [SerializeField] private Vector3 rbVector = new Vector3(0, 0, 1);

        void Start()
        {
            flippedText.enabled = false;
            flippedpanel.SetActive(false);
        }

        void Update()
        {
            Quaternion deltaRotationLeft = Quaternion.Euler(new Vector3(0, 2, 0) * Time.fixedDeltaTime);
            Quaternion deltaRotationRight = Quaternion.Euler(new Vector3(0, -2, 0) * Time.fixedDeltaTime);
            setRbVector();
            
            var vel = rbVector * (float) PerfCalc.Speed;
            vel.y = _rb.velocity.y;
            _rb.velocity = vel;
            int axis = 0;
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                if (PerfCalc.Speed > 0)
                {
                    _rb.MoveRotation(_rb.rotation * deltaRotationRight);
                }
                axis = -1;
            } else if (Input.GetKey(KeyCode.D))
            {
                if (PerfCalc.Speed > 0)
                {
                    _rb.MoveRotation(_rb.rotation * deltaRotationLeft);
                }
                axis = 1;
            }
            if (_rb.rotation.z * 360 >= 120 || _rb.rotation.z * 360 <= -120 || _rb.rotation.x * 360 >= 120 || _rb.rotation.x * 360 <= -120)
            {
                flippedText.enabled = true;
                flippedpanel.SetActive(true);
                if (Input.GetKey(KeyCode.Space))
                {
                    _rb.rotation = Quaternion.Euler(new Vector3(0, _rb.rotation.y * 360, 0));                    
                    _rb.angularVelocity = new Vector3(0, 0, 0);
                }
            } else
            {
                flippedText.enabled = false;
                flippedpanel.SetActive(false);
            }

            frontLeft.steerAngle = 15f * axis;
            frontRight.steerAngle = 15f * axis;

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
            SpeedometerArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, ((float) (PerfCalc.Speed * -2.8) + 8)));
            double speed = (PerfCalc.Speed * 3.6);
            speedText.text = $"{speed:F2}";
        }

        /// <summary>
        /// Ajuste le vecteur de déplacement de la voiture.
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
        /// <param name="col">Collider à utiliser.</param>
        /// <param name="trans">Transformateur à utiliser.</param>
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
