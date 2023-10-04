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
    [Header("�����̃I�u�W�F�N�g�̔z��ԍ�")]
    [SerializeField]
    int collectNum;
    [SerializeField]
    AudioClip collectSE;
    [SerializeField]
    AudioClip incollectSE;
    AudioSource myAS;
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
                photonView.RPC(nameof(CrearObject),RpcTarget.All,i);
			}
            else
			{
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
	void CrearObject(int objectNum)
	{
        myAS.PlayOneShot(collectSE);
        Debug.Log("�����I�u�W�F�N�g���C���^���N�g���ꂽ");
        key[objectNum].SetIsCleard();
	}
    [PunRPC]
	void StopPlayer(int objectNum)
	{
        myAS.PlayOneShot(incollectSE);
        key[objectNum].SetIsCleard();
        Debug.Log("�s�����I�u�W�F�N�g���C���^���N�g���ꂽ");
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
