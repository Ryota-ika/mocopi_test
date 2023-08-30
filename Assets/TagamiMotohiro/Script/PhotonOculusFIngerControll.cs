using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonOculusFIngerControll : MonoBehaviourPunCallbacks,IPunObservable
{
    OculusFinger[] fingers=new OculusFinger[5];
    float handTriggerValue;
    float indexTriggerValue;
    // Start is called before the first frame update
    void Start()
    {
           
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    //送信側で指の情報を送信
    //受信側で指の情報を受信
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            foreach (OculusFinger item in fingers) {
                //指タイプが左手だった場合
                if (item.fingerType == OculusFinger.FingerType.L_Index ||
                    item.fingerType == OculusFinger.FingerType.L_Middle ||
                    item.fingerType == OculusFinger.FingerType.L_Pinky ||
                    item.fingerType == OculusFinger.FingerType.L_Ring ||
                    item.fingerType == OculusFinger.FingerType.L_Thumb)
                {

                }
                else/*それ以外(右手だった場合)*/ { 
                    
                }
            }
        }else{
            //情報を受信
            float jyouhou = 0.5f;
            
		}
    }
}
