using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCanvas : MonoBehaviour
{
    [SerializeField]
    private Transform camera = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera);
    }
}
