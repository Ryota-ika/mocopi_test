using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviVoiceCtrl : MonoBehaviour
{
    [Header("�i�r���g�̃I�[�f�B�I�\�[�X")]
    [SerializeField]
    AudioSource naviAS;
    [Header("�{�C�X�ꗗ")]
    [SerializeField]
    AudioClip[] voiceList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayVoice(int voicePatternNum)//�{�C�X�p�^�[���ꗗ�̒�����{�C�X���擾���čĐ�
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString()+"�Ԃ̃{�C�X���Đ�����");
    }
}
