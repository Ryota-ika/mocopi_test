using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
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
        //�����ɓ���������C���X�^���X�𐶐����Areceiver�ɃC���X�^���X�̃A�o�^�[�����擾���A�g���b�L���O�J�n
		GameObject MyInstance=PhotonNetwork.Instantiate("MocopiAvatar",Vector3.zero,Quaternion.identity);
        receiver.AddAvatar(MyInstance.GetComponent<MocopiAvatar>(),PortNum);
        Transform CameraPos = MyInstance.transform.GetChild(3);
        //�J�������v���C���[�̓��̈ʒu�ɒu���A�q�I�u�W�F�N�g��
        OVRcamera.transform.position = CameraPos.position;
        OVRcamera.transform.parent = MyInstance.transform;
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
