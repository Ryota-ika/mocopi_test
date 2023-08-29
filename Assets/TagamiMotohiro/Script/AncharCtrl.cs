using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using RootMotion.FinalIK;
using System.Linq;

public class AncharCtrl : MonoBehaviourPunCallbacks
    //アンカーだけ生成してアバターはオフラインで管理するやり方
{
    [Header("アンカー一覧")]
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
    [Header("mocopiアバター")]
    [SerializeField]
    Transform myMocopiAvatar;
    [Header("アンカー実体")]
    [SerializeField]
    GameObject controller;
    [SerializeField]
    GameObject anchar;
    [Header("アバター一覧")]
    [SerializeField]
    List<GameObject> avatarList;
    [Header("各プレイヤーごとのスタート地点")]
    [SerializeField]
    List<Transform> startPoint;
    [SerializeField]
    LayerMask layer;
    [Tooltip("The settings for VRIK calibration.")] public VRIKCalibrator.Settings settings;
    //自分のアバターの一時保存に使う
    GameObject myAvatar;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
	public override void OnConnectedToMaster()
	{
        RoomOptions roomProps = new RoomOptions();
        roomProps.MaxPlayers = 2;
        roomProps.CleanupCacheOnLeave = true;
        PhotonNetwork.JoinOrCreateRoom("ROOM", roomProps, TypedLobby.Default);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
	public override void OnPlayerEnteredRoom(Player newPlayer)
    //ほかのプレイヤーが入ってきたらローカルにアバターを生成し、オンライン上に生成されてるアンカーをもとにキャリブレーション
	{
        int playerNum = PhotonNetwork.LocalPlayer.ActorNumber;
        StartCoroutine(GetPlayerAnchar(playerNum));
    }
	public override void OnJoinedRoom()//自分が部屋に入った時にアンカーをオンライン上に生成しアバターはローカル上に生成する
	{
        myMocopiAvatar.position = startPoint[PhotonNetwork.LocalPlayer.ActorNumber-1].position;
        //アンカー生成
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
        Transform head = AncharInstantiete(_head, anchar);
        Transform body = AncharInstantiete(_body, anchar);
        Transform leftHand = AncharInstantiete(_leftHand, anchar);
        Transform rightHand = AncharInstantiete(_rightHand, anchar);
        Transform leftFoot = AncharInstantiete(_leftFoot, anchar);
        Transform rightFoot = AncharInstantiete(_rightFoot, anchar);
        
        myAvatar=Instantiate(avatarList[0]);
        StartCoroutine(StartCaliblation(myAvatar,head,body,leftHand,rightHand,leftFoot,rightFoot));
        if (PhotonNetwork.CurrentRoom.PlayerCount==2)
        {
            StartCoroutine(GetPlayerAnchar(1));
        }
    }
    Transform AncharInstantiete(Transform anchar,GameObject instance)
    { 
        GameObject g=PhotonNetwork.Instantiate(instance.name,anchar.position,anchar.rotation);
        g.transform.SetParent(anchar);
        return g.transform;
    }
	// Update is called once per frame
	void Update()
    {
    }
    IEnumerator GetPlayerAnchar(int playerNum)
    {
        GameObject avatar = Instantiate(avatarList[0], Vector3.zero, Quaternion.identity);
        avatar.layer = layer;
        yield return new WaitForSeconds(2);
        List<Transform> anchar = new List<Transform>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Anchar"))
        {
            //Ancharタグのオブジェクトの中から新しく入ったプレイヤーのアンカーを抽出
            PhotonView view = item.GetComponent<PhotonView>();
            if (view != null && view.OwnerActorNr == playerNum)
            {
                anchar.Add(item.transform);
            }
        }
        Debug.Log(anchar.Count);
        StartCoroutine(StartCaliblation(avatar, anchar[0], anchar[1], anchar[2], anchar[3], anchar[4], anchar[5]));
    }
    IEnumerator StartCaliblation(GameObject avatar,Transform head,Transform body,Transform lefthand,Transform righthand,Transform leftFoot,Transform rightFoot)
    {
        //インスタンスが間に合うまで待機
        bool isStart = false;
        yield return new WaitUntil(()=>avatar.GetComponent<VRIK>()!=null);
        VRIK ik = avatar.GetComponent<VRIK>();
        //3秒後にキャリブレーション
        yield return new WaitForSeconds(3);
        VRIKCalibrator.Calibrate(ik,settings,head,body,lefthand,righthand,leftFoot,rightFoot);
    }
	private void OnApplicationQuit()
	{
        Debug.Log("プレイヤーが退室した");
        PhotonNetwork.LeaveRoom();
	}
}
