using System.Collections; //�쐬�����@5/27 
using System.Collections.Generic;
using UnityEngine;
//using UniRx;

public class MocopiPlayerWork : MonoBehaviour//���̃{�[���̏㉺�����m���đO�i����
{
    public enum FootState { //���s�ҋ@�����E����������
        RIGHT,
        LEFT
    };
    [SerializeField]
    FootState state;
    [Header("�A�o�^�[")]
    [SerializeField]
    Transform avater;
    [Header("������RigidBody")]
    [SerializeField]
    Rigidbody myRigidBody;
    [Header("�E��")]
    [SerializeField]
    Transform rightFoot;
    [Header("����")]
    [SerializeField]
    Transform leftFoot;
    [Header("����̋���")]
    [SerializeField]
    float stepLenge;
    [Header("���������臒l")]
    [SerializeField]
    float stepThresHold;
    [Header("��������")]
    [SerializeField]
    bool isStart = false;
    Vector3 lateFootPos;
    bool isStepping=false;
    bool workWeigting=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!isStart) { return; }//���������܂ő҂�
        if (workWeigting) {
            if (state == FootState.RIGHT)
            {
                StartCoroutine(WorkControll(rightFoot, "�E��"));
                workWeigting = false;
                state = FootState.LEFT;
            }
            else if (state==FootState.LEFT) {
                StartCoroutine(WorkControll(leftFoot, "����"));
                workWeigting = false;
                state = FootState.RIGHT;
            }
        }
		
	}
    IEnumerator WorkControll(Transform foot,string logtext)//���̓��������ĕ����������肷��
    {
        lateFootPos = foot.position;
        Debug.Log(logtext+"�ҋ@��");
        while (!isStepping) {
            float footvelocity = lateFootPos.y - foot.position.y;
            if (Mathf.Abs(footvelocity) > stepThresHold && !isStepping)
            {
                isStepping = true;
                StartCoroutine(Step());
            }
            lateFootPos = foot.position;
            yield return null;
        }
        workWeigting = true;
        isStepping = false;
        Debug.Log(logtext + "����"); 
    }
    IEnumerator Step()
    {
        Vector3 avaterfoward = avater.forward;//�A�o�^�[�̐��ʃx�N�g�������
        avaterfoward.y = 0;//��֍s���Ȃ��悤��y��0��
        avaterfoward = avaterfoward.normalized;//0�ɂ����l�𐳋K��
		Vector3 targetPosition = transform.position + (avaterfoward * stepLenge);
		float t = 0;//�X�e�b�v�o�H�⊮�p�̎���t�ϐ�
		while (Vector3.Magnitude(targetPosition-transform.position)>0.1f&&t<stepLenge)
		{
            Vector3 nowPos = Vector3.Lerp(transform.position,targetPosition,t);
            this.myRigidBody.MovePosition(nowPos);
			t += Time.deltaTime;
			yield return null;
		}
        isStepping = false;
    }
}
