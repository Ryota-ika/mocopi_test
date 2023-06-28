using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRSetUp : MonoBehaviour
{
    //Mocopi�̓��̓�����VR�̃J�����̓��������킳��ƂƂ�ł��Ȃ������̂�
    //�q�I�u�W�F�N�g�Ƃ��ɂ͂����ʒu�̂ݓ���
    //���̂��߂̃N���X
    [Header ("OVR�J����")]
    [SerializeField]
    GameObject OVRCamera;
    [Header("����TransForm")]
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
