//9åé13ì˙ çÇã¥ó¡ëæ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureChest : MonoBehaviour
{
    [SerializeField] private
    float openAngle = 90f; //ïÛî†ÇÃäWÇäJÇ≠äpìx
    [SerializeField] private
    float openTime = 2f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public GameObject chestHinge;
    private Animator animator;
    [SerializeField]
    private GameObject Animated_Chest_01;
    // Start is called before the first frame update

    private void Start()
    {
        animator = chestHinge.GetComponent<Animator>();
    }

    private IEnumerator DelaydMethodCoroutine(float delayTime)
    {
        //ÇRïbå„Ç…ï‘Ç∑
        yield return new WaitForSeconds(delayTime);

        Destroy(Animated_Chest_01);
        Animated_Chest_01.SetActive(false);
    }

    public void OpenLid()
    {
        animator.SetBool("Open", true);
        float delayTime = 3.0f;
        StartCoroutine(DelaydMethodCoroutine(delayTime));
        GetComponent<AudioSource>().Play();
    }
}
