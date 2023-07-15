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
    [Header("歩く判定の閾値")]
    [SerializeField]
    float stepThresHold;
    [Header("準備完了")]
    [SerializeField]
    bool isStart = false;
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
        Debug.Log(logtext + "完了"); 
    }
    IEnumerator Step()
    {
        Vector3 avaterfoward = avater.forward;//アバターの正面ベクトルを取る
        avaterfoward.y = 0;//上へ行かないようにyは0に
        avaterfoward = avaterfoward.normalized;//0にした値を正規化
		Vector3 targetPosition = transform.position + (avaterfoward * stepLenge);
		float t = 0;//ステップ経路補完用の時間t変数
		while (Vector3.Magnitude(targetPosition-transform.position)>0.1f&&t<stepLenge)
		{
            if (isCollisionWall)
            {
                //CollisinEnterで変数を変更
                isCollisionWall = false;
                Instantiate(wallHitEffect,transform.position,Quaternion.identity);
                yield break;
            }
            Vector3 nowPos = Vector3.Lerp(transform.position,targetPosition,t);
            this.myRigidBody.MovePosition(nowPos);
			t += Time.deltaTime;
			yield return null;
		}
        isStepping = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isCollisionWall = false;


         }
    }
}
