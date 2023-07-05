//6��21��
//https://xrdnk.hateblo.jp/entry/meta_avatars_sdk_for_unity_4
using Oculus.Avatar2;
using Node = UnityEngine.XR.XRNode;

namespace xrdnk
{
    /// <summary>
    /// OvrAvatarInputManager �̎����N���X
    /// <para>�n���h�g���b�L���O��O��Ƃ��Ă���</para>
    /// <remarks>Meta Avatar �̃{�f�B�g���b�L���O�ɕK�v</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarHandTrackingInputManager : OvrAvatarInputManager
    {
        void Start()
        {
            if (BodyTracking != null)
            {
                // IOvrAvatarInputTrackingDelegate �J�X�^�������̓o�^
                BodyTracking.InputTrackingDelegate = new CustomOvrAvatarHandInputTrackingDelegate();
                // IOvrAvatarHandTrackingDelegate �J�X�^�������̓o�^
                BodyTracking.HandTrackingDelegate = new CustomOvrAvatarHandTrackingDelegate();
            }
        }
    }

    /// <summary>
    /// OvrAvatarInputTrackingDelegate �̎����N���X
    /// <para>�n���h�g���b�L���O��O��Ƃ��Ă���</para>
    /// <remarks>��̈ʒu�����m���āCMeta Avatar �̘r�̓����𐧌�</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarHandInputTrackingDelegate : OvrAvatarInputTrackingDelegate
    {
        /// <summary>
        /// GetRawInputTrackingState �̃I�[�o�[���C�h
        /// <remarks>��̈ʒu�������m���ď�Ԃ𖈃t���[���X�V
        /// </remarks>
        /// </summary>
        /// <param name="inputTrackingState">OvrAvatarInputTrackingState</param>
        /// <returns>true:�X�V������ false:�X�V�����Ȃ�</returns>
        public override bool GetRawInputTrackingState(out OvrAvatarInputTrackingState inputTrackingState)
        {
            var leftHandActive = false;
            var rightHandActive = false;

            // �n���h�g���b�L���O���[�h�̏ꍇ
            if (OVRInput.GetActiveController() == OVRInput.Controller.Hands)
            {
                leftHandActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.LHand);
                rightHandActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.RHand);
            }

            // HMD �����݂���ꍇ
            if (OVRNodeStateProperties.IsHmdPresent())
            {
                // OvrInputTrackingState �̏����ݒ�
                inputTrackingState = new OvrAvatarInputTrackingState
                {
                    headsetActive = true,
                    leftControllerActive = leftHandActive,
                    rightControllerActive = rightHandActive,
                    leftControllerVisible = true,
                    rightControllerVisible = true
                };

                // ������ Position �ݒ�
                if (
                    OVRNodeStateProperties.GetNodeStatePropertyVector3
                        (
                            Node.CenterEye,
                            NodeStatePropertyType.Position,
                            OVRPlugin.Node.EyeCenter,
                            OVRPlugin.Step.Render,
                            out var headPos
                        )
                    )
                {
                    inputTrackingState.headset.position = headPos;
                }

                // ������ Orientation �ݒ�
                if (
                    OVRNodeStateProperties.GetNodeStatePropertyQuaternion
                        (
                            Node.CenterEye,
                            NodeStatePropertyType.Orientation,
                            OVRPlugin.Node.EyeCenter,
                            OVRPlugin.Step.Render,
                            out var headRot
                        )
                    )
                {
                    inputTrackingState.headset.orientation = headRot;
                }

                // ���R���g���[���� Position �ݒ�
                inputTrackingState.leftController.position =
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
                // ���R���g���[���� Orientation �ݒ�
                inputTrackingState.leftController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);

                // �E�R���g���[���� Position �ݒ�
                inputTrackingState.rightController.position =
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
                // �E�R���g���[���� Orientation �ݒ�
                inputTrackingState.rightController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

                // ��Ԃ𖈃t���[���X�V������
                return true;
            }
            // HMD �����݂��Ȃ��ꍇ
            else
            {
                inputTrackingState = default;
                // ��Ԃ𖈃t���[���X�V�����Ȃ�
                return false;
            }
        }
    }

    /// <summary>
    /// IOvrAvatarHandTrackingDelegate �̎����N���X
    /// <remarks>�A�o�^�[�̎w�E��̓����ɃA�j���[�V������ǉ����邽�߂ɕK�v</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarHandTrackingDelegate : IOvrAvatarHandTrackingDelegate
    {
        /// <summary>
        /// GetHandData �̎���
        /// <para>true��Ԃ����ƂŃn���h�g���b�L���O�̏�Ԃ𖈃t���[���X�V�ł���悤�ɂȂ�(�f�t�H���g�ł�false)</para>
        /// </summary>
        /// <param name="handData">OvrAvatarTrackingHandsState</param>
        /// <returns>true: �X�V������ false: �X�V�����Ȃ�</returns>
        public bool GetHandData(OvrAvatarTrackingHandsState handData) => true;
    }
}