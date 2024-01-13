using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
// �W�F�X�`���[�񓚑�������̈ʒu�ɓ��B�����̂����m����N���X
public class RespondentArrivalDetection : MonoBehaviourPunCallbacks{
    [SerializeField]
    GameObject stanbyPanel;
    [SerializeField]
    float detectionRange;
    [SerializeField]
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ArrivalDetection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
	// �񓚑��ɃW�F�X�`���[�����J������
	void GestureDisclosure()
	{
        stanbyPanel.SetActive(false);
	}

    IEnumerator ArrivalDetection()
	{
        while (Vector3.Distance(player.position,transform.position) > detectionRange)
		{
            yield return null;
		}
        photonView.RPC(nameof(GestureDisclosure), RpcTarget.All);
        Destroy(gameObject);
	}
}
