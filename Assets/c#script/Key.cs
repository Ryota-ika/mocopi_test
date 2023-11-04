//８月３０日　髙橋涼太
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    float actionDistance = 1f; //プレイヤーが宝箱との距離をとる
    [SerializeField]
    private TresureChest treasureChest; //宝箱の蓋のscript
    [SerializeField]
    private PickUpAndRelease pickUpAndRelease;
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private MocopiPlayerWork mocopiPlayerWork;
    [SerializeField]
    private float targetDistace = 1f;
    private bool isNearTreasureChest = false;

    private bool hasTalkingKey = false; //鍵の話をしたかどうかのフラグ
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーと宝箱の距離を計算
        float distanceToTreasureChest = Vector3.Distance(transform.position, treasureChest.transform.position);

        //プレイヤーが宝箱に近づいた場合
        if (distanceToTreasureChest <= actionDistance)
        {
            if (!isNearTreasureChest)
            {
                treasureChest.OpenLid();
                isNearTreasureChest = true;
                pickUpAndRelease.isBoxOpened = true;
                this.gameObject.SetActive(false);
                float delayTime = 3.0f;
                //naviTextVoiceCtrl.PlayTextVoice(8,8);
                naviTextVoiceCtrl.StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(8, 8));
            }
        }
        if (mocopiPlayerWork.GetIsCanWalk())
        {
            if (!naviTextVoiceCtrl.isTextPlaying)
            {
                float distance = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
                if (distance <= targetDistace && !hasTalkingKey)
                {
                    //naviTextVoiceCtrl.PlayTextVoice(1, 1);
                    StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(1, 1));
                    hasTalkingKey = true;
                }
            }
            //鍵発見

        }
    }
}
