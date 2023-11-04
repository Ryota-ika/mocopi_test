using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothControll : MonoBehaviour
{
    [SerializeField]
    Cloth cloth;
    [SerializeField]
    Vector3 vector;
    [SerializeField]
    float power;
    [SerializeField]
    Transform navi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.InverseTransformDirection(vector);
        forward.y = 0;

        //ここに任意のvector3を用意してforwardにかけ合わせたいです
        cloth.externalAcceleration = forward;
    }
}
