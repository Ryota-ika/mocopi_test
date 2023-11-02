using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyObject : MonoBehaviourPunCallbacks
	//制作担当　田上
	//条件を達成したらオンになる系オブジェクトの基底クラス
{
	[SerializeField]
    protected bool isCleard = false;
	protected virtual void CrearDirection()
	{
	}
    public bool GetIsCleard()
	{
		return isCleard;
	}
	public void SetIsCleard()
	{
		isCleard = true;
	}
}
