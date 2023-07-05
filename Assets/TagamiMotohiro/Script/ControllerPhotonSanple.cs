using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControllerPhotonSanple : MonoBehaviourPunCallbacks,IPunObservable
{
	private void Start()
	{
        
	}
	void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.position);
        }
        else {
            transform.rotation = (Quaternion)stream.ReceiveNext();
            transform.position=(Vector3)stream.ReceiveNext();
        }
    }
}

