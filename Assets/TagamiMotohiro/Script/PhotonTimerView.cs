using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonTimerView : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    float timer;
    [SerializeField]
    TMPro.TextMeshProUGUI TimerText;
    bool isTimerStart = false;
    [SerializeField]
    StartRoomKey startButton;
    bool isGameOver;
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    bool[] timeLateList = new bool[] { false, false, false };
    [SerializeField]
    MocopiPlayerWork player;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!startButton.GetIsCleard()) { return; }
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        //各時間制限にナビが反応
        //あまりに雑すぎるのでゲームショウ終了後に直す
        if (!naviTextVoiceCtrl.isTextPlaying)
        {
            if (timer <= 120 && !timeLateList[0])
            {
                StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(13, 13));
                timeLateList[0] = true;
            }
            if (timer <= 60 && !timeLateList[1])
            {
                TimerText.color = Color.red;
                StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(14, 14));
                timeLateList[1] = true;
            }
            if (timer <= 30 && !timeLateList[2])
            {
                StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(15, 15));
                timeLateList[2] = true;
            }
            TimerText.text = minutes.ToString("00") + (":") + seconds.ToString("00");
            if (timer <= 0 && !isGameOver)
            {
                TimerText.gameObject.SetActive(false);
                player.SetIsCanWalk(false);
                Debug.Log("ゲームオーバー");
                //naviTextVoiceCtrl.PlayTextVoice(10,10);
                isGameOver = true;
                StartCoroutine(naviTextVoiceCtrl.WaitAndPlayTextVoice(10, 10));
            }

        }
    }
    // 時間を各ROM内で同期する
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(timer);
        }
        else
        {
            timer = (float)stream.ReceiveNext();
        }
    }
}
