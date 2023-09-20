using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class QuizTorch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    List<KeyObject> torch_list;
    [SerializeField]
    int collectNum;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (torch_list[collectNum].GetIsCleard())
		{
            photonView.RPC(nameof(Clere),RpcTarget.All);
		}else
		{
            photonView.RPC(nameof(StopPlayer), RpcTarget.All);
		}
    }
    [PunRPC]
    void Clere()
    {
        Destroy(this.gameObject);
    }
    [PunRPC]
    void StopPlayer()
    {

    }
}
