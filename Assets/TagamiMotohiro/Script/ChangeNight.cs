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
    [SerializeField]
    GameObject hummer;
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
            hummer.SetActive(false);
		}
    }
	private void ChangeSky()
	{
        RenderSettings.ambientSkyColor = new Color(60/255f, 60/255f, 60/255f, 1);
        directionalLight.transform.rotation=Quaternion.AngleAxis(-90f,Vector3.right);
        RenderSettings.skybox = nightSky;
	}
}
