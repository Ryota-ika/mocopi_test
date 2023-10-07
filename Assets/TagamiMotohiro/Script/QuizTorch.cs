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
    [SerializeField]
    List<Animator> openDoor; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (torch_list[collectNum].GetIsCleard())
		{
            photonView.RPC(nameof(Clear),RpcTarget.All);
		}else
		{
            photonView.RPC(nameof(StopPlayer), RpcTarget.All);
		}
    }
    [PunRPC]
    void Clear()
    {
        foreach (Animator item in openDoor)
		{
            item.SetTrigger("Rock_move");
		}
    }
    [PunRPC]
    void StopPlayer()
    {

    }
}
