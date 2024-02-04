using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// �v���C���[�̑ҋ@��Ԃ��Ď�����N���X
public class StartRoomKey : KeyObject
{
    [SerializeField]
    AudioClip countDownSE;
    [SerializeField]
    AudioSource myAS;
    [SerializeField]
    MocopiPlayerWork player;
    static bool _1pStanby = false;
    static bool _2pStanby = false;
    bool stanbyOK = false;
    [SerializeField]
    int countDown = 3;
    [SerializeField]
    TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    AudioSource BGM;
    [SerializeField]
    AudioClip inGameBGM;
    // Start is called before the first frame update
    void Start()
    {
        player.SetIsCanWalk(false);
    }

    // Update is called once per frame
    void Update()
    {
        CrearDirection();
    }
	protected override void CrearDirection()
	{
        // Space�L�[�ŏ�������
        if ((Input.GetKeyDown(KeyCode.Space)||OVRInput.GetDown(OVRInput.RawButton.X))&&AncharCtrl.GetisConnected())
        {
            SetStart();
        }
        // 1P��2P�̏�����Ԃ����������Q�[���J�n
        if (_1pStanby&&_2pStanby&&!stanbyOK)
		{
            stanbyOK = true;
            StartCoroutine(StartCount());
		}
        //�v���C���[��������Ă��Ȃ��Ă������J�n
        if (Input.GetKeyDown(KeyCode.C)||OVRInput.GetDown(OVRInput.RawButton.B))
		{
            stanbyOK = true;
            StartCoroutine(StartCount());
        }
	}
    // ���������ɂ���
    public void SetStart()
	{
        var roomProps = new ExitGames.Client.Photon.Hashtable();
        if (AncharCtrl.GetPlayeNum() == 1)
        {
            roomProps.Add("1pStanby", true);
        }
        else
        {
            roomProps.Add("2pStanby", true);
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
    }
    // �Q�[���J�n�̏���
    IEnumerator StartCount()
	{
        myAS.PlayOneShot(countDownSE);
        BGM.Stop();
        countDownText.text = countDown.ToString();
        while (countDown >= 0)
		{
            countDownText.text = countDown.ToString();
            if (countDown == 0)
			{
                countDownText.text = "START!";
			}
            yield return new WaitForSeconds(1);
            countDown-=1;
		}
        countDownText.gameObject.SetActive(false);
        BGM.PlayOneShot(inGameBGM);
        isCleard = true;
        player.SetIsCanWalk(true);
	}
    // ����v���C���[�̏������������m���ă��[�J���ɔ��f
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("�v���p�e�B���ύX���ꂽ");
        _1pStanby = (bool)PhotonNetwork.CurrentRoom.CustomProperties["1pStanby"];
        _2pStanby = (bool)PhotonNetwork.CurrentRoom.CustomProperties["2pStanby"];
    }
    //�@�e�v���C���[�̏�����Ԃ��擾
    public static bool GetPlayerStanby(int playerNum)
	{
        if (playerNum == 1)
		{
            return _1pStanby;
		}else if (playerNum == 2)
		{
            return _2pStanby;
		}
        return false;
	}
}
