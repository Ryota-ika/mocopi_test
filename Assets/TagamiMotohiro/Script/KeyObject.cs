using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
	//����S���@�c��
	//������B��������I���ɂȂ�n�I�u�W�F�N�g�̊��N���X
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
