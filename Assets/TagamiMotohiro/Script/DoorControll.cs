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
