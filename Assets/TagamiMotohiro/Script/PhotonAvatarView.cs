using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonAvatarView : MonoBehaviour, IPunObservable
{
    [Header("アンカー情報リスト")]
    [SerializeField]
    List<Transform> childAnchorList = new List<Transform>();
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //子オブジェクトのデータ送受信
    {
        if (stream.IsWriting)//送信側の場合
        {
            stream.SendNext(childAnchorList.Count);
            //アンカーの要素数をstreamに送る
            foreach (Transform child in childAnchorList)
            {
                //一つずつpositionの情報をstreamに送る
                stream.SendNext(child.position);
                stream.SendNext(child.rotation);
            }
        }
        else
        {//受信側の場合
            int childCount = (int)stream.ReceiveNext();
            Debug.Log(childCount);
            for (int i = 0; i < childCount; i++)
            {
                //一つずつstreamから変換データを取得
                Vector3 position = (Vector3)stream.ReceiveNext();
                Quaternion rotation = (Quaternion)stream.ReceiveNext();
                Debug.Log(position);
                //変換を反映
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
