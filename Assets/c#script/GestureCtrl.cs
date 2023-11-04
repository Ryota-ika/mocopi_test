using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCtrl : MonoBehaviour
{
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private MocopiPlayerWork mocopiPlayerWork;
    [SerializeField]
    private float targetDistance = 1.0f;
    private bool hasTalkingGestureDiscovery = false;//ジェスチャー発見の話をしたかどうかのフラグ
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mocopiPlayerWork.GetIsCanWalk())
        {
            if (!naviTextVoiceCtrl.isTextPlaying)
            {
                float distance = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
                if (distance <= targetDistance && !hasTalkingGestureDiscovery)
                {
                    //naviTextVoiceCtrl.PlayTextVoice(6, 6);
                    StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(6, 6));
                    hasTalkingGestureDiscovery = true;
                }
            }
        }
    }
}
