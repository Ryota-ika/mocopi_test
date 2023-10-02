//９月１日　髙橋涼太
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [SerializeField]
    private float maxDurability = 10.0f;  //壁の最大耐久度
    [SerializeField]
    private float currentDurability;　　//現在の耐久度
    [SerializeField] private GameObject Axe;
    public Rigidbody[] pieces;
    private OVRInput.Controller controller;
    bool isCanDestroy=true;
    //public float minRequireForce = 50.0f; //壁を壊す最低限の力
    [SerializeField]
    private NaviTextVoiceCtrl naviTextVoiceCtrl;

    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;  //初期化
    }

    public void OnAxeHit(float hitSpeed)
    {
        float damage = hitSpeed;
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
        //壁を壊す処理（アニメーションの再生やモデルの変更
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        foreach (Rigidbody item in pieces)
        {
            item.freezeRotation = false;
            item.constraints = FreezeCancellation();
        }
        //naviTextVoiceCtrl.PlayTextVoice(0,7);
        //naviTextVoiceCtrl.StartCoroutine(naviTextVoiceCtrl.DelateText(5));
        StartCoroutine(InvokeDestroy(3));
        //Axe.SetActive(false);
        //Destroy(Axe);
    }
    //指定の秒数自分を破壊できなくする
    public IEnumerator bannedDestroy(int bannedTime)
	{
        isCanDestroy = false;
        yield return new WaitForSeconds(bannedTime);
        isCanDestroy = true;
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

    }
}
