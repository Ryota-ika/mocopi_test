using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using RootMotion.FinalIK;
using RootMotion.Demos;
using TMPro;

public class CallibrationSanple : MonoBehaviourPunCallbacks//アバターにMocopiのモーションをキャリブレートする
{
    [SerializeField]
    GameObject avatar;
    VRIK ik;
    [Tooltip("The settings for VRIK calibration.")] public VRIKCalibrator.Settings settings;
    [Tooltip("The HMD.")] public Transform headTracker;
    [Tooltip("(Optional) A tracker placed anywhere on the body of the player, preferrably close to the pelvis, on the belt area.")] public Transform bodyTracker;
    [Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's left hand.")] public Transform leftHandTracker;
    [Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's right hand.")] public Transform rightHandTracker;
    [Tooltip("(Optional) A tracker placed anywhere on the ankle or toes of the player's left leg.")] public Transform leftFootTracker;
    [Tooltip("(Optional) A tracker placed anywhere on the ankle or toes of the player's right leg.")] public Transform rightFootTracker;

    [Header("右ひじ")]
    [SerializeField]
    Transform rightElbow;
    [Header("左ひじ")]
    [SerializeField]
    Transform leftElbow;

    [Header("Data stored by Calibration")]
    public VRIKCalibrator.CalibrationData data = new VRIKCalibrator.CalibrationData();

    [Header("カメラセットアップ用のクラス")]
    [SerializeField]
    OVRSetUp setUp;
    bool isCallibration = false;
    bool isOnline=false;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
	public override void OnConnectedToMaster()
	{
        PhotonNetwork.JoinLobby();
    }
	public override void OnJoinedLobby()
	{
        Debug.Log(PhotonNetwork.CurrentLobby.Name+"に接続");
        PhotonNetwork.JoinOrCreateRoom("SanpleRoom",new RoomOptions(),TypedLobby.Default);
	}
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
        Debug.Log("入室に失敗しました"+message);
	}
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
        GameObject[] avatarList = GameObject.FindGameObjectsWithTag("Avatar");
        foreach (GameObject item in avatarList) {
            if (!avatar.GetPhotonView().IsMine)
            {
                
            }
        }
	}
	public override void OnJoinedRoom()
	{
        GameObject avatarInstance=
		PhotonNetwork.Instantiate(avatar.name,Vector3.zero,Quaternion.identity);
        ik = avatarInstance.GetComponent<VRIK>();
        GameObject[] avatarList = GameObject.FindGameObjectsWithTag("Avatar");
        foreach (GameObject item in avatarList)
        {
            if (!avatar.GetPhotonView().IsMine)
            {
                
            }
        }
        isOnline = true;
	}
	// Update is called once per frame
	void Update()
    {
        if (!isOnline) { return; }
        if (isCallibration) {}
        if (Input.GetKey(KeyCode.C)||OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        { 
            //キャリブレーションとカメラのセットアップ
            VRIKCalibrator.Calibrate(ik, settings, headTracker, bodyTracker, leftHandTracker, rightHandTracker, leftFootTracker, rightFootTracker);
            ik.solver.leftArm.bendGoal = leftElbow;
            ik.solver.rightArm.bendGoal = rightElbow;
            setUp.CameraSetUp();
        }
    }
}
