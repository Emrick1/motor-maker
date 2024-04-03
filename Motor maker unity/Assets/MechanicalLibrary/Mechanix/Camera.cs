using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private Vector3 localRot;
    [SerializeField] public Vector3 distanceCamera;
    [SerializeField] public float mouseSpeed = 3;
    [SerializeField] private float mouseDampening = 10;
    [SerializeField] Transform Player;


    void Start()
    {
        
    }

    void Update()
    {
        
        localRot.y = Player.rotation.y;
        localRot.x = Player.rotation.x;

        localRot.x += Input.GetAxis("Mouse X") * mouseSpeed;
        localRot.y -= Input.GetAxis("Mouse Y") * mouseSpeed;

        localRot.y = Mathf.Clamp(localRot.y, 0f, 80f);
        Quaternion qt = Quaternion.Euler(localRot.x, localRot.y, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, qt, Time.deltaTime * mouseDampening);

    }
}
