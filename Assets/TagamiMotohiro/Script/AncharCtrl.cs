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
    static int playerNum;
    [Tooltip("The settings for VRIK calibration.")] public VRIKCalibrator.Settings settings;
    //自分のアバターの一時保存に使う
    GameObject myAvatar;
    Player[] players=new Player[2];
    // Start is called before the first frame update
    void Start()
    {
        //接続する前に明示的に１p側か２p側か選択させるようにしました
        StartCoroutine(InitPlayer());
        Debug.Log("プレイヤー選択開始");
    }
    //プレイヤーがどちら側か選択
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
        Debug.Log("プレイヤーが選択された");
        
	}
    public void SelectPlayerNum(int value)
	{
        playerNum = value;
        startConnection();
	}
    void startConnection()
	{
        Debug.Log("接続開始");
        PhotonNetwork.ConnectUsingSettings();
	}
    //部屋作成者は初期プロパティを設定
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
	//マスターに接続されたら
	public override void OnConnectedToMaster()
	{
        //プレイヤー番号をカスタムプロパティに設定
        ExitGames.Client.Photon.Hashtable playerCustomProps = new ExitGames.Client.Photon.Hashtable();
        playerCustomProps.Add("PlayerNum", playerNum);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);
        //部屋に入室
        PhotonNetwork.JoinOrCreateRoom("ROOM.", new RoomOptions(), TypedLobby.Default);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
	public override void OnPlayerEnteredRoom(Player newPlayer)
    //ほかのプレイヤーが入ってきたらローカルにアバターを生成し、オンライン上に生成されてるアンカーをもとにキャリブレーション
	{
        int playerNum = (int)newPlayer.CustomProperties["PlayerNum"];
        StartCoroutine(GetPlayerAnchar(playerNum));
    }
	public override void OnJoinedRoom()//自分が部屋に入った時にアンカーをオンライン上に生成しアバターはローカル上に生成する
	{
        int posnum=0;
        if (playerNum==1) {
            posnum = 0;
        }else
        {
            posnum = 1;
        }
        myMocopiAvatar.position = startPoint[posnum].position;
        //アンカー生成
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
        Transform head = AncharInstantiete(_head, anchar);
        Transform body = AncharInstantiete(_body, anchar);
        Transform leftHand = AncharInstantiete(_leftHand, anchar);
        Transform rightHand = AncharInstantiete(_rightHand, anchar);
        Transform leftFoot = AncharInstantiete(_leftFoot, anchar);
        Transform rightFoot = AncharInstantiete(_rightFoot, anchar);
        
        myAvatar=Instantiate(avatarList[posnum]);
        StartCoroutine(StartCaliblation(myAvatar, head, body, leftHand, rightHand, leftFoot, rightFoot));
        //自身が2人目以降のプレイヤーだった場合、1Pの情報を取得
        if (!PhotonNetwork.IsMasterClient) {
            StartCoroutine(GetPlayerAnchar(GetHostPlayerNum()));
        }
    }
    //部位アンカーを生成
    Transform AncharInstantiete(Transform anchar,GameObject instance)
    { 
        GameObject g=PhotonNetwork.Instantiate(instance.name,anchar.position,anchar.rotation);
        g.transform.SetParent(anchar);
        return g.transform;
    }
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
        Debug.LogError("部屋に入室できねぇ"+message);
	}
	// Update is called once per frame
	void Update()
    {
    }
    //指定したプレイヤーのアンカーを抽出してキャリブレーション開始
    IEnumerator GetPlayerAnchar(int playerNum)
    {
        GameObject avatar = Instantiate(avatarList[(playerNum%2)], Vector3.zero, Quaternion.identity);
        avatar.layer = 1;
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
        StartCoroutine(StartCaliblation(avatar, anchar[0], anchar[1], anchar[2], anchar[3], anchar[4], anchar[5]));
    }
    //引数で受け取ったアバターとそれぞれのアンカーをキャリブレーション
    IEnumerator StartCaliblation(GameObject avatar,Transform head,Transform body,Transform lefthand,Transform righthand,Transform leftFoot,Transform rightFoot)
    {
        //インスタンスが間に合うまで待機
        yield return new WaitUntil(()=>avatar.GetComponent<VRIK>()!=null);
        VRIK ik = avatar.GetComponent<VRIK>();
        //ボタンを押したらキャリブレーション
        while (!Input.GetKeyDown(KeyCode.C)) {
            yield return null;
        }
        VRIKCalibrator.Calibrate(ik,settings,head,body,lefthand,righthand,leftFoot,rightFoot);
    }
	private void OnApplicationQuit()
	{
        Debug.Log("プレイヤーが退室した");
        PhotonNetwork.LeaveRoom();
	}
    int GetHostPlayerNum()
	{
        // ルーム内の全てのプレイヤーを取得
        Player[] players = PhotonNetwork.PlayerList;

        // ホストプレイヤーを見つける
        Player hostPlayer = null;
        foreach (Player player in players)
        {
            if (player.IsMasterClient)
            {
                hostPlayer = player;
                break;
            }
        }
        //　ホストのプレイヤー番号を取得
        Debug.Log((int)hostPlayer.CustomProperties["PlayerNum"]);
        return (int)hostPlayer.CustomProperties["PlayerNum"];
    }

    public static int GetPlayeNum()
	{
        return playerNum;
	}
}
