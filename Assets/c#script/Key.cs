using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float actionDistance = 1f; //プレイヤーが宝箱との距離をとる

    private bool isNearTreasureChest=false;
    private TresureChest treasureChest; //宝箱の蓋のscript
    // Start is called before the first frame update
    void Start()
    {
        treasureChest = FindObjectOfType<TresureChest>();
    }

    // Update is called once per frame
    void Update()
    {
        /*treasureChest.OpenLid();*/
        //プレイヤーと宝箱の距離を計算
        float distanceToTreasureChest = Vector3.Distance(transform.position,treasureChest.transform.position);

        //プレイヤーが宝箱に近づいた場合
        if (distanceToTreasureChest<=actionDistance)
        {
            if (!isNearTreasureChest)
            {
                treasureChest.OpenLid();
                isNearTreasureChest = true;
            }
        }
        else
        {
            if (isNearTreasureChest)
            {
                treasureChest.CloseLid();
                isNearTreasureChest = false;
            }
        }
    }
}
