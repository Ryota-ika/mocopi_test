//6月21日

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
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
    [SerializeField]
    List<Transform> startPosList=new List<Transform>();
    const int PortNum=12350;
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
        GameObject MyInstance=null;
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        //部屋に入室したらインスタンスを生成し、receiverにインスタンスのアバター情報を取得し、トラッキング開始
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
            MyInstance=PhotonNetwork.Instantiate("MocopiAvatar",startPosList[0].position,Quaternion.identity);
		    receiver.AddAvatar(MyInstance.GetComponent<MocopiAvatar>(), PortNum+PhotonNetwork.LocalPlayer.ActorNumber);
            receiver.StartReceiving();
            break;
            case 2:
            MyInstance = PhotonNetwork.Instantiate("MocopiAvatar", startPosList[1].position, Quaternion.identity);
            receiver.AddAvatar(MyInstance.GetComponent<MocopiAvatar>(), PortNum
                +PhotonNetwork.LocalPlayer.ActorNumber);
            receiver.StartReceiving();
            break;
        }
        Transform CameraPos = GetObjectChild(MyInstance.transform,"CameraPos");
        Debug.Log(CameraPos.gameObject.name);
		////カメラをプレイヤーの頭の位置に置き、子オブジェクト化
		OVRcamera.transform.position = CameraPos.position;
		OVRcamera.transform.parent = CameraPos.transform;
	}
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
    }
    Transform GetObjectChild(Transform parent,string name)//カメラ参照用の再帰子オブジェクト探索メソッド
    {
        if (name==parent.name) {
            return parent;
        }
        for (int i=0;i<parent.childCount;i++) {
            Transform child=parent.transform.GetChild(i);
            Transform result = GetObjectChild(child,name);
            if (result!=null) {
                return result;
            }
        }
        return null;
    }
}
