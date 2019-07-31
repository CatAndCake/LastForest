using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    Transform target;

    [SerializeField]
    Vector3 offset;
    public Quaternion rotate;

    public float smoothing;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x - offset.x, target.position.y - offset.y, target.position.z - offset.z);
        //transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
    }
}
