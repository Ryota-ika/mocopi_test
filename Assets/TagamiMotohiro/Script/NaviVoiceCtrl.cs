using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviVoiceCtrl : MonoBehaviour
{
    [Header("ナビ自身のオーディオソース")]
    [SerializeField]
    AudioSource naviAS;
    [Header("ボイス一覧")]
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
    public void PlayVoice(int voicePatternNum)//ボイスパターン一覧の中からボイスを取得して再生
    {
        //naviAS.PlayOneShot(voiceList[voicePatternNum]);
        Debug.Log(voicePatternNum.ToString()+"番のボイスを再生した");
    }
}
