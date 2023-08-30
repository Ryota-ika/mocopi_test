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
    //���M���Ŏw�̏��𑗐M
    //��M���Ŏw�̏�����M
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            foreach (OculusFinger item in fingers) {
                //�w�^�C�v�����肾�����ꍇ
                if (item.fingerType == OculusFinger.FingerType.L_Index ||
                    item.fingerType == OculusFinger.FingerType.L_Middle ||
                    item.fingerType == OculusFinger.FingerType.L_Pinky ||
                    item.fingerType == OculusFinger.FingerType.L_Ring ||
                    item.fingerType == OculusFinger.FingerType.L_Thumb)
                {

                }
                else/*����ȊO(�E�肾�����ꍇ)*/ { 
                    
                }
            }
        }else{
            //������M
            float jyouhou = 0.5f;
            
		}
    }
}
