using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class QuizStone : MonoBehaviourPunCallbacks
{
    [SerializeField]
    DestroyWall wall;
	[SerializeField]
	KeyObject key;
    [Header("このオブジェクトが正解かどうか")]
    [SerializeField]
    bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wall==null) {
            if (isCorrect)
            {
                photonView.RPC(nameof(CrearObject),RpcTarget.All);
            }
            else {
                photonView.RPC(nameof(StopPlayer),RpcTarget.All);
            }
        }
    }

    [PunRPC]
	void CrearObject()
	{
        key.SetIsCleard();
        Debug.Log("正解オブジェクトがインタラクトされた");
	}
    [PunRPC]
	void StopPlayer()
	{

	}
}
