using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonTimerView : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    float timer;
    [SerializeField]
    TMPro.TextMeshProUGUI TimerText;
    bool isTimerStart = false;
    [SerializeField]
    StartRoomKey startButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!startButton.GetIsCleard()){ return; }
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        TimerText.text = minutes.ToString("00")+(":")+seconds.ToString("00");
        if (timer<=0) {
            Debug.Log("ゲームオーバー");
        }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
        if (PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(timer);
        }
        else {
            timer = (float)stream.ReceiveNext();
        }
	}
}
