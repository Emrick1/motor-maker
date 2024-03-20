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
            Quaternion deltaRotationLeft = Quaternion.Euler(new Vector3(0, 20, 0) * Time.fixedDeltaTime);
            Quaternion deltaRotationRight = Quaternion.Euler(new Vector3(0, -20, 0) * Time.fixedDeltaTime);

            
            var vel = rbVector * (float) PerfCalc.Speed;
            vel.y = _rb.velocity.y;
            _rb.velocity = vel;
            int axis = 0;
            if (Input.GetKey(KeyCode.A))
            {
                _rb.MoveRotation(_rb.rotation * deltaRotationRight);
                carCollider.transform.rotation = _rb.rotation * deltaRotationLeft;
                axis = -1;
                rbVector.y += 0.05f;
            } else if (Input.GetKey(KeyCode.D))
            {
                _rb.MoveRotation(_rb.rotation * deltaRotationLeft);
                carCollider.transform.rotation = _rb.rotation * deltaRotationRight;
                axis = 1;
                rbVector.y -= 0.05f;
            }

            frontLeft.steerAngle = 15f * axis;
            frontRight.steerAngle = 15f * axis;



            UpdateWheel(frontLeft, frontLeftTransform);
            UpdateWheel(frontRight, frontRightTransform);
            UpdateWheel(rearLeft, rearLeftTransform);
            UpdateWheel(rearRight, rearRightTransform);
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
