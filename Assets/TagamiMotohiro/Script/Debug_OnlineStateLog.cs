using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

// オンラインの接続状態とかその他の要素をビルド環境でも見れるようにするクラス
public class Debug_OnlineStateLog : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    TextMeshProUGUI connectStateText;
    [SerializeField]
    TextMeshProUGUI connectRoomNameText;
    [SerializeField]
    TextMeshProUGUI connectPlayersStateText;
    [SerializeField]
    TextMeshProUGUI naviClothState;

    [SerializeField]
    GameObject naviCloth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 情報表示切替
        if (Input.GetKeyDown(KeyCode.F11)) {
            canvas.SetActive(!canvas.activeSelf);
        }
        // 接続関連の表示
        if (connectStateText == null) { return; }
        connectStateText.text = "接続の状態 = " + PhotonNetwork.IsConnected.ToString();
        if (connectRoomNameText == null) { return; } 
        connectRoomNameText.text = "現在の部屋名 = " + PhotonNetwork.CurrentRoom.Name;
        if (connectPlayersStateText == null) { return; }
        connectPlayersStateText.text = "プレイヤーの数 = " + PhotonNetwork.PlayerList.Length;

        // ステージの状態の表示
        naviClothState.text = naviCloth.transform.position.ToString();

    }

}
