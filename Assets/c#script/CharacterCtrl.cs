//2023îN9åé16ì˙Å@çÇã¥ó¡ëæ

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
        Vector3 target_pos = naviPos.position;
        transform.position=Vector3.SmoothDamp(transform.position,target_pos,ref velocity,smoothTime);
        Vector3 loockPos = targetPlayer.position - transform.position;
        loockPos.y = 0;
        if (loockPos != Vector3.zero)
		{
            Quaternion rotation = Quaternion.LookRotation(loockPos);
            transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*rotateTime);
		}
    }
}
