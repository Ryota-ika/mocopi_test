using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour//����S���@�c��
    //�~�b�V������B��������h�A���J����X�N���v�g
{
    [Header("�h�A�̃A�j���[�^�[")]
    [SerializeField]
    Animator animator;
    [Header("�L�[�A�C�e��")]
    // ���ꂪ���ׂăN���A�����΃h�A���J��
    [SerializeField]
    List<KeyObject> keyObjects = new List<KeyObject>();
    bool isOpen = false;
    [SerializeField]
    BoxCollider doorColider;
    AudioSource myAS;
    [SerializeField]
    AudioClip doorOpenSE;
    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenDerection()&& !isOpen)
        {
            
            animator.SetTrigger("Open");
            isOpen = true;
            doorColider.enabled = false;
            myAS.PlayOneShot(doorOpenSE);
        }
    }
    // �L�[�ƂȂ�I�u�W�F�N�g�����ׂăN���A����Ă��邩�m�F
    bool OpenDerection()
    {
        bool result=true;
        if (keyObjects != null)
        {
            foreach (KeyObject item in keyObjects)
            {
                if (!item.GetIsCleard())
                {
                    result = false;
                }
            }
        }else
		{
            result = true;
		}
        return result;
    }
}
