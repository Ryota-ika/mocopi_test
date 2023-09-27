using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetSpecificPositionSerif : MonoBehaviour
{
    [SerializeField] string objectTag;
    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        objectTag = other.gameObject.tag;
        switch (objectTag)
        {
            case "1st":
                text.text = "aaa";
                break;
            case "2nd":
                text.text = "‚¦‚¦‚¦‚¦‚¦‚¦";
                break;
            case "3rd":
                break;
        }
    }
}
