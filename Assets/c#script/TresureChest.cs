//6ŒŽ22“ú
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureChest : MonoBehaviour
{
    public float openAngle = 90f; //•ó” ‚ÌŠW‚ðŠJ‚­Šp“x
    public float openTime = 2f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float timer = 0f;
    private bool isOpen = false;

    public GameObject chestHinge;
    private Animator animator;
    [SerializeField]
    private GameObject Animated_Chest_01;
    // Start is called before the first frame update

    private void Start()
    {
        /*initialRotation = transform.rotation;

        targetRotation = Quaternion.Euler(0f, openAngle, 0f);*/
        animator = chestHinge.GetComponent<Animator>();
        //Animated_Chest_01 = GameObject.Find("Animated_Chest_01 (1)");
    }

    private IEnumerator DelaydMethodCoroutine(float delayTime)
    {
        //‚R•bŒã‚É•Ô‚·
        yield return new WaitForSeconds(delayTime);

        Destroy(Animated_Chest_01);
        Animated_Chest_01.SetActive(false);
    }
    private void Update()
    {
        /*timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / openTime);
        transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t); */
        /*animator.SetBool("Open", true);*/
    }

    public void OpenLid()
    {
        /*isOpen = true;
        timer = 0f;*/
        /*transform.rotation = targetRotation;*/
        animator.SetBool("Open", true);
        float delayTime = 3.0f;
        StartCoroutine(DelaydMethodCoroutine(delayTime));
    }

    public void CloseLid()
    {
        /*isOpen = false;
        timer = 0f;
        transform.rotation = initialRotation;*/
        /*Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = targetRotation;*/
    }
}
