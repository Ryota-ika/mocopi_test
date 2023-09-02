using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
	//制作担当　田上
	//条件を達成したらオンになる系オブジェクトの基底クラス
{
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
