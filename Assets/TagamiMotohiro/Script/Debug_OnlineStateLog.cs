using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

// �I�����C���̐ڑ���ԂƂ����̑��̗v�f���r���h���ł������悤�ɂ���N���X
public class Debug_OnlineStateLog : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    TextMeshProUGUI connectStateText;
    [SerializeField]
    TextMeshProUGUI connectRoomNameText;
    [SerializeField]
    TextMeshProUGUI connectPlayersStateText;
    [SerializeField]
    TextMeshProUGUI naviClothState;

    [SerializeField]
    GameObject naviCloth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���\���ؑ�
        if (Input.GetKeyDown(KeyCode.F11)) {
            canvas.SetActive(!canvas.activeSelf);
        }
        // �ڑ��֘A�̕\��
        if (connectStateText == null) { return; }
        connectStateText.text = "�ڑ��̏�� = " + PhotonNetwork.IsConnected.ToString();
        if (connectRoomNameText == null) { return; } 
        connectRoomNameText.text = "���݂̕����� = " + PhotonNetwork.CurrentRoom.Name;
        if (connectPlayersStateText == null) { return; }
        connectPlayersStateText.text = "�v���C���[�̐� = " + PhotonNetwork.PlayerList.Length;

        // �X�e�[�W�̏�Ԃ̕\��
        naviClothState.text = naviCloth.transform.position.ToString();

    }

}
