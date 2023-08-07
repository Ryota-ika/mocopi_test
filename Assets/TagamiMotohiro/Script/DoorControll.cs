using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour//����S���@�c��
    //�~�b�V������B��������h�A���J����X�N���v�g
{
    [Header("�h�A�̃A�j���[�^�[")]
    [SerializeField]
    Animator animator;
    [SerializeField]
    List<KeyObject> keyObjects = new List<KeyObject>();
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenDerection())
        {
            animator.SetTrigger("Open");
        }
    }
    bool OpenDerection()
    {
        bool result=true;
        foreach (KeyObject item in keyObjects) {
            if (!item.GetIsCleard()) {
                result = false;
            }
        }
        return result;
    }
}
