using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCtrl : MonoBehaviour
{
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private float targetDistance = 1f;
    private bool hasTalkingTorch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //èºñæî≠å©
        float distance = Vector3.Distance(transform.position,naviTextVoiceCtrl.transform.position);
        if (distance <= targetDistance && !hasTalkingTorch)
        {
            naviTextVoiceCtrl.PlayTextVoice(2,2);
            StartCoroutine(naviTextVoiceCtrl.DelateText(5));
            hasTalkingTorch=true;
        }
    }
}
