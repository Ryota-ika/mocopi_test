using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Mocopi.Receiver;

public class MultiPlaySanple : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [Header("Scene上にあるカメラ")]
    [SerializeField]
    GameObject OVRcamera;
    [Header("Scene上にあるMocopiレシーバー")]
    [SerializeField]
    MocopiSimpleReceiver receiver;
    const int PortNum=12351;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }
	public override void OnConnectedToMaster()
	{
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }
	public override void OnJoinedRoom()
	{
        //部屋に入室したらインスタンスを生成し、receiverにインスタンスのアバター情報を取得し、トラッキング開始
		GameObject MyInstance=PhotonNetwork.Instantiate("MocopiAvatar",Vector3.zero,Quaternion.identity);
        receiver.AddAvatar(MyInstance.GetComponent<MocopiAvatar>(),PortNum);
        Transform CameraPos = MyInstance.transform.GetChild(3);
        //カメラをプレイヤーの頭の位置に置き、子オブジェクト化
        OVRcamera.transform.position = CameraPos.position;
        OVRcamera.transform.parent = MyInstance.transform;
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
