//WROú@ûü´Á¾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    float actionDistance = 1f; //vC[ªó ÆÌ£ðÆé
    [SerializeField]
    private TresureChest treasureChest; //ó ÌWÌscript
    [SerializeField]
    private PickUpAndRelease pickUpAndRelease;
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private float targetDistace = 1f;
    private bool isNearTreasureChest = false;

    private bool hasTalkingKey = false; //®Ìbðµ½©Ç¤©ÌtO
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //vC[Æó Ì£ðvZ
        float distanceToTreasureChest = Vector3.Distance(transform.position,treasureChest.transform.position);

        //vC[ªó ÉßÃ¢½ê
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
        //®­©
        float distance = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
        if (distance <= targetDistace && !hasTalkingKey)
        {
            naviTextVoiceCtrl.PlayTextVoice(1, 1);
            StartCoroutine(naviTextVoiceCtrl.DelateText(5));
            hasTalkingKey = true;
        }
    }
}
