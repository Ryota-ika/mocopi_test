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
    [Header("Scene��ɂ���J����")]
    [SerializeField]
    GameObject OVRcamera;
    [Header("Scene��ɂ���Mocopi���V�[�o�[")]
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
        //�����ɓ���������C���X�^���X�𐶐����Areceiver�ɃC���X�^���X�̃A�o�^�[�����擾���A�g���b�L���O�J�n
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
        ////�J�������v���C���[�̓��̈ʒu�ɒu��
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
    Transform GetObjectChild(Transform parent,string name)//�J�����Q�Ɨp�̍ċA�q�I�u�W�F�N�g�T�����\�b�h
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
