//WROϊ@ϋό΄ΑΎ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    float actionDistance = 1f; //vC[ͺσ ΖΜ£πΖι
    [SerializeField]
    private TresureChest treasureChest; //σ ΜWΜscript
    [SerializeField]
    private PickUpAndRelease pickUpAndRelease;
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;

    private bool isNearTreasureChest = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //vC[Ζσ Μ£πvZ
        float distanceToTreasureChest = Vector3.Distance(transform.position,treasureChest.transform.position);

        //vC[ͺσ ΙίΓ’½κ
        if (distanceToTreasureChest <= actionDistance)
        {
            if (!isNearTreasureChest)
            {
                treasureChest.OpenLid();
                isNearTreasureChest = true;
                pickUpAndRelease.isBoxOpened = true;
                this.gameObject.SetActive(false);
                float delayTime = 3.0f;
                //naviTextVoiceCtrl.PlayTextVoice(0,8);
                //naviTextVoiceCtrl.StartCoroutine(naviTextVoiceCtrl.DelateText(5)); 
            }
        }
    }
}
