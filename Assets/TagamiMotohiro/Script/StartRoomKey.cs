using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomKey : KeyObject
{
    [SerializeField]
    MocopiPlayerWork player;
    // Start is called before the first frame update
    void Start()
    {
        player.SetIsCanWalk(false);
    }

    // Update is called once per frame
    void Update()
    {
        CrearDirection();
    }
	protected override void CrearDirection()
	{
        if ((OVRInput.Get(OVRInput.RawButton.RHandTrigger)&&OVRInput.Get(OVRInput.RawButton.LHandTrigger))||
            Input.GetKeyDown(KeyCode.Space)) {
            isCleard = true;
            player.SetIsCanWalk(true) ;
        }
	}
}
