using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class QuizStone : MonoBehaviourPunCallbacks
{
    [SerializeField]
    DestroyWall[] wall;
	[SerializeField]
	KeyObject[] key;
    [Header("正解のオブジェクトの配列番号")]
    [SerializeField]
    int collectNum;
    [SerializeField]
    AudioClip collectSE;
    [SerializeField]
    AudioClip incollectSE;
    AudioSource myAS;
    int objectNum=0;
    // Start is called before the first frame update
    void Start()
    {
        myAS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<key.Length;i++)
	    {
            if (wall[i] != null||key[i].GetIsCleard()) { continue; } 
            if (i == collectNum)
			{
                key[i].SetIsCleard();
                objectNum = i;
                photonView.RPC(nameof(CrearObject),RpcTarget.All);
			}
            else
			{
                Debug.Log("不正解(岩)");
                objectNum = i;
                photonView.RPC(nameof(StopPlayer),RpcTarget.All);
			}
	    }
    }
	public void StartQuiz()
	{
        for (int i = 0; i < wall.Length; i++)
		{
            wall[i].SetIsCanblake(true);
		}
	}
    [PunRPC]
	void CrearObject()
	{
        myAS.PlayOneShot(collectSE);
        Debug.Log("正解オブジェクトがインタラクトされた");
        key[objectNum].SetIsCleard();
	}
    [PunRPC]
	void StopPlayer()
	{
        myAS.PlayOneShot(incollectSE);
        key[objectNum].SetIsCleard();
        Debug.Log("不正解オブジェクトがインタラクトされた");
        for (int i=0;i<wall.Length;i++)
		{
            StartCoroutine(wall[i].bannedDestroy(10));
		}
	}
    public bool isQuizCollected()
	{
        return key[collectNum].GetIsCleard();
	}
}
