using System.Collections; //作成日時　5/27 
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MocopiPlayerWork : MonoBehaviour//足のボーンの上下を検知して前進する
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!isStart) { return; }//準備完了まで待つ
		float footVelocity = lateFootPos.y - leftFoot.position.y;//足の動きの速さを取る(今は左のみ)
		if (Mathf.Abs(footVelocity) > stepThresHold && !isStepping)
		{
			isStepping = true;
			Debug.Log("歩いた");
			StartCoroutine(Step());
		}
		lateFootPos = leftFoot.position;
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
            Vector3 nowPos = Vector3.Lerp(transform.position,targetPosition,t);
            this.myRigidBody.MovePosition(nowPos);
			t += Time.deltaTime;
			yield return null;
		}
        isStepping = false;
    }
}
