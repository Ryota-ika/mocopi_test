using System.Collections; //作成日時　5/27 
using System.Collections.Generic;
using UnityEngine;
//using UniRx;

public class MocopiPlayerWork : MonoBehaviour//足のボーンの上下を検知して前進する
{
    public enum FootState { //歩行待機中が右足か左足か
        RIGHT,
        LEFT
    };
    [SerializeField]
    FootState state;
    [SerializeField]
    GameObject wallHitEffect;
    [Header("アバター")]
    [SerializeField]
    Transform avater;
    [Header("自分のRigidBody")]
    [SerializeField]
    Rigidbody myRigidBody;
    [Header("右足")]
    [SerializeField]
    Transform rightFoot;
    [Header("左足")]
    [SerializeField]
    Transform leftFoot;
    [Header("一歩の距離")]
    [SerializeField]
    float stepLenge;
    [Header("歩くスピード")]
    [SerializeField]
    float stepSpeed;
    [Header("歩く判定の閾値")]
    [SerializeField]
    float stepThreshold;
    [Header("準備完了")]
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
		if (!isStart) { return; }//準備完了まで待つ
        if (workWeigting) {
            if (state == FootState.RIGHT)
            {
                StartCoroutine(WorkControll(rightFoot, "右足"));
                workWeigting = false;
                state = FootState.LEFT;
            }
            else if (state==FootState.LEFT) {
                StartCoroutine(WorkControll(leftFoot, "左足"));
                workWeigting = false;
                state = FootState.RIGHT;
            }
        }
		
	}
    IEnumerator WorkControll(Transform foot,string logtext)//足の動きを見て歩いたか判定する
    {
        lateFootPos = foot.position;
        float footvelocity = 0;
        Debug.Log(logtext+"待機中");
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
        Debug.Log(logtext + "完了"); 
    }
    IEnumerator Step()
    {
        Vector3 avatarfoward = avater.forward;//アバターの正面ベクトルを取る
        avatarfoward.y = 0;//上へ行かないようにyは0に
        avatarfoward = avatarfoward.normalized;//0にした値を正規化
        Vector3 targetPoint = transform.position+(avatarfoward*stepLenge);
        float distanceToTarget = Vector3.Magnitude(targetPoint - transform.position);
        float t = 0;//ステップ経路補完用の時間t変数
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
