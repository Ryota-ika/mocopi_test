using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLookAt : MonoBehaviour
{
    [Header("�J����")]
    [SerializeField]
    Transform Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera);  
    }
}
