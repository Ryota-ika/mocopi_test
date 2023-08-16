using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Key“–‚½‚Á‚½");
        if (collision.gameObject.tag == "Key")
        {
            Destroy(this.gameObject,0.2f);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Axe")
        {
            Destroy(this.gameObject, 0.2f);
        }
    }
}
