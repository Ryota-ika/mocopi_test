using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using RootMotion.FinalIK;
using System.Linq;

public class AncharCtrl : MonoBehaviourPunCallbacks
    //�A���J�[�����������ăA�o�^�[�̓I�t���C���ŊǗ��������
{
    [Header("�A���J�[�ꗗ")]
    [SerializeField]
    Transform _head;
    [SerializeField]
    Transform _body;
    [SerializeField]
    Transform _rightHand;
    [SerializeField]
    Transform _leftHand;
    [SerializeField]
    Transform _rightFoot;
    [SerializeField]
    Transform _leftFoot;
    [Header("mocopi�A�o�^�[")]
    [SerializeField]
    Transform myMocopiAvatar;
    [Header("�A���J�[����")]
    [SerializeField]
    GameObject controller;
    [SerializeField]
    GameObject anchar;
    [Header("�A�o�^�[�ꗗ")]
    [SerializeField]
    List<GameObject> avatarList;
    [Header("�e�v���C���[���Ƃ̃X�^�[�g�n�_")]
    [SerializeField]
    List<Transform> startPoint;
    [SerializeField]
    LayerMask layer;
    static int playerNum;
    [Tooltip("The settings for VRIK calibration.")] public VRIKCalibrator.Settings settings;
    //�����̃A�o�^�[�̈ꎞ�ۑ��Ɏg��
    GameObject myAvatar;
    Player[] players=new Player[2];
    // Start is called before the first frame update
    void Start()
    {
        //�ڑ�����O�ɖ����I�ɂPp�����Qp�����I��������悤�ɂ��܂���
        StartCoroutine(InitPlayer());
        Debug.Log("�v���C���[�I���J�n");
    }
    //�v���C���[���ǂ��瑤���I��
    IEnumerator InitPlayer()
	{
        bool selected = false;
        while (!selected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectPlayerNum(1);
                selected = true;
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectPlayerNum(2);
                selected = true;
            }
            yield return null;
        }
        Debug.Log("�v���C���[���I�����ꂽ");
        
	}
    public void SelectPlayerNum(int value)
	{
        playerNum = value;
        startConnection();
	}
    void startConnection()
	{
        Debug.Log("�ڑ��J�n");
        PhotonNetwork.ConnectUsingSettings();
	}
    //�����쐬�҂͏����v���p�e�B��ݒ�
	public override void OnCreatedRoom()
	{ 
        RoomOptions roomProps = new RoomOptions();
        roomProps.MaxPlayers = 2;
        roomProps.CleanupCacheOnLeave = true;
        ExitGames.Client.Photon.Hashtable roomInfo = new ExitGames.Client.Photon.Hashtable();
        roomInfo.Add("1pStanby", false);
        roomInfo.Add("2pStanby", false);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomInfo);
    }
	//�}�X�^�[�ɐڑ����ꂽ��
	public override void OnConnectedToMaster()
	{
        //�v���C���[�ԍ����J�X�^���v���p�e�B�ɐݒ�
        ExitGames.Client.Photon.Hashtable playerCustomProps = new ExitGames.Client.Photon.Hashtable();
        playerCustomProps.Add("PlayerNum", playerNum);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);
        //�����ɓ���
        PhotonNetwork.JoinOrCreateRoom("ROOM.", new RoomOptions(), TypedLobby.Default);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
	public override void OnPlayerEnteredRoom(Player newPlayer)
    //�ق��̃v���C���[�������Ă����烍�[�J���ɃA�o�^�[�𐶐����A�I�����C����ɐ�������Ă�A���J�[�����ƂɃL�����u���[�V����
	{
        int playerNum = (int)newPlayer.CustomProperties["PlayerNum"];
        StartCoroutine(GetPlayerAnchar(playerNum));
    }
	public override void OnJoinedRoom()//�����������ɓ��������ɃA���J�[���I�����C����ɐ������A�o�^�[�̓��[�J����ɐ�������
	{
        int posnum=0;
        if (playerNum==1) {
            posnum = 0;
        }else
        {
            posnum = 1;
        }
        myMocopiAvatar.position = startPoint[posnum].position;
        //�A���J�[����
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
        Transform head = AncharInstantiete(_head, anchar);
        Transform body = AncharInstantiete(_body, anchar);
        Transform leftHand = AncharInstantiete(_leftHand, anchar);
        Transform rightHand = AncharInstantiete(_rightHand, anchar);
        Transform leftFoot = AncharInstantiete(_leftFoot, anchar);
        Transform rightFoot = AncharInstantiete(_rightFoot, anchar);
        
        myAvatar=Instantiate(avatarList[posnum]);
        StartCoroutine(StartCaliblation(myAvatar, head, body, leftHand, rightHand, leftFoot, rightFoot));
        //���g��2�l�ڈȍ~�̃v���C���[�������ꍇ�A1P�̏����擾
        if (!PhotonNetwork.IsMasterClient) {
            StartCoroutine(GetPlayerAnchar(GetHostPlayerNum()));
        }
    }
    //���ʃA���J�[�𐶐�
    Transform AncharInstantiete(Transform anchar,GameObject instance)
    { 
        GameObject g=PhotonNetwork.Instantiate(instance.name,anchar.position,anchar.rotation);
        g.transform.SetParent(anchar);
        return g.transform;
    }
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
        Debug.LogError("�����ɓ����ł��˂�"+message);
	}
	// Update is called once per frame
	void Update()
    {
    }
    //�w�肵���v���C���[�̃A���J�[�𒊏o���ăL�����u���[�V�����J�n
    IEnumerator GetPlayerAnchar(int playerNum)
    {
        GameObject avatar = Instantiate(avatarList[(playerNum%2)], Vector3.zero, Quaternion.identity);
        avatar.layer = 1;
        yield return new WaitForSeconds(2);
        List<Transform> anchar = new List<Transform>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Anchar"))
        {
            //Anchar�^�O�̃I�u�W�F�N�g�̒�����V�����������v���C���[�̃A���J�[�𒊏o
            PhotonView view = item.GetComponent<PhotonView>();
            if (view != null && view.OwnerActorNr == playerNum)
            {
                anchar.Add(item.transform);
            }
        }
        StartCoroutine(StartCaliblation(avatar, anchar[0], anchar[1], anchar[2], anchar[3], anchar[4], anchar[5]));
    }
    //�����Ŏ󂯎�����A�o�^�[�Ƃ��ꂼ��̃A���J�[���L�����u���[�V����
    IEnumerator StartCaliblation(GameObject avatar,Transform head,Transform body,Transform lefthand,Transform righthand,Transform leftFoot,Transform rightFoot)
    {
        //�C���X�^���X���Ԃɍ����܂őҋ@
        yield return new WaitUntil(()=>avatar.GetComponent<VRIK>()!=null);
        VRIK ik = avatar.GetComponent<VRIK>();
        //�{�^������������L�����u���[�V����
        while (!Input.GetKeyDown(KeyCode.C)) {
            yield return null;
        }
        VRIKCalibrator.Calibrate(ik,settings,head,body,lefthand,righthand,leftFoot,rightFoot);
    }
	private void OnApplicationQuit()
	{
        Debug.Log("�v���C���[���ގ�����");
        PhotonNetwork.LeaveRoom();
	}
    int GetHostPlayerNum()
	{
        // ���[�����̑S�Ẵv���C���[���擾
        Player[] players = PhotonNetwork.PlayerList;

        // �z�X�g�v���C���[��������
        Player hostPlayer = null;
        foreach (Player player in players)
        {
            if (player.IsMasterClient)
            {
                hostPlayer = player;
                break;
            }
        }
        //�@�z�X�g�̃v���C���[�ԍ����擾
        Debug.Log((int)hostPlayer.CustomProperties["PlayerNum"]);
        return (int)hostPlayer.CustomProperties["PlayerNum"];
    }

    public static int GetPlayeNum()
	{
        return playerNum;
	}
}
