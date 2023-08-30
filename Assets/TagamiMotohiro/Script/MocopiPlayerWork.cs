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
    float stepLength;
    [Header("�����X�s�[�h")]
    [SerializeField]
    float stepSpeed;
    [Header("���������臒l")]
    [SerializeField]
    float stepThreshold;
    [Header("���s�\���ǂ���")]
    [SerializeField]
    bool isCanWark=false;
    [SerializeField]
    float rayOfset;
    [SerializeField]
    float lastEffectTime;
    float effectTime=0;
    RaycastHit hit;
    Vector3 lateFootPos;
    bool isStepping=false;
    bool workWeigting=true;
    bool isCollisionWall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!isCanWark) { return; }//���������܂ő҂�
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
        CollisionDirection();
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
        Vector3 targetPoint = transform.position+(avatarfoward*stepLength);
        float distanceToTarget = Vector3.Magnitude(targetPoint - transform.position);
        float t = 0;//�X�e�b�v�o�H�⊮�p�̎���t�ϐ�
		while ((distanceToTarget>0.1f||t<stepLength)&&!isCollisionWall)
		{
            float stepDistance = Mathf.MoveTowards(0,distanceToTarget,Time.deltaTime*stepLength);
            Vector3 nowPos = Vector3.MoveTowards(transform.position,targetPoint,stepDistance*stepSpeed);
            myRigidBody.MovePosition(nowPos);
			t += Time.deltaTime;
			yield return null;
            distanceToTarget= Vector3.Magnitude(targetPoint - transform.position);
        }
        isStepping = false;
        isCollisionWall = false;
    }
    void CollisionDirection()//�ǂɓ����������ǂ�������
	{
        Vector3 rayDirection=new Vector3(avater.forward.x,0,avater.forward.z).normalized;
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition+new Vector3(0,1.6f,0),rayDirection);
        Debug.DrawRay(transform.position,rayDirection,Color.black,rayOfset);
        if (Physics.Raycast(ray, out hit, rayOfset))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Wall")
            {
                isCollisionWall = true;
                Vector3 point = hit.point;
                effectTime -= Time.deltaTime;
                if (effectTime<0) {//�G�t�F�N�g�̃^�C�~���O�𐧌�
                    Instantiate(wallHitEffect, point, Quaternion.FromToRotation(hit.point,transform.position));
                    effectTime = lastEffectTime;
                }
                
            }
            else {
                effectTime = 0;
                isCollisionWall = false;
            }
        }
        else {
            effectTime = 0;
            isCollisionWall = false;
        }
	}
    public bool GetIsCanWalk() { 
        return isCanWark;
    }
    public void SetIsCanWalk(bool value)
    {
        isCanWark = value;
    }
}
