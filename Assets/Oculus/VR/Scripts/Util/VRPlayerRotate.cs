using UnityEngine;
using System.Collections;
public class VRPlayerRotate : MonoBehaviour
{
    public float rotationSpeed = 5f; // 回転速度
    public float rotationThreshold = 0.5f; // スティックの入力閾値

    private bool isRotating = false;

    void Update()
    {
        if (!isRotating)
        {
            float horizontalInput = Input.GetAxis("HorizontalR");

            // スティックの入力が閾値を超えたら回転を開始
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

        // 目標の回転角度を設定
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0f, targetAngle, 0f);

        // 回転を補完して適用
        StartCoroutine(RotateOverTime(targetRotation));
    }

    IEnumerator RotateOverTime(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false; // 回転が完了したらフラグをリセット
    }
}