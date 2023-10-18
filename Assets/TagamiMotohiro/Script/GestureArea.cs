using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestureArea : MonoBehaviourPunCallbacks
{
    [Header("プレイヤー")]
    [SerializeField]
     MocopiPlayerWork Player;
    [SerializeField]
    float radius;
    bool collected=false;

    [SerializeField]
    QuizStone quizStone;
    [SerializeField]
    QuizTorch quizTorch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ジェスチャーエリアの範囲内に入ったらプレイヤーを特定の位置に固定して正解するまで動けなくする
        if (Vector3.Distance(transform.position,Player.transform.position)<radius&&Player.GetIsCanWalk()&&!collected)
        {
            Player.transform.position = transform.position;
            photonView.RPC(nameof(StartGestureQuiz),RpcTarget.All);
            Player.SetIsCanWalk(false);
            Debug.Log("ジェスチャー待機状態に入った");
        }
    }
    [PunRPC]
	void StartGestureQuiz()
	{
        StartCoroutine(CheckQuizCleard());
	}
    //対応するクイズ用のスクリプトがクリア状態になっているか監視
    IEnumerator CheckQuizCleard()
	{
        if (quizStone != null)
        {
            quizStone.StartQuiz();
            while (quizStone.isQuizCollected())
            {
                yield return null;
            }
        }
        if (quizTorch != null)
		{
            quizTorch.StartQuiz();
            while (quizTorch.getIsCleard())
			{
                yield return null;
			}
		}
        Debug.Log("クイズがクリアされた");
        collected = true;
    }
}
