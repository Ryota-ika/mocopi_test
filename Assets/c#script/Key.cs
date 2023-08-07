using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float actionDistance = 1f; //�v���C���[���󔠂Ƃ̋������Ƃ�

    private bool isNearTreasureChest=false;
    private TresureChest treasureChest; //�󔠂̊W��script
    // Start is called before the first frame update
    void Start()
    {
        treasureChest = FindObjectOfType<TresureChest>();
    }

    // Update is called once per frame
    void Update()
    {
        /*treasureChest.OpenLid();*/
        //�v���C���[�ƕ󔠂̋������v�Z
        float distanceToTreasureChest = Vector3.Distance(transform.position,treasureChest.transform.position);

        //�v���C���[���󔠂ɋ߂Â����ꍇ
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
