using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControll : MonoBehaviour
{
    [SerializeField]
    Transform torch;
    [SerializeField]
    GameObject[] setActiveObject;
    bool isFire = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,torch.position)<=0.6f) {
            foreach (GameObject item in setActiveObject)
            {
                item.SetActive(true);
            }
            isFire = true;
            Debug.Log("“_‰Î‚µ‚½");
        }
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "torch") {
            
        }
	}
}
