//2023.9.27�@��������
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaviTextVoiceCtrl : MonoBehaviour
{
    [Header("�e�L�X�g�ꗗ")]
    [SerializeField]
    [Multiline]  //���s�R�[�h
    List<string> naviTextList=new List<string>();
    [Header("�i�r���g�̃I�[�f�B�I�\�[�X")]
    [SerializeField]
    AudioSource naviAS;
    [Header("�{�C�X�ꗗ")]
    [SerializeField]
    AudioClip[] voiceList;
    [SerializeField]
    public TextMeshProUGUI text;
    [SerializeField]
    private GameObject textObject;
    [Header("�󔠂̊W")]
    [SerializeField]
    private GameObject tresureChest;
    [Header("��")]
    [SerializeField]
    private GameObject key;
    [Header("����")]
    [SerializeField]
    private GameObject torch;
    [Header("�C��")]
    [SerializeField]
    private GameObject candlestick;
    [Header("���A�]�[��")]
    [SerializeField]
    private GameObject cave;
    [Header("�Ђъ��ꂽ�ǔ���")]
    [SerializeField]
    private GameObject crackedWall;
    [Header("�W�F�X�`���[����")]
    [SerializeField]
    private GameObject gestureDiscovery;
    /*[Header("�Ђъ��ꂽ�ǔ���")]
    [SerializeField]
    private GameObject crackedWall1;*/
    [SerializeField]
    private NaviAnimationCtrl naviAnimationCtrl;

    private float targetDistance = 1f;
    private bool hasTalkingTresureChest = false; //�󔠂̘b���������ǂ����̃t���O
    private bool hasTalkingKey = false; //���̘b���������ǂ����̃t���O
    private bool hasTalkingTorch = false;//�����̘b���������ǂ����̃t���O
    private bool hasTalkingCandlestick = false;//�C��̘b���������ǂ����̃t���O
    private bool hasTalkingCave = false;//���A�̘b���������ǂ����̃t���O
    private bool hasTalkingCrackedWall = false;//�Ђъ��ꂽ�ǂ̘b���������ǂ����̃t���O
    private bool hasTalkingGestureDiscovery = false;//�W�F�X�`���[�����̘b���������ǂ����̃t���O
    // Start is called before the first frame update
    void Start()
    {
        textObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //���t���󔠂�������B
        if (tresureChest != null)
        {
        float distance = Vector3.Distance(transform.position, tresureChest.transform.position);
        if (distance <= targetDistance && !hasTalkingTresureChest)
        {
            
            PlayTextVoice(0,0);
            //text.text = "�󔠂݂����I\n�ł��������Ă�݂���...\n����T���ɍs���I";
            //text.text = naviTextList[0];
            StartCoroutine(DelateText(5));
            hasTalkingTresureChest = true;
        }
        }
        //���𔭌�
        float distance1 = Vector3.Distance(transform.position, key.transform.position);
        if (distance1 <= targetDistance && !hasTalkingKey)
        {
            naviAnimationCtrl.PlayFunnyMotion();
            PlayTextVoice(0,1);
            //text.text = "�������Ɍ�������ˁI\n�ǂ��̌����낤�H";
            StartCoroutine(DelateText(5));
            hasTalkingKey = true;
        }
        //��������
        float distance2 = Vector3.Distance(transform.position,torch.transform.position);
        if(distance2 <= targetDistance && !hasTalkingTorch)
        {
            PlayTextVoice(0,2);
            //text.text = "���̏��������Ɏg���邩���I\n�����Ă����Ƃ��I";
            StartCoroutine(DelateText(5));
            hasTalkingTorch = true;
        }
        //�C�䔭��
        float distance3 = Vector3.Distance(transform.position, candlestick.transform.position);
        if (distance3 <= targetDistance && !hasTalkingCandlestick)
        {
            PlayTextVoice(0,3);
            //text.text = "�������g���ΐC��ɉ΂�t���ꂻ���I";
            StartCoroutine(DelateText(5));
            hasTalkingCandlestick = true;
        }
        //���A�]�[���ɓ�������
        float distance4 = Vector3.Distance(transform.position, cave.transform.position);
        if (distance4 <= targetDistance && !hasTalkingCave)
        {
            PlayTextVoice(0,4);
            //text.text = "���A�̒�������^���Â��ˁ`\n�����̖�����𗊂�ɐi�����I";
            StartCoroutine(DelateText(5));
            hasTalkingCave = true;
        }
        //�Ђъ��ꂽ�ǔ���
        float distance5 = Vector3.Distance(transform.position, crackedWall.transform.position);
        if (distance5 <= targetDistance && !hasTalkingCrackedWall)
        {
            PlayTextVoice(0,5);
            //text.text = "���̕ǉ����ŉ󂹂Ȃ����ȁH�ӂ��T���Ă݂悤�I";
            StartCoroutine(DelateText(5));
            hasTalkingCrackedWall = true;
        }
        //�W�F�X�`���[����
        float distance6 = Vector3.Distance(transform.position, gestureDiscovery.transform.position);
        if (distance6 <= targetDistance && !hasTalkingGestureDiscovery)
        {
            PlayTextVoice(0,6);
            //text.text = "�悵�I����Ő�ɐi�߂����I";
            StartCoroutine(DelateText(5));
            hasTalkingGestureDiscovery = true;
        }
        //�W�F�X�`���[����
        float distance7 = Vector3.Distance(transform.position, gestureDiscovery.transform.position);
        if (distance7 <= targetDistance && !hasTalkingGestureDiscovery)
        {
            PlayTextVoice(0, 6);
            //text.text = "�悵�I����Ő�ɐi�߂����I";
            StartCoroutine(DelateText(5));
            hasTalkingGestureDiscovery = true;
        }

    }

    public IEnumerator DelateText(float time)
    {
        textObject.SetActive(true);
        yield return new WaitForSeconds(time);
        textObject.SetActive(false);

    }
    public void PlayTextVoice(int voicePatternNum, int textPatternNum)//�{�C�X�p�^�[���ꗗ�̒�����{�C�X���擾���čĐ�
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString() + "�Ԃ̃{�C�X���Đ�����");
        for (int i = 0; i < naviTextList.Count; i++)
        {
            text.text = naviTextList[textPatternNum];
        }
        StartCoroutine( FlowsText(textPatternNum));
    }
    private IEnumerator FlowsText(int textPatternNum)
    {
        var delay = new WaitForSeconds(0.1f);
        var length = naviTextList[textPatternNum].Length;
        //�P�������\�����鉉�o
        for (var i = 0; i < length; i++)
        {
            //���X�ɕ\���������𑝂₵�Ă���
            text.maxVisibleCharacters = i;
            //��莞�ԑҋ@
            yield return delay;
        }
        //���o���I�������S�Ă̕�����\������
        text.maxVisibleCharacters = length;
    }
}
