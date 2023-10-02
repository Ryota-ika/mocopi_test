//2023.9.27
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaviTextVoiceCtrl : MonoBehaviour
{
    [Header("ナビ自身のオーディオソース")]
    [SerializeField]
    AudioSource naviAS;
    [Header("ボイス一覧")]
    [SerializeField]
    AudioClip[] voiceList;
    /*[Header("テキスト一覧")]
    [SerializeField]
    TextMeshPro[] textList;*/
    [SerializeField]
    public TextMeshProUGUI text;
    [SerializeField]
    private GameObject textObject;
    [Header("宝箱の蓋")]
    [SerializeField]
    private TresureChest tresureChest;
    [Header("鍵")]
    [SerializeField]
    private GameObject key;
    [Header("松明")]
    [SerializeField]
    private GameObject torch;
    [Header("燭台")]
    [SerializeField]
    private GameObject candlestick;
    [Header("洞窟ゾーン")]
    [SerializeField]
    private GameObject cave;
    [Header("ひび割れた壁発見")]
    [SerializeField]
    private GameObject crackedWall;


    private float targetDistance = 1f;
    private bool hasTalkingTresureChest = false; //宝箱の話をしたかどうかのフラグ
    private bool hasTalkingKey = false; //鍵の話をしたかどうかのフラグ
    private bool hasTalkingTorch = false;//松明の話をしたかどうかのフラグ
    private bool hasTalkingCandlestick = false;//燭台の話をしたかどうかのフラグ
    private bool hasTalkingCave = false;//洞窟の話をしたかどうかのフラグ
    private bool hasTalkingCrackedWall = false;//ひび割れた壁の話をしたかどうかのフラグ
    // Start is called before the first frame update
    void Start()
    {
        textObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //鍵付き宝箱を見つける。
        float distance = Vector3.Distance(transform.position, tresureChest.transform.position);
        if (distance <= targetDistance && !hasTalkingTresureChest)
        {
            PlayTextVoice(0);
            text.text = "宝箱みつけた！\nでも鍵がついてるみたい...\n鍵を探しに行こ！";
            StartCoroutine(DelateText(5));
            hasTalkingTresureChest = true;
        }
        //鍵を発見
        float distance1 = Vector3.Distance(transform.position, key.transform.position);
        if (distance1 <= targetDistance && !hasTalkingKey)
        {
            text.text = "あそこに鍵があるね！\nどこの鍵だろう？";
            StartCoroutine(DelateText(5));
            hasTalkingKey = true;
        }
        //松明発見
        float distance2 = Vector3.Distance(transform.position,torch.transform.position);
        if(distance2 <= targetDistance && !hasTalkingTorch)
        {
            text.text = "この松明何かに使えるかも！\n持っていっとこ！";
            StartCoroutine(DelateText(5));
            hasTalkingTorch = true;
        }
        //燭台発見
        float distance3 = Vector3.Distance(transform.position, candlestick.transform.position);
        if (distance3 <= targetDistance && !hasTalkingCandlestick)
        {
            text.text = "松明を使えば燭台に火を付けれそう！";
            StartCoroutine(DelateText(5));
            hasTalkingCandlestick = true;
        }
        //洞窟ゾーンに入った時
        float distance4 = Vector3.Distance(transform.position, cave.transform.position);
        if (distance4 <= targetDistance && !hasTalkingCave)
        {
            text.text = "洞窟の中だから真っ暗だね～\n松明の明かりを頼りに進もう！";
            StartCoroutine(DelateText(5));
            hasTalkingCave = true;
        }
        //ひび割れた壁発見
        float distance5 = Vector3.Distance(transform.position, crackedWall.transform.position);
        if (distance5 <= targetDistance && !hasTalkingCrackedWall)
        {
            text.text = "この壁何かで壊せないかな？辺りを探してみよう！";
            StartCoroutine(DelateText(5));
            hasTalkingCrackedWall = true;
        }
        //ひび割れた壁発見
        /*float distance6 = Vector3.Distance(transform.position, crackedWall.transform.position);
        if (distance6 <= targetDistance && !hasTalkingCrackedWall)
        {
            text.text = "よし！これで先に進めそう！";
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
    public void PlayTextVoice(int voicePatternNum/*, int textPatternNum*/)//ボイスパターン一覧の中からボイスを取得して再生
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString() + "番のボイスを再生した");

    }
}
