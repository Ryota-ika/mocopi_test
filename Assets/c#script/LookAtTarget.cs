using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]
    private Transform myself;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 forward = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = target.position - myself.position;

        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);

        var offsetRotation = Quaternion.FromToRotation(forward, Vector3.forward);
        myself.rotation = lookAtRotation * offsetRotation;
    }
}
