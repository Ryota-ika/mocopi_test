//9åé13ì˙ çÇã¥ó¡ëæ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureChest : MonoBehaviour
{
    [SerializeField]
    private
    float openAngle = 90f; //ïÛî†ÇÃäWÇäJÇ≠äpìx
    [SerializeField]
    private
    float openTime = 2f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public GameObject chestHinge;
    private Animator animator;
    [SerializeField]
    private GameObject Animated_Chest_01;

    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private MocopiPlayerWork mocopiPlayerWork;
    private bool hasTalkingTresureChst = false;
    [SerializeField]
    private float targetDistance = 1f;

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
        naviTextVoiceCtrl.PlayTextVoice(8, 8);
        StartCoroutine(naviTextVoiceCtrl.DelateText(5));
    }
    private void Update()
    {
        if (mocopiPlayerWork.GetIsCanWalk())
        {
            if (Animated_Chest_01 != null)
            {
                float distace = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
                if (distace <= targetDistance && !hasTalkingTresureChst)
                {
                    naviTextVoiceCtrl.PlayTextVoice(0, 0);
                    StartCoroutine(naviTextVoiceCtrl.DelateText(5));
                    hasTalkingTresureChst = true;
                }
            }

        }


    }
}
