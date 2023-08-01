using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour//����S���@�c��
    //�~�b�V������B��������h�A���J����X�N���v�g
{
    [Header("�h�A�̃A�j���[�^�[")]
    [SerializeField]
    Animator animator;
    [Header("�v���C���[")]
    [SerializeField]
    Transform player;
    [Header("����̔��a")]
    [SerializeField]
    float radius;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,player.position)<=radius&&!isOpen) {
            animator.SetTrigger("Open");
            isOpen = false;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
