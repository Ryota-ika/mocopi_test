using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mocopi.Receiver;

public class MocopiInstantiateSumple : MonoBehaviour
{
    [SerializeField]
    GameObject avatar;
    [SerializeField]
    MocopiSimpleReceiver receiver;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = Instantiate(avatar,Vector3.zero,Quaternion.identity);
        receiver.AddAvatar(g.GetComponent<MocopiAvatar>(),12351);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
