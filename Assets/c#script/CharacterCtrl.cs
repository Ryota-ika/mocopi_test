//2023”N5ŒŽ30“ú

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{

    [SerializeField] 
    private Transform targetPlayer;
    [SerializeField]
    float smoothTime = 0.5f;
    Vector3 velocity= Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_pos = targetPlayer.TransformPoint(new Vector3(0.5f, 1.0f, -1.0f));

        transform.position=Vector3.SmoothDamp(transform.position,target_pos,ref velocity,smoothTime);
    }
}
