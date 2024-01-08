using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AvatarRefarence : MonoBehaviour
{
    [MenuItem("�c��/�A�o�^�[�q�I�u�W�F�N�g���ǂݍ���")]
    // Start is called before the first frame update
    public static void GetChildList() {
        GameObject Avatar = GameObject.FindGameObjectWithTag("Avatar");
        PhotonAvatarView view=Avatar.gameObject.GetComponent<PhotonAvatarView>();
        SetChild(Avatar.transform,view);
    }
    public static void SetChild(Transform pearent,PhotonAvatarView view)
    {
        foreach (Transform item in pearent.transform) {
            view.addAnchorList(item);
            if (item.childCount > 0) {
                SetChild(item,view);
                item.gameObject.AddComponent<Photon.Pun.PhotonTransformView>();
            }
        }
        EditorUtility.SetDirty(view);
    }
}
