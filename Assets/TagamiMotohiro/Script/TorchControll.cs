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
    [SerializeField]
    float colliderRange;
    [SerializeField]
    bool isCanFire=true;
    [SerializeField]
    AudioSource myAS;
    [SerializeField]
    AudioClip fireSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	protected override void CrearDirection()
	{
		if (!isCleard&&Vector3.Distance(transform.position,torch.position)<=colliderRange&&isCanFire) {
            foreach (GameObject item in setActiveObject)
            {
                item.SetActive(true);
            }
            isCleard = true;
            myAS.PlayOneShot(fireSE);
            Debug.Log("点火した");
        }
	}
	// Update is called once per frame
	void Update()
    {
        CrearDirection();
    }
    public IEnumerator BanedTorchFire(float banedTime)
	{
        isCanFire = false;
        Debug.Log(this.gameObject.name+"たいまつ点火不可");
        yield return new WaitForSeconds(banedTime);
        Debug.Log("たいまつ点火可能");
        isCanFire = true;
	}
    public void setIsCanFire(bool value)
	{
        isCanFire = value;
	}
}
