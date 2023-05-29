//2023”N5ŒŽ29“ú

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    public Transform target;
    public float distance = 1f;
    public float speed = 0.5f;

    private Vector3 velocity= Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target !=null)
        {
            Vector3 targetPosition=target.position-target.forward*distance;

            Vector3 direction=target.position - transform.position;

            float currentDistance= targetPosition.magnitude;
            targetPosition.Normalize();

            if (currentDistance > distance)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);
                /*Vector3 move_vector = targetPosition * (currentDistance - distance);
                transform.Translate(move_vector,Space.World);*/
            }
        }
    }
}
