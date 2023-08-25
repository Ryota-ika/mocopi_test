using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviVoicePoint : MonoBehaviour//�v���C���[���߂Â�����i�r�̃{�C�X���Đ�������X�N���v�g
{
    [SerializeField]
    Transform player;
    [SerializeField]
    NaviVoiceCtrl navi;
    [SerializeField]
    int voiceNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 1f) {
            navi.PlayVoice(voiceNum);
            this.gameObject.SetActive(false);
        } 
    }
}
