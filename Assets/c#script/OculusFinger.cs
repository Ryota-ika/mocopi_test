using UnityEngine; //�쐬���� 6/29
using System.Collections.Generic;

public class OculusFinger : MonoBehaviour
{
    [TooltipAttribute("Awake���Ɍ��݂�FingerType�ɂ��킹�Ď����ݒ���s����")]
    public bool isAwakeAutoSetup = true;
    [TooltipAttribute("Oculus Touch�ɂ��^�b�`���͂�L���ɂ��邩")]
    public bool isEnableTouchControl = true;

    public enum FingerType
    {
        Custom,     // �Ǝ��ݒ�
        Auto,       // Awake���Ɏw�̖��O���玩���I�ݒ�
        L_Thumb,    // ����e�w
        L_Index,    // ����l�����w
        L_Middle,   // ���蒆�w
        L_Ring,     // �����w
        L_Pinky,    // ���菬�w
        R_Thumb,    // �E��e�w
        R_Index,    // �E��l�����w
        R_Middle,   // �E�蒆�w
        R_Ring,     // �E���w
        R_Pinky,    // �E�菬�w
    }

    [TooltipAttribute("�w�̃^�C�v")]
    public FingerType fingerType = FingerType.Auto;

    [HeaderAttribute("Oculus Touch Settings")]

    [TooltipAttribute("�G���Ǝw�𔼕��Ȃ���ߐڃZ���T�[�{�^����")]
    public List<OVRInput.RawTouch> touchButtonPool;
    [TooltipAttribute("����Ǝw���Ȃ���g���K�[�itouchButtonPool�w�莞�͔����`�Ō�܂Łj")]
    public OVRInput.RawAxis1D trigger = OVRInput.RawAxis1D.None;
    [Range(0, 0.99f), TooltipAttribute("�g���K�[���g���Ƃ��A�w���Ȃ��n�߂�J�n�ʒu")]
    public float triggerStart = 0.0f;
    [TooltipAttribute("�G��Ă��ĂȂ����g���K�[�������Ȃ����Ƃ��Ɏw�𔼕��Ȃ���ߐڃZ���T�[�{�^��")]
    public OVRInput.RawTouch relatedTouchButton;

    [HeaderAttribute("Joint Settings")]

    [SerializeField, TooltipAttribute("���{����w��ɂȂ����Ă����֐߂�ݒ�i���w��̂Ƃ�Awake�Ŏ����ݒ�j")]
    private List<Transform> jointPool;
    [SerializeField, TooltipAttribute("�e�֐߂����{�̊p�x�ɑ΂��Ăǂ̊����ŋȂ��邩��ݒ�i���w��̂Ƃ�Awake�Ŏ����ݒ�j")]
    private List<float> jointLevelPool;
    private List<Quaternion> jointBaseRotPool; // �e�֐߂̏����p���iAwake���Ɏ����L���j

    [HeaderAttribute("Finger Angle")]
    [TooltipAttribute("�w�֐߂̉�]��")]
    public Vector3 axis = Vector3.up;
    [TooltipAttribute("���݂̉�]�p�x�i���s���ɓ��I�ɕς��j")]
    public float angle = 0.0f;
    [TooltipAttribute("�ő�̉�]�p�x")]
    public float maxAngle = 90.0f;
    [Range(0, 1), TooltipAttribute("�w���J���Ƃ��̕�Ԃ̑���")]
    public float openLerpLevel = 0.3f;
    [Range(0, 1), TooltipAttribute("�w�����Ƃ��̕�Ԃ̑���")]
    public float closeLerpLevel = 0.15f;
    [TooltipAttribute("�e�w���i�e�w�𗧂Ă���悤�ɂ��邩�j")]
    public bool isThumb = false;
    private bool isThumbsUp = false; // �e�w�𗧂ĂĂ��邩

    void Awake()
    {
        if (isAwakeAutoSetup) { AutoSetup(); }

        jointBaseRotPool = new List<Quaternion>();
        foreach (Transform joint in jointPool)
        {
            jointBaseRotPool.Add(joint.transform.localRotation);
        }
    }

    // ���݂�FingerType�ɂ��킹�Ċe��ݒ�������ݒ�i�R���|�[�l���g�̃��j���[��������s�\�j
    [ContextMenu("Automatic Setup")]
    void AutoSetup()
    {
        AutoSetupJoint();
        SetupFingerType(fingerType);
    }

    // �w�̍��{��OculusFinger�R���|�[�l���g�������Ƃ��āA���̎q���̍ŏ��ɂ�����̂�
    // ���Ɋ֐߂Ƃ��āA���[�̑O�܂Ŏ����ݒ肷��i�蓮�ݒ�ς݂̏ꍇ�͂��̂܂܁j
    // ���킹�āA�֐߂��Ȃ���ʂ��ʂɐݒ�\�ɂ���
    void AutoSetupJoint()
    {
        if (jointPool.Count <= 0)
        {
            Transform t = transform;
            while (t && t.childCount > 0)
            {
                jointPool.Add(t);
                jointLevelPool.Add(1.0f);
                t = t.GetChild(0);
            }
        }
    }

    // GameObject�̖��O���w�̖��O�̂Ƃ��w�^�C�v�ƃ{�^�����]���������ݒ�
    void AutoSetupFingerType()
    {
        FingerType type = FingerType.Custom;
        string name = transform.name.ToLower();
        string[] typeNames = { "thumb", "index", "middle", "ring", "pinky" };
        int typeIndex = (int)FingerType.L_Thumb;
        if (name.IndexOf("right") >= 0) { typeIndex = (int)FingerType.R_Thumb; }

        for (int i = 0; i < typeNames.Length; i++)
        {
            if (name.IndexOf(typeNames[i]) >= 0)
            {
                type = (FingerType)(typeIndex + i);
                break;
            }
        }
        SetupFingerType(type);
    }

    void Update()
    {
        if (!isEnableTouchControl) return;

        // Oculus Touch�̋ߐڃZ���T�[�t���{�^�����w�肵�Ă��āA�G��Ă�����w�𔼕��Ȃ���
        float touchLevel = 0.0f;
        foreach (OVRInput.RawTouch touch in touchButtonPool)
        {
            if (OVRInput.Get(touch)) { touchLevel = 0.5f; break; }
        }

        // �ߐڃZ���T�[���w�肵�Ă��āA�Ȃ����i���̎w�́j�g���K�[�������ł��Ђ��Ă�����w�𔼕��Ȃ���
        bool isRelatedTouch = OVRInput.Get(relatedTouchButton) && OVRInput.Get(trigger) > 0.1f;
        if (isRelatedTouch) touchLevel = 0.5f;

        // �ߐڃZ���T�[�ɐG��Ă邩�A�ߐڃZ���T�[���w�肵�Ă��Ȃ��Ƃ��A�g���K�[�ŋȂ���
        if (touchLevel > 0.0f || touchButtonPool.Count <= 0)
        {
            float triggerLevel = (OVRInput.Get(trigger) - triggerStart) / (1 - triggerStart);
            triggerLevel = Mathf.Clamp01(triggerLevel);
            if (touchLevel > 0.0f) { triggerLevel *= 0.5f; } // �ߐڃZ���T�[���g���Ă���Ƃ��͎c����g���K�[�ŋȂ���
            touchLevel += triggerLevel;
        }

        // �e�w�̂Ƃ��A�i���w�A���w�Ȃǂ́j�w��̃g���K�[�������Ă��āA�i�e�w�́j�ߐڃZ���T�[����w������Ă�����e�w�𗧂Ă郂�[�h�ɂ���
        isThumbsUp = (isThumb && touchLevel <= 0.05f && OVRInput.Get(trigger) > triggerStart);
        if (isThumbsUp) { touchLevel = -0.5f; }

        // �w���ԗʂɂ��킹�ĕ��
        float lerpLevel = (touchLevel <= 0.0f) ? openLerpLevel : closeLerpLevel;
        if (touchLevel > 0.0f && touchButtonPool.Count > 0 && OVRInput.Get(trigger) < 0.1f) { lerpLevel *= 0.5f; } // ������ƋߐڃZ���T�[�ɐG�ꂽ�����̂Ƃ��͂�������Ԃ���
        angle = Mathf.Lerp(angle, maxAngle * touchLevel, lerpLevel);
    }

    void LateUpdate()
    {
        if (jointPool.Count != jointBaseRotPool.Count && jointPool.Count != jointBaseRotPool.Count)
        {
            Debug.LogError("jointData Error.");
            return;
        }

        // �w�̊p�x�ɂ��킹�Ċe�֐߂̎p��������
        for (int i = 0; i < jointPool.Count; i++)
        {
            Transform joint = jointPool[i];
            float jointLevel = jointLevelPool[i];
            if (isThumbsUp) { jointLevel = 0.5f - i * 0.1f; }
            float rot = angle * jointLevel;
            Quaternion jointBaseRot = jointBaseRotPool[i];
            joint.localRotation = jointBaseRot * Quaternion.AngleAxis(angle * jointLevel, axis);
        }
    }

    // �w�^�C�v�ɂ��킹�ă{�^�����]���������ݒ�
    // �i�L�����N�^�[�ɂ���Ē������K�v�ȏꍇ�͉��L���e�����������邩�AInspector��Ŏ蓮�ݒ肷��j
    void SetupFingerType(FingerType type)
    {
        this.fingerType = type;
        if (type == FingerType.Custom) return;
        if (type == FingerType.Auto) { AutoSetupFingerType(); return; }

        touchButtonPool.Clear();
        relatedTouchButton = OVRInput.RawTouch.None;
        triggerStart = 0.0f;
        axis = new Vector3(0, 1, 0);
        isThumb = false;

        switch (fingerType)
        {
            case FingerType.L_Thumb:
                touchButtonPool.Add(OVRInput.RawTouch.LThumbstick);
                touchButtonPool.Add(OVRInput.RawTouch.LThumbRest);
                touchButtonPool.Add(OVRInput.RawTouch.X);
                touchButtonPool.Add(OVRInput.RawTouch.Y);
                trigger = OVRInput.RawAxis1D.LHandTrigger;
                axis = new Vector3(-0.1f, 0.8f, -0.4f);
                isThumb = true;
                break;
            case FingerType.L_Index:
                trigger = OVRInput.RawAxis1D.LIndexTrigger;
                triggerStart = 0.5f;
                relatedTouchButton = OVRInput.RawTouch.LIndexTrigger;
                axis = new Vector3(0.1f, 1, 0);
                break;
            case FingerType.L_Middle:
                trigger = OVRInput.RawAxis1D.LHandTrigger;
                relatedTouchButton = OVRInput.RawTouch.LIndexTrigger;
                triggerStart = 0.95f;
                break;
            case FingerType.L_Ring:
                trigger = OVRInput.RawAxis1D.LHandTrigger;
                triggerStart = 0.1f;
                break;
            case FingerType.L_Pinky:
                trigger = OVRInput.RawAxis1D.LHandTrigger;
                axis = new Vector3(0, 1, -0.1f);
                break;

            case FingerType.R_Thumb:
                touchButtonPool.Add(OVRInput.RawTouch.RThumbstick);
                touchButtonPool.Add(OVRInput.RawTouch.RThumbRest);
                touchButtonPool.Add(OVRInput.RawTouch.A);
                touchButtonPool.Add(OVRInput.RawTouch.B);
                trigger = OVRInput.RawAxis1D.RHandTrigger;
                axis = new Vector3(0.1f, 0.5f, 0.4f);
                isThumb = true;
                break;
            case FingerType.R_Index:
                trigger = OVRInput.RawAxis1D.RIndexTrigger;
                triggerStart = 0.1f;
                relatedTouchButton = OVRInput.RawTouch.RIndexTrigger;
                axis = new Vector3(-0.1f, 1, 0);
                break;
            case FingerType.R_Middle:
                trigger = OVRInput.RawAxis1D.RHandTrigger;
                relatedTouchButton = OVRInput.RawTouch.RIndexTrigger;
                triggerStart = 0.95f;
                break;
            case FingerType.R_Ring:
                trigger = OVRInput.RawAxis1D.RHandTrigger;
                triggerStart = 0.1f;
                break;
            case FingerType.R_Pinky:
                trigger = OVRInput.RawAxis1D.RHandTrigger;
                axis = new Vector3(0, 1, 0.1f);
                break;
        }

        if (jointLevelPool.Count >= 3)
        {
            float[,] levels = {
                { 0.05f, 0.5f, 0.9f },  // �e�w
                { 0.9f, 1.0f, 1.2f },   // �l�����w
                { 1.0f, 0.8f, 1.6f },   // ���w
                { 1.0f, 0.7f, 1.6f },   // ��w
                { 1.0f, 0.7f, 1.6f }    // ���w
            };

            int fi = (int)fingerType;
            int levelType = (fi >= (int)FingerType.R_Thumb) ? fi - (int)FingerType.R_Thumb : fi - (int)FingerType.L_Thumb;
            jointLevelPool[0] = levels[levelType, 0];
            jointLevelPool[1] = levels[levelType, 1];
            jointLevelPool[2] = levels[levelType, 2];

        }
    }
}