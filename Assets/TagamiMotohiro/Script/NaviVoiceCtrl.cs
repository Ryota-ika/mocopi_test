using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaviTextVoiceCtrl1 : MonoBehaviour
{
    [Header("ナビ自身のオーディオソース")]
    [SerializeField]
    AudioSource naviAS;
    [Header("ボイス一覧")]
    [SerializeField]
    AudioClip[] voiceList;
    [Header("テキスト一覧")]
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
    public void PlayTextVoice(int voicePatternNum,int textPatternNum)//ボイスパターン一覧とテキストパターン一覧の中からボイスを取得して再生
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString()+"番のボイスを再生した");
        Debug.Log(textPatternNum.ToString()+"番のテキストを再生した");
    }
}
