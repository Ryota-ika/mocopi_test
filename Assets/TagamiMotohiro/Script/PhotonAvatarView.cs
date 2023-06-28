using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonAvatarView : MonoBehaviour, IPunObservable
{
    [Header("�A���J�[��񃊃X�g")]
    [SerializeField]
    List<Transform> childAnchorList = new List<Transform>();
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //�q�I�u�W�F�N�g�̃f�[�^����M
    {
        if (stream.IsWriting)//���M���̏ꍇ
        {
            stream.SendNext(childAnchorList.Count);
            int i = 0;
            //�A���J�[�̗v�f����stream�ɑ���
            foreach (Transform child in childAnchorList)
            {
                //�����position�̏���stream�ɑ���
                stream.SendNext(child.position);
                stream.SendNext(child.rotation);
                if (i == 108)
                {
                    Debug.Log("���鑤 " + i.ToString() + " Position:" + child.position);
                }
                //Debug.Log(i.ToString() + " Rotation:" + child.rotation);
                i++;
            }
        }
        else
        {//��M���̏ꍇ
            int childCount = (int)stream.ReceiveNext();
            Debug.Log(childCount);
            for (int i = 0; i < childCount; i++)
            {
                //�����stream����ϊ��f�[�^���擾
                Vector3 position = (Vector3)stream.ReceiveNext();
                Quaternion rotation = (Quaternion)stream.ReceiveNext();
                if (i == 108)
                {
                    Debug.Log("�󂯂鑤 " + i.ToString() + "Position:" + position);
                }
                //�ϊ��𔽉f
                Transform child = childAnchorList[i];
                child.position = position;
                child.rotation = rotation;
            }
        }
    }
    public void addAnchorList(Transform item)
    {
        childAnchorList.Add(item);
    }
}
