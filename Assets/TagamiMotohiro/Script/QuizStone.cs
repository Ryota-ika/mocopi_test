using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizStone : KeyObject
{
    [SerializeField]
    DestroyWall wall;
    [Header("���̃I�u�W�F�N�g���������ǂ���")]
    [SerializeField]
    bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CrearDirection();
    }
	protected override void CrearDirection()
	{
		if (wall == null)
		{
            isCleard = true;
		}
	}
}
