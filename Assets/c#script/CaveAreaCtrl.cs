using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveAreaCtrl : MonoBehaviour
{
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private MocopiPlayerWork mocopiPlayerWork;
    [SerializeField]
    private float targetDistance = 3f;
    private bool hasTalkingCave = false; //“´ŒA‚Ì˜b‚ð‚µ‚½‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mocopiPlayerWork.GetIsCanWalk())
        {
            float distance = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
            if (distance <= targetDistance && !hasTalkingCave)
            {
                naviTextVoiceCtrl.PlayTextVoice(4, 4);
                StartCoroutine(naviTextVoiceCtrl.DelateText(5));
                hasTalkingCave = true;
            }

        }
    }
}
