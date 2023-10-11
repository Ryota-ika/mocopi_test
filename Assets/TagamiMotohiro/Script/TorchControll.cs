using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControll : KeyObject
    //����S���@�c��
    //�����܂ɉ΂�����X�N���v�g
{
    [SerializeField]
    Transform torch;
    [SerializeField]
    GameObject[] setActiveObject;
    [SerializeField]
    float colliderRange;
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
		if (!isCleard&&Vector3.Distance(transform.position,torch.position)<=colliderRange) {
            foreach (GameObject item in setActiveObject)
            {
                item.SetActive(true);
            }
            isCleard = true;
            myAS.PlayOneShot(fireSE);
            Debug.Log("�_�΂���");
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
        yield return new WaitForSeconds(banedTime);
        isCanFire = true;
	}
}
