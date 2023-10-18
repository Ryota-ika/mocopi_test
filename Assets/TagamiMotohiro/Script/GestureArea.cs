using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestureArea : MonoBehaviourPunCallbacks
{
    [Header("�v���C���[")]
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
        //�W�F�X�`���[�G���A�͈͓̔��ɓ�������v���C���[�����̈ʒu�ɌŒ肵�Đ�������܂œ����Ȃ�����
        if (Vector3.Distance(transform.position,Player.transform.position)<radius&&Player.GetIsCanWalk()&&!collected)
        {
            Player.transform.position = transform.position;
            photonView.RPC(nameof(StartGestureQuiz),RpcTarget.All);
            Player.SetIsCanWalk(false);
            Debug.Log("�W�F�X�`���[�ҋ@��Ԃɓ�����");
        }
    }
    [PunRPC]
	void StartGestureQuiz()
	{
        StartCoroutine(CheckQuizCleard());
	}
    //�Ή�����N�C�Y�p�̃X�N���v�g���N���A��ԂɂȂ��Ă��邩�Ď�
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
        Debug.Log("�N�C�Y���N���A���ꂽ");
        collected = true;
    }
}
