using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class QuizTorch : MonoBehaviourPunCallbacks
{
    //田上　エリア２のたいまつを使う問題のスクリプト
    //note
    //QuizStoneとQuizTorchはいずれ統合させたい
    [SerializeField]
    List<TorchControll> torch_list;
    [SerializeField]
    int collectNum;
    [SerializeField]
    List<Animator> openDoor;
    bool isCleard = false;
    bool isPenaltyTime = false;
    [SerializeField]
    AudioClip collectSE;
    [SerializeField]
    AudioClip unCollectSE;
    AudioSource myAS;
    
    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //たいまつの状態を監視し、クリアされているか確認
        for(int i=0; i<torch_list.Count; i++) {
            if (torch_list[i].GetIsCleard())
            {
                if (i != collectNum && !isCleard)
				{
                    Debug.Log(i + "たいまつ不正解");
                    torch_list[i].SetIsCleard();
                    photonView.RPC(nameof(StopPlayer),RpcTarget.All,i);
                    isPenaltyTime = true;
                    return;
				}

                if (i == collectNum && !isCleard)
                {
                    photonView.RPC(nameof(Clear), RpcTarget.All,i);
                    isCleard = true;
                    Debug.Log(i+"たいまつ正解");
                    return;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
		{
            photonView.RPC(nameof(Clear), RpcTarget.All, 0);
            isCleard = true;
            return;
        }
    }
    [PunRPC]
    void Clear(int collectnum)
    {
        torch_list[collectnum].SetIsCleard();
        StartCoroutine(durationPlaySE(2,collectSE));
    }
    [PunRPC]
    void StopPlayer(int num)
    {
        torch_list[num].SetIsCleard();
        StartCoroutine(durationPlaySE(2,unCollectSE));
        foreach (TorchControll torch in torch_list)
		{
            StartCoroutine(torch.BanedTorchFire(10f));
		}
    }
    IEnumerator durationPlaySE(float durationTime,AudioClip SE)
	{
        //火をつけてしばらくたったらSE再生
        yield return new WaitForSeconds(durationTime);
        myAS.PlayOneShot(SE);
        //クリアしていたら出口をふさぐ岩を動かす
        if (isCleard)
		{
            foreach (Animator item in openDoor)
		    {
                item.SetTrigger("Rock_move");
		    }
		}
	}
    public bool getIsCleard()
	{
        return isCleard;
	}
    public void StartQuiz()
    {
        for (int i = 0; i < torch_list.Count; i++)
        {
            torch_list[i].setIsCanFire(true);
        }
    }
}
