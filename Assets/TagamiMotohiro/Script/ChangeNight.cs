using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNight : MonoBehaviour
{
    [SerializeField]
    Light directionalLight;
    [SerializeField]
    Material nightSky;
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
        directionalLight.color = Color.black;
        directionalLight.transform.rotation=Quaternion.AngleAxis(-90f,Vector3.right);
        RenderSettings.skybox = nightSky;
	}
}
