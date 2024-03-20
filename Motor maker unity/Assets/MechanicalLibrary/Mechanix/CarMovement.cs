using Mechanix;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Mechanix
{

     public class CarMovement : MonoBehaviour
    {

        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Vector3 m_EulerAngleVelocity;
        void Start()
        {
            m_EulerAngleVelocity = new Vector3(0, 100, 0);
        }

        void Update()
        {
            var vel = new Vector3(0, 0, 1) * (float) PerfCalc.Speed;
            vel.y = _rb.velocity.y;
            _rb.velocity = vel;
            if (Input.GetKey(KeyCode.A))
            {
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
                _rb.MoveRotation(_rb.rotation * deltaRotation);
            }
        }
    }
}
