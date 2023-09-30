//�W���R�O��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float actionDistance = 1f; //�v���C���[���󔠂Ƃ̋������Ƃ�

    private bool isNearTreasureChest=false;
    [SerializeField]
    private TresureChest treasureChest; //�󔠂̊W��script
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
        //�v���C���[�ƕ󔠂̋������v�Z
        float distanceToTreasureChest = Vector3.Distance(transform.position,treasureChest.transform.position);

        //�v���C���[���󔠂ɋ߂Â����ꍇ
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
                //naviTextVoiceCtrl.text.text = "����͉����ɂ����邩���I\n�����Ă����Ƃ��I";
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
