using Mechanix;
using System;
using System.Numerics;
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
                if (PerfCalc.Speed >= 1)
                {
                    _rb.MoveRotation(_rb.rotation * deltaRotationRight);
                }
                axis = -1;
            } else if (Input.GetKey(KeyCode.D))
            {
                if (PerfCalc.Speed >= 1)
                {
                    _rb.MoveRotation(_rb.rotation * deltaRotationLeft);
                }
                axis = 1;
            }

            frontLeft.steerAngle = 15f * axis;
            frontRight.steerAngle = 15f * axis;

            UpdateWheel(frontLeft, frontLeftTransform);
            UpdateWheel(frontRight, frontRightTransform);
            UpdateWheel(rearLeft, rearLeftTransform);
            UpdateWheel(rearRight, rearRightTransform);
        }

        private void setRbVector()
        {
            Vector3 position;
            Quaternion rotation;
            frontLeft.GetWorldPose(out position, out rotation);
            Vector3 position2;
            Quaternion rotation2;
            rearLeft.GetWorldPose(out position2, out rotation2);
            rbVector = position - position2;
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
