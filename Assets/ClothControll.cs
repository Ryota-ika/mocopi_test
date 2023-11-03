using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothControll : MonoBehaviour
{
    Cloth cloth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;

        cloth.externalAcceleration = forward;
    }
}
