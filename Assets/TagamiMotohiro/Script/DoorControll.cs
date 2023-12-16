using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour//制作担当　田上
    //ミッションを達成したらドアを開けるスクリプト
{
    [Header("ドアのアニメーター")]
    [SerializeField]
    Animator animator;
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
    bool OpenDerection()
    {
        // 
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
