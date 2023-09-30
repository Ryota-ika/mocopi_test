//８月３０日
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float actionDistance = 1f; //プレイヤーが宝箱との距離をとる

    private bool isNearTreasureChest=false;
    [SerializeField]
    private TresureChest treasureChest; //宝箱の蓋のscript
    [SerializeField]
    private PickUpAndRelease pickUpAndRelease;
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*treasureChest.OpenLid();*/
        //プレイヤーと宝箱の距離を計算
        float distanceToTreasureChest = Vector3.Distance(transform.position,treasureChest.transform.position);

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
                naviTextVoiceCtrl.PlayTextVoice(0,8);
                //naviTextVoiceCtrl.text.text = "これは何かにつかえるかも！\n持っていっとこ！";
                naviTextVoiceCtrl.StartCoroutine(naviTextVoiceCtrl.DelateText(5)); ;
                //pickUpAndRelease.StartCoroutine(pickUpAndRelease.DelaydMethodCoroutine(delayTime));
                //Destroy(this.gameObject);
            }
        }
        /*else
        {
            if (isNearTreasureChest)
            {
                treasureChest.CloseLid();
                isNearTreasureChest = false;
            }
        }*/
    }
}
