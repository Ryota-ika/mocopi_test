using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRSetUp : MonoBehaviour
{
    //Mocopiの頭の動きとVRのカメラの動きが合わさるととんでもなく酔うので
    //子オブジェクトとかにはせず位置のみ同期
    //そのためのクラス
    [Header ("OVRカメラ")]
    [SerializeField]
    GameObject OVRCamera;
    [Header("頭のTransForm")]
    [SerializeField]
    Transform Head;
    bool isTrakkingStart=false;

    public void CameraSetUp()
    {
        OVRCamera.transform.position = Head.position;
        OVRCamera.transform.rotation = Head.rotation;
        isTrakkingStart = true;
    }
	private void FixedUpdate()
	{
        if (isTrakkingStart)
        {
            OVRCamera.transform.position = Head.position;
        }
	}
}
