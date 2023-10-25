//2023.10.13�@��������
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [SerializeField]
    private float maxDurability = 10.0f;  //�ǂ̍ő�ϋv�x
    [SerializeField]
    private float currentDurability;//���݂̑ϋv�x
    [SerializeField] private GameObject Axe;
    public Rigidbody[] pieces;
    [SerializeField]
    bool isCanDestroy = true;
    //public float minRequireForce = 50.0f; //�ǂ��󂷍Œ���̗�
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;
    [SerializeField]
    private float targetDistance = 5f;
    private bool hasTalkingCrackedWall = false;//�Ђъ��ꂽ�ǂ̘b���������ǂ����̃t���O
    [SerializeField]
    AudioClip damageSE;
    [SerializeField]
    AudioClip breakSE;
    AudioSource myAS;
    [SerializeField]
    private MocopiPlayerWork mocopiPlayerWork;
    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        currentDurability = maxDurability;  //������
    }

    public void OnAxeHit(float hitSpeed)
    {
        float damage = hitSpeed;
        myAS.PlayOneShot(damageSE);
        DecreaseDurability(damage);
    }

    private void DecreaseDurability(float damage)
    {
        if (!isCanDestroy) { return; }
        StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.LTouch));
        StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.RTouch));

        Debug.Log(damage);
        currentDurability -= damage;

        if (currentDurability <= 0)
        {
            DestroyWallObject();
        }
    }

    private static IEnumerator Vibrate(float duration = 0.1f, float frequency = 0.5f, float amplitude = 0.1f, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        yield return new WaitForSeconds(duration);

        OVRInput.SetControllerVibration(0, 0, controller);
    }
    void DestroyWallObject()
    {
        //�ǂ��󂷏����i�A�j���[�V�����̍Đ��⃂�f���̕ύX
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        myAS.PlayOneShot(breakSE);
        foreach (Rigidbody item in pieces)
        {
            item.freezeRotation = false;
            item.isKinematic = false;
            item.constraints = FreezeCancellation();
        } 
        StartCoroutine(InvokeDestroy(3));
        if (naviTextVoiceCtrl == null) { return; }
        naviTextVoiceCtrl.PlayTextVoice(7,7);
        naviTextVoiceCtrl.StartCoroutine(naviTextVoiceCtrl.DelateText(5));
        //Axe.SetActive(false);
        //Destroy(Axe);
    }
    //�w��̕b��������j��ł��Ȃ�����
    public IEnumerator bannedDestroy(int bannedTime)
    {
        isCanDestroy = false;
        yield return new WaitForSeconds(bannedTime);
        isCanDestroy = true;
    }
    public void SetIsCanblake(bool value)
    {
        isCanDestroy = value;
    }
    IEnumerator InvokeDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        this.gameObject.SetActive(false);
    }
    RigidbodyConstraints FreezeCancellation()
    {
        RigidbodyConstraints c = RigidbodyConstraints.None;
        c = RigidbodyConstraints.FreezePositionX;
        c = RigidbodyConstraints.FreezePositionY;
        c = RigidbodyConstraints.FreezePositionZ;
        return c;
    }
    // Update is called once per frame
    void Update()
    {
        if (mocopiPlayerWork.GetIsCanWalk())
        {
            if (naviTextVoiceCtrl == null) { return; }
            float distance = Vector3.Distance(transform.position, naviTextVoiceCtrl.transform.position);
            if (distance <= targetDistance && !hasTalkingCrackedWall)
            {
                //mocopiPlayerWork.SetIsCanWalk(true);
                naviTextVoiceCtrl.PlayTextVoice(5, 5);
                StartCoroutine(naviTextVoiceCtrl.DelateText(5));
                hasTalkingCrackedWall = true;
            }
        }
    }
}
