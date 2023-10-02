//2023.9.27
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI text;
    [SerializeField]
    private GameObject textObject;
    [Header("�󔠂̊W")]
    [SerializeField]
    private TresureChest tresureChest;
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


    private float targetDistance = 1f;
    private bool hasTalkingTresureChest = false; //�󔠂̘b���������ǂ����̃t���O
    private bool hasTalkingKey = false; //���̘b���������ǂ����̃t���O
    private bool hasTalkingTorch = false;//�����̘b���������ǂ����̃t���O
    private bool hasTalkingCandlestick = false;//�C��̘b���������ǂ����̃t���O
    private bool hasTalkingCave = false;//���A�̘b���������ǂ����̃t���O
    private bool hasTalkingCrackedWall = false;//�Ђъ��ꂽ�ǂ̘b���������ǂ����̃t���O
    // Start is called before the first frame update
    void Start()
    {
        textObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //���t���󔠂�������B
        float distance = Vector3.Distance(transform.position, tresureChest.transform.position);
        if (distance <= targetDistance && !hasTalkingTresureChest)
        {
            PlayTextVoice(0);
            text.text = "�󔠂݂����I\n�ł��������Ă�݂���...\n����T���ɍs���I";
            StartCoroutine(DelateText(5));
            hasTalkingTresureChest = true;
        }
        //���𔭌�
        float distance1 = Vector3.Distance(transform.position, key.transform.position);
        if (distance1 <= targetDistance && !hasTalkingKey)
        {
            text.text = "�������Ɍ�������ˁI\n�ǂ��̌����낤�H";
            StartCoroutine(DelateText(5));
            hasTalkingKey = true;
        }
        //��������
        float distance2 = Vector3.Distance(transform.position,torch.transform.position);
        if(distance2 <= targetDistance && !hasTalkingTorch)
        {
            text.text = "���̏��������Ɏg���邩���I\n�����Ă����Ƃ��I";
            StartCoroutine(DelateText(5));
            hasTalkingTorch = true;
        }
        //�C�䔭��
        float distance3 = Vector3.Distance(transform.position, candlestick.transform.position);
        if (distance3 <= targetDistance && !hasTalkingCandlestick)
        {
            text.text = "�������g���ΐC��ɉ΂�t���ꂻ���I";
            StartCoroutine(DelateText(5));
            hasTalkingCandlestick = true;
        }
        //���A�]�[���ɓ�������
        float distance4 = Vector3.Distance(transform.position, cave.transform.position);
        if (distance4 <= targetDistance && !hasTalkingCave)
        {
            text.text = "���A�̒�������^���Â��ˁ`\n�����̖�����𗊂�ɐi�����I";
            StartCoroutine(DelateText(5));
            hasTalkingCave = true;
        }
        //�Ђъ��ꂽ�ǔ���
        float distance5 = Vector3.Distance(transform.position, crackedWall.transform.position);
        if (distance5 <= targetDistance && !hasTalkingCrackedWall)
        {
            text.text = "���̕ǉ����ŉ󂹂Ȃ����ȁH�ӂ��T���Ă݂悤�I";
            StartCoroutine(DelateText(5));
            hasTalkingCrackedWall = true;
        }
        //�Ђъ��ꂽ�ǔ���
        /*float distance6 = Vector3.Distance(transform.position, crackedWall.transform.position);
        if (distance6 <= targetDistance && !hasTalkingCrackedWall)
        {
            text.text = "�悵�I����Ő�ɐi�߂����I";
            StartCoroutine(DelateText(5));
            hasTalkingCrackedWall = true;
        }*/

    }

    public IEnumerator DelateText(float time)
    {
        textObject.SetActive(true);
        yield return new WaitForSeconds(time);
        textObject.SetActive(false);

    }
    public void PlayTextVoice(int voicePatternNum/*, int textPatternNum*/)//�{�C�X�p�^�[���ꗗ�̒�����{�C�X���擾���čĐ�
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString() + "�Ԃ̃{�C�X���Đ�����");

    }
}
