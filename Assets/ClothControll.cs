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

        //�����ɔC�ӂ�vector3��p�ӂ���forward�ɂ������킹�����ł�
        cloth.externalAcceleration = forward;
    }
}
