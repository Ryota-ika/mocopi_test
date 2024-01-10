using System.Collections; //�쐬�����@5/27 
using System.Collections.Generic;
using UnityEngine;

public class MocopiPlayerWork : MonoBehaviour//���̃{�[���̏㉺�����m���đO�i����
{
    public enum FootState { //���s�ҋ@�����E����������
        RIGHT,
        LEFT
    };
    [SerializeField]
    Transform stepPos;
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
    [SerializeField]
    float footRiseThreshold;
    [SerializeField]
    float footDescentMaxTime = 3;
    [Header("���s�\���ǂ���")]
    [SerializeField]
    bool isCanWark=false;
    [SerializeField]
    float rayOfset;
    [SerializeField]
    float lastEffectTime;
    [SerializeField]
    LayerMask mask;
    float effectTime=0;
    RaycastHit hit;
    Vector3 lateFootPos;
    bool isStepping=false;
    bool workWeigting=true;
    bool isCollisionWall = false;
    [Header("�v���C���[�֘A�̌��ʉ�")]
    [SerializeField]
    AudioClip wallHitSE;
    AudioSource myAS;

    // Start is called before the first frame update
    void Start()
    {
        myAS = this.GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        //���������܂ő҂�
        if (!isCanWark) { return; }

        //�E���ƍ����Ō��݂ɑ����݂����m����֐����N��
        if (workWeigting||Input.GetKey(KeyCode.LeftShift)) {
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
    //���̓��������ĕ����������肷��
    IEnumerator WorkControll(Transform foot,string logtext)
    {
        bool isFootRaised = false;
        lateFootPos = foot.position;
        float originFootPos_Y = lateFootPos.y;
        float footDescentTime = 0;
        // �����㏸����܂Ŕ�������
        while (!isFootRaised){
            // ���̏㏸������
            float footRise = Mathf.Abs(lateFootPos.y - foot.position.y);
            // �������ʏ㏸�����玟�֐i��
            if (footRise >= footRiseThreshold)
			{
                isFootRaised = true;
                lateFootPos = foot.position;
			}
            yield return null;
        }
        while (!isStepping)
        {
            if (Mathf.Abs(foot.position.y - originFootPos_Y) <= 0.01f)
			{
                StartCoroutine(Step(footDescentMaxTime - footDescentTime));
				isStepping = true;
            }
            if (footDescentTime > footDescentMaxTime)
			{
                isStepping = true;
			}
            footDescentTime += Time.deltaTime;
        }
        workWeigting = true;
        isStepping = false;
    }
    IEnumerator Step(float stepSpeed)
    {
        Vector3 avatarfoward = avater.forward;//�A�o�^�[�̐��ʃx�N�g�������
        avatarfoward.y = 0;//��֍s���Ȃ��悤��y��0��
        avatarfoward = avatarfoward.normalized;//0�ɂ����l�𐳋K��
        float t = 0;//�X�e�b�v�o�H�⊮�p�̎���t�ϐ�
		while (t<stepLength&&!isCollisionWall)
		{
            if (!isCanWark)
			{
                isStepping = false;
                yield break;
			}
            transform.position+=(avatarfoward*stepSpeed)*Time.deltaTime;
			t += Time.deltaTime;
			yield return null;
			
		}
        isStepping = false;
    }
    void CollisionDirection()//�ǂɓ����������ǂ�������
	{
        Vector3 rayDirection=new Vector3(avater.forward.x,0,avater.forward.z).normalized;
        //avatar�I�u�W�F�N�g�̐���
        if (Physics.SphereCast(transform.position+new Vector3(0,1.6f,0),0.15f,rayDirection,out hit,rayOfset,mask))
        {
            if (hit.collider.tag == "Wall")
            {
                isCollisionWall = true;
                Vector3 point = hit.point;
                effectTime -= Time.deltaTime;
                stepPos.position = hit.point;
                if (effectTime<0) {
                    //�G�t�F�N�g�̃^�C�~���O�𐧌�
                    myAS.PlayOneShot(wallHitSE);
                    Instantiate(wallHitEffect, point, Quaternion.FromToRotation(Vector3.forward,hit.normal));
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
    void OnDrawGizmos()
    {
        //�@Capsule�̃��C���^���I�Ɏ��o��
        Gizmos.color = Color.red;
        Vector3 rayPosition = transform.position;
        Gizmos.DrawWireSphere(rayPosition + new Vector3(avater.forward.x, 0, avater.forward.z).normalized * rayOfset,0.15f);
    }
    public bool GetIsCanWalk() { 
        return isCanWark;
    }
    public void SetIsCanWalk(bool value)
    {
        isCanWark = value;
    }
}
