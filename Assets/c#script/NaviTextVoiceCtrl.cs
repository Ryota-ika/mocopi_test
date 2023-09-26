using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class NaviTextVoiceCtrl : MonoBehaviour
{
    [Header("�i�r���g�̃I�[�f�B�I�\�[�X")]
    [SerializeField]
    AudioSource naviAS;
    [Header("�{�C�X�ꗗ")]
    [SerializeField]
    AudioClip[] voiceList;
    /*[Header("�e�L�X�g�ꗗ")]
    [SerializeField]
    TextMeshPro[] textList;*/
    [SerializeField]
    private TextMeshProUGUI text;
    [Header("�󔠂̊W")]
    [SerializeField]
    private TresureChest tresureChest;
    private float targetDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position,tresureChest.transform.position);
        if (distance <= targetDistance)
        {
            PlayTextVoice(0);
            text.text = "�󔠂݂����I\n�ł��������Ă�݂���...\n����T���ɍs���I";
        }
    }
    public void PlayTextVoice(int voicePatternNum/*, int textPatternNum*/)//�{�C�X�p�^�[���ꗗ�̒�����{�C�X���擾���čĐ�
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString()+"�Ԃ̃{�C�X���Đ�����");
        
    }
}
