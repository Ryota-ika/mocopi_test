using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// プレイヤーの待機状態を監視するクラス
public class StartRoomKey : KeyObject
{
    [SerializeField]
    AudioClip countDownSE;
    [SerializeField]
    AudioSource myAS;
    [SerializeField]
    MocopiPlayerWork player;
    static bool _1pStanby = false;
    static bool _2pStanby = false;
    bool stanbyOK = false;
    [SerializeField]
    int countDown = 3;
    [SerializeField]
    TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    AudioSource BGM;
    [SerializeField]
    AudioClip inGameBGM;
    // Start is called before the first frame update
    void Start()
    {
        player.SetIsCanWalk(false);
    }

    // Update is called once per frame
    void Update()
    {
        CrearDirection();
    }
	protected override void CrearDirection()
	{
        // Spaceキーで準備完了
        if ((Input.GetKeyDown(KeyCode.Space)||OVRInput.GetDown(OVRInput.RawButton.X))&&AncharCtrl.GetisConnected())
        {
            SetStart();
        }
        // 1Pと2Pの準備状態がそろったらゲーム開始
        if (_1pStanby&&_2pStanby&&!stanbyOK)
		{
            stanbyOK = true;
            StartCoroutine(StartCount());
		}
        //プレイヤーがそろっていなくても強制開始
        if (Input.GetKeyDown(KeyCode.C)||OVRInput.GetDown(OVRInput.RawButton.B))
		{
            stanbyOK = true;
            StartCoroutine(StartCount());
        }
	}
    // 準備完了にする
    public void SetStart()
	{
        var roomProps = new ExitGames.Client.Photon.Hashtable();
        if (AncharCtrl.GetPlayeNum() == 1)
        {
            roomProps.Add("1pStanby", true);
        }
        else
        {
            roomProps.Add("2pStanby", true);
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
    }
    // ゲーム開始の処理
    IEnumerator StartCount()
	{
        myAS.PlayOneShot(countDownSE);
        BGM.Stop();
        countDownText.text = countDown.ToString();
        while (countDown >= 0)
		{
            countDownText.text = countDown.ToString();
            if (countDown == 0)
			{
                countDownText.text = "START!";
			}
            yield return new WaitForSeconds(1);
            countDown-=1;
		}
        countDownText.gameObject.SetActive(false);
        BGM.PlayOneShot(inGameBGM);
        isCleard = true;
        player.SetIsCanWalk(true);
	}
    // 相手プレイヤーの準備完了を検知してローカルに反映
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("プロパティが変更された");
        _1pStanby = (bool)PhotonNetwork.CurrentRoom.CustomProperties["1pStanby"];
        _2pStanby = (bool)PhotonNetwork.CurrentRoom.CustomProperties["2pStanby"];
    }
    //　各プレイヤーの準備状態を取得
    public static bool GetPlayerStanby(int playerNum)
	{
        if (playerNum == 1)
		{
            return _1pStanby;
		}else if (playerNum == 2)
		{
            return _2pStanby;
		}
        return false;
	}
}
