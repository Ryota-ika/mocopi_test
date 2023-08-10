using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(this.gameObject,0.2f);
        }
    }
}
