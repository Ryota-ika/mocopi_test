using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartRoomKey : KeyObject
{
    [SerializeField]
    MocopiPlayerWork player;
    static bool _1pStanby=false;
    static bool _2pStanby=false;
    bool stanbyOK = false;
    [SerializeField]
    int countDown = 3;
    [SerializeField]
    TMPro.TextMeshProUGUI countDownText;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetStart();
        }
        if (_1pStanby&&_2pStanby&&!stanbyOK)
		{
            stanbyOK = true;
            StartCoroutine(StartCount());
		}
        if (Input.GetKeyDown(KeyCode.C))
		{
            stanbyOK = true;
            StartCoroutine(StartCount());
        }
        
	}
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
        Debug.Log("プロパティ変更");
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
    }
    IEnumerator StartCount()
	{
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
        Debug.Log("ドアが開く");
        isCleard = true;
        player.SetIsCanWalk(true);
	}
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("プロパティが変更された");
        _1pStanby = (bool)PhotonNetwork.CurrentRoom.CustomProperties["1pStanby"];
        _2pStanby = (bool)PhotonNetwork.CurrentRoom.CustomProperties["2pStanby"];
    }
    public static bool GetPlayerStanby(int playerNum)
	{
        if (playerNum == 1)
		{
            return _1pStanby;
		}else if (playerNum == 2)
		{
            return _2pStanby;
		}
        Debug.LogError("プレイヤー指定がなんか変です");
        return false;
	}
}
