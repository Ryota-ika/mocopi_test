using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour//制作担当　田上
    //ミッションを達成したらドアを開けるスクリプト
{
    [Header("ドアのアニメーター")]
    [SerializeField]
    Animator animator;
    [Header("プレイヤー")]
    [SerializeField]
    Transform player;
    [Header("判定の半径")]
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
