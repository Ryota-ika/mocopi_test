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
    Transform cameraPos;
    const int PortNum=12350;
    bool isTrakkingStart = false;
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
        cameraPos = GetObjectChild(MyInstance.transform,"CameraPos");
        ////カメラをプレイヤーの頭の位置に置く
        StartCoroutine(StartOVRTrakking());
	}
    IEnumerator StartOVRTrakking()
    {
        while (!isTrakkingStart)
        {
            if (Input.GetKey(KeyCode.Space)) {
                isTrakkingStart=true;
            }
            yield return null;
        }
        OVRcamera.transform.position = cameraPos.position;
        transform.rotation = cameraPos.rotation;
    }
	// Update is called once per frame
	void Update()
    {
        if (!isTrakkingStart) { return; }
        OVRcamera.transform.position = cameraPos.position;
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
