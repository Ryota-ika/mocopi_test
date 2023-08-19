using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureArea : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField]
     MocopiPlayerWork Player;
    [SerializeField]
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,Player.transform.position)<radius&&Player.GetIsCanWalk()==true) {
            Player.transform.position = transform.position;
            Player.SetIsCanWalk(false);
            Debug.Log("ジェスチャー待機状態に入った");
        }  
    }
}
