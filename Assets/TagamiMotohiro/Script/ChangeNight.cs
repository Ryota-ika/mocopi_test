using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNight : MonoBehaviour
{
    [SerializeField]
    Light directionalLight;
    [SerializeField]
    Material nightSky;
    [SerializeField]
    float radius;
    [SerializeField]
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position,transform.position)<radius)
		{
            ChangeSky();
		}
    }
	private void ChangeSky()
	{
        RenderSettings.ambientSkyColor = Color.black;
        directionalLight.transform.rotation=Quaternion.AngleAxis(-90f,Vector3.right);
        RenderSettings.skybox = nightSky;
	}
}
