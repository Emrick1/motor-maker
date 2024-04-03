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

     public class CarMovement : MonoBehaviour
    {

        [SerializeField] private Rigidbody _rb;
        [SerializeField] public TextMeshProUGUI flippedText;
        [SerializeField] public TextMeshProUGUI speedText;
        [SerializeField] public GameObject flippedpanel;
        [SerializeField] public GameObject SpeedometerArrow;
        [SerializeField] private Vector3 m_EulerAngleVelocity;
        [SerializeField] private WheelCollider frontRight;
        [SerializeField] private WheelCollider frontLeft;
        [SerializeField] private WheelCollider rearRight;
        [SerializeField] private WheelCollider rearLeft;
        [SerializeField] private BoxCollider carCollider;
        [SerializeField] private Transform frontRightTransform;
        [SerializeField] private Transform frontLeftTransform;
        [SerializeField] private Transform rearRightTransform;
        [SerializeField] private Transform rearLeftTransform;
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
            
            var vel = rbVector * (int) PerfCalc.Speed;
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
                _rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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

        private void UpdateSpeedometer()
        {
            SpeedometerArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, ((float) (PerfCalc.Speed * -2.8) + 8)));
            speedText.text = ((int) PerfCalc.Speed * 3.6).ToString();
        }

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
