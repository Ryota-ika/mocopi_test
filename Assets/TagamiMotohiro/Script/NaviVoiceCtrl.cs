using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaviTextVoiceCtrl1 : MonoBehaviour
{
    [Header("�i�r���g�̃I�[�f�B�I�\�[�X")]
    [SerializeField]
    AudioSource naviAS;
    [Header("�{�C�X�ꗗ")]
    [SerializeField]
    AudioClip[] voiceList;
    [Header("�e�L�X�g�ꗗ")]
    [SerializeField]
    TextMeshProUGUI[] textList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayTextVoice(int voicePatternNum,int textPatternNum)//�{�C�X�p�^�[���ꗗ�ƃe�L�X�g�p�^�[���ꗗ�̒�����{�C�X���擾���čĐ�
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString()+"�Ԃ̃{�C�X���Đ�����");
        Debug.Log(textPatternNum.ToString()+"�Ԃ̃e�L�X�g���Đ�����");
    }
}
