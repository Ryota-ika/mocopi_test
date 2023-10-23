using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplationCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject imageObject;
    // Start is called before the first frame update
    void Start()
    {
        imageObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        imageObject.SetActive(true);
    }
}
