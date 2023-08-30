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
    float stepLength;
    [Header("歩くスピード")]
    [SerializeField]
    float stepSpeed;
    [Header("歩く判定の閾値")]
    [SerializeField]
    float stepThreshold;
    [Header("歩行可能かどうか")]
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
		if (!isCanWark) { return; }//準備完了まで待つ
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
        CollisionDirection();
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
        Vector3 targetPoint = transform.position+(avatarfoward*stepLength);
        float distanceToTarget = Vector3.Magnitude(targetPoint - transform.position);
        float t = 0;//ステップ経路補完用の時間t変数
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
    void CollisionDirection()//壁に当たったかどうか判定
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
                if (effectTime<0) {//エフェクトのタイミングを制御
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
