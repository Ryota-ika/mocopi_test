using UnityEngine;
using System.Collections;
public class VRPlayerRotate : MonoBehaviour
{
    public float rotationSpeed = 5f; // ��]���x
    public float rotationThreshold = 0.5f; // �X�e�B�b�N�̓���臒l

    private bool isRotating = false;

    void Update()
    {
        if (!isRotating)
        {
            float horizontalInput = Input.GetAxis("HorizontalR");

            // �X�e�B�b�N�̓��͂�臒l�𒴂������]���J�n
            if (Mathf.Abs(horizontalInput) > rotationThreshold)
            {
                RotatePlayer(horizontalInput > 0 ? 45f : -45f);
            }
        }
    }

    void RotatePlayer(float targetAngle)
    {
        isRotating = true;

        Quaternion currentRotation = transform.rotation;

        // �ڕW�̉�]�p�x��ݒ�
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0f, targetAngle, 0f);

        // ��]��⊮���ēK�p
        StartCoroutine(RotateOverTime(targetRotation));
    }

    IEnumerator RotateOverTime(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false; // ��]������������t���O�����Z�b�g
    }
}