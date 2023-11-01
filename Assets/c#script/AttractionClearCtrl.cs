using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionClearCtrl : MonoBehaviour
{
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private float targetDistance = 1.0f;
    private bool hasTalkingClear = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!naviTextVoiceCtrl.isTextPlaying)
        {
            float distance = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
            if (distance <= targetDistance && !hasTalkingClear)
            {
                StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(11, 11));
                //naviTextVoiceCtrl.PlayTextVoice(11,11);
                hasTalkingClear = true;
            }

        }
    }
}
