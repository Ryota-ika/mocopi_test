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
    [SerializeField]
    GameObject wallHitEffect;
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
    [Header("�����X�s�[�h")]
    [SerializeField]
    float stepSpeed;
    [Header("���������臒l")]
    [SerializeField]
    float stepThreshold;
    [Header("��������")]
    [SerializeField]
    bool isStart = false;
    [SerializeField]
    float rayOfset;
    Vector3 lateFootPos;
    bool isStepping=false;
    bool workWeigting=true;
    bool isCollisionWall = false;
    [SerializeField]
    LayerMask mask;
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
        float footvelocity = 0;
        Debug.Log(logtext+"�ҋ@��");
        while (!isStepping) {

            footvelocity = lateFootPos.y - foot.position.y;
            if (Mathf.Abs(footvelocity) > stepThreshold && !isStepping)
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
        Vector3 avatarfoward = avater.forward;//�A�o�^�[�̐��ʃx�N�g�������
        avatarfoward.y = 0;//��֍s���Ȃ��悤��y��0��
        avatarfoward = avatarfoward.normalized;//0�ɂ����l�𐳋K��
        Vector3 targetPoint = transform.position+(avatarfoward*stepLenge);
        float distanceToTarget = Vector3.Magnitude(targetPoint - transform.position);
        float t = 0;//�X�e�b�v�o�H�⊮�p�̎���t�ϐ�
		while ((distanceToTarget>0.1f||t<stepLenge)&&!isCollisionWall)
		{
            float stepDistance = Mathf.MoveTowards(0,distanceToTarget,Time.deltaTime*stepLenge);
            Vector3 nowPos = Vector3.MoveTowards(transform.position,targetPoint,stepDistance);
            myRigidBody.MovePosition(nowPos);
			t += Time.deltaTime*stepSpeed;
			yield return null;
            distanceToTarget= Vector3.Magnitude(targetPoint - transform.position);
        }
        isStepping = false;
        isCollisionWall = false;
    }

	private void OnTriggerStay(Collider other)
	{
        if (other.gameObject.tag=="Wall") {
            isCollisionWall = true;
        }
	}
	private void OnTriggerExit(Collider other)
	{
        isCollisionWall = false;
	}
}
