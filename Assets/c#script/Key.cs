//�W���R�O���@��������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    float actionDistance = 1f; //�v���C���[���󔠂Ƃ̋������Ƃ�
    [SerializeField]
    private TresureChest treasureChest; //�󔠂̊W��script
    [SerializeField]
    private PickUpAndRelease pickUpAndRelease;
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private MocopiPlayerWork mocopiPlayerWork;
    [SerializeField]
    private float targetDistace = 1f;
    private bool isNearTreasureChest = false;

    private bool hasTalkingKey = false; //���̘b���������ǂ����̃t���O
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�ƕ󔠂̋������v�Z
        float distanceToTreasureChest = Vector3.Distance(transform.position, treasureChest.transform.position);

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
            //������

        }
    }
}
