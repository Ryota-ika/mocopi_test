using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainCtrl : MonoBehaviour
{
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private float targetDistance = 1.0f;
    private bool hasTalkingExplation = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distace = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
        if (distace <= targetDistance && !hasTalkingExplation)
        {
            naviTextVoiceCtrl.PlayTextVoice(9, 9);
            StartCoroutine(naviTextVoiceCtrl.DelateText(5));
            hasTalkingExplation = true;
        }
    }
}
