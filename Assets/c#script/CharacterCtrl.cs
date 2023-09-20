//2023”N9ŒŽ16“ú

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{

    [SerializeField] 
    private Transform targetPlayer;
    [SerializeField]
    private Transform naviPos;
    [SerializeField]
    private Transform ovrCameraRig;
    [SerializeField]
    private Vector3 offSet = new Vector3 (-0.5f, 0.5f, 0.0f);
    [SerializeField]
    float smoothTime = 0.5f;
    [SerializeField]
    float rotateTime;
    Vector3 velocity= Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_pos = ovrCameraRig.position + offSet;
        transform.position=Vector3.SmoothDamp(transform.position,target_pos,ref velocity,smoothTime);
        Vector3 loockPos = ovrCameraRig.position - transform.position;
        loockPos.y = 0;
        if (loockPos != Vector3.zero)
		{
            Quaternion rotation = Quaternion.LookRotation(loockPos);
            transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*rotateTime);
		}
    }
}
