using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
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
    private TextMeshProUGUI text;
    [Header("宝箱の蓋")]
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
            text.text = "宝箱みつけた！\nでも鍵がついてるみたい...\n鍵を探しに行こ！";
        }
    }
    public void PlayTextVoice(int voicePatternNum/*, int textPatternNum*/)//ボイスパターン一覧の中からボイスを取得して再生
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString()+"番のボイスを再生した");
        
    }
}
