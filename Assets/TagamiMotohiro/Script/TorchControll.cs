using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControll : KeyObject
    //制作担当　田上
    //たいまつに火をつけるスクリプト
{
    [SerializeField]
    Transform torch;
    [SerializeField]
    GameObject[] setActiveObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	protected override void CrearDirection()
	{
		if (!isCleard&&Vector3.Distance(transform.position,torch.position)<=0.6f) {
            foreach (GameObject item in setActiveObject)
            {
                item.SetActive(true);
            }
            isCleard = true;
            Debug.Log("点火した");
        }
	}
	// Update is called once per frame
	void Update()
    {
        CrearDirection();
    }
}
