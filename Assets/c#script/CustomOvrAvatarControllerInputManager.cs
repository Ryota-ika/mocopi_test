//6��21��

using Oculus.Avatar2;
using Node = UnityEngine.XR.XRNode;
using Button = OVRInput.Button;
using Touch = OVRInput.Touch;

namespace xrdnk
{
    /// <summary>
    /// OvrAvatarInputManager �̎����N���X
    /// <para>�R���g���[���ɂ��g���b�L���O��O��Ƃ��Ă���</para>
    /// <remarks>Meta Avatar �̃{�f�B�g���b�L���O�ɕK�v</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarControllerInputManager : OvrAvatarInputManager
    {
        void Start()
        {
            if (BodyTracking != null)
            {
                // IOvrAvatarInputTrackingDelegate �J�X�^�������̓o�^
                BodyTracking.InputTrackingDelegate = new CustomOvrAvatarControllerInputTrackingDelegate();
                // IOvrAvatarInputControlDelegate �J�X�^�������̓o�^
                BodyTracking.InputControlDelegate = new CustomOvrAvatarControllerInputControlDelegate();
            }
        }
    }

    /// <summary>
    /// OvrAvatarInputTrackingDelegate �̎����N���X
    /// <para>�n���h�g���b�L���O�ł͂Ȃ��R���g���[���ɂ��g���b�L���O��O��Ƃ��Ă���</para>
    /// <remarks>�R���g���[���̈ʒu�����m���āCMeta Avatar �̘r�̓����𐧌�</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarControllerInputTrackingDelegate : OvrAvatarInputTrackingDelegate
    {
        /// <summary>
        /// GetRawInputTrackingState �̃I�[�o�[���C�h
        /// <remarks>�R���g���[���̈ʒu�������m���ď�Ԃ𖈃t���[���X�V
        /// </remarks>
        /// </summary>
        /// <param name="inputTrackingState">OvrAvatarInputTrackingState</param>
        /// <returns>true:�X�V������ false:�X�V�����Ȃ�</returns>
        public override bool GetRawInputTrackingState(out OvrAvatarInputTrackingState inputTrackingState)
        {
            var leftControllerActive = false;
            var rightControllerActive = false;

            // �n���h�g���b�L���O���[�h�ł͂Ȃ��ꍇ
            if (OVRInput.GetActiveController() != OVRInput.Controller.Hands)
            {
                leftControllerActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.LTouch);
                rightControllerActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.RTouch);
            }

            // HMD �����݂���ꍇ
            if (OVRNodeStateProperties.IsHmdPresent())
            {
                // OvrInputTrackingState �̏����ݒ�
                inputTrackingState = new OvrAvatarInputTrackingState
                {
                    headsetActive = true,
                    leftControllerActive = leftControllerActive,
                    rightControllerActive = rightControllerActive,
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
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                // ���R���g���[���� Orientation �ݒ�
                inputTrackingState.leftController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

                // �E�R���g���[���� Position �ݒ�
                inputTrackingState.rightController.position =
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                // �E�R���g���[���� Orientation �ݒ�
                inputTrackingState.rightController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

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
    /// OvrAvatarInputControlDelegate �̎����N���X
    /// <para>�n���h�g���b�L���O�ł͂Ȃ��R���g���[���ɂ��g���b�L���O��O��Ƃ��Ă���</para>
    /// <remarks>�R���g���[���œ��͂����{�^�������m���āCMeta Avatar �̎w�̓����𐧌�</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarControllerInputControlDelegate : OvrAvatarInputControlDelegate
    {
        /// <summary>
        /// GetInputControlState �̃I�[�o�[���C�h
        /// <remarks>�{�^���E�^�b�`���͂����m���ď�Ԃ𖈃t���[���X�V</remarks>
        /// </summary>
        /// <param name="inputControlState">OvrAvatarInputControlState</param>
        /// <returns>true:�X�V������ false:�X�V�����Ȃ�</returns>
        public override bool GetInputControlState(out OvrAvatarInputControlState inputControlState)
        {
            // OvrAvatarInputControlState �̏����ݒ�
            inputControlState = new OvrAvatarInputControlState
            {
                // GetControllerType �̎������e���m�F
                // UNITY_ANDROID && !UNITY_EDITOR �ł���� Touch�C����ȊO�ł���� Rift �̃R���g���[���^�C�v�ɂȂ�
                type = GetControllerType()
            };

            // ���R���g���[���̏�Ԃ����m
            UpdateControllerInput(ref inputControlState.leftControllerState, OVRInput.Controller.LTouch);
            // �E�R���g���[���̏�Ԃ����m
            UpdateControllerInput(ref inputControlState.rightControllerState, OVRInput.Controller.RTouch);

            return true;
        }

        /// <summary>
        /// OVRInput �̓��͂����m���āC�R���g���[���̃{�^���E�^�b�`��Ԃ��X�V
        /// </summary>
        /// <param name="controllerState">OvrAvatarControllerState</param>
        /// <param name="controller">OVRInput.Controller</param>
        void UpdateControllerInput(ref OvrAvatarControllerState controllerState, OVRInput.Controller controller)
        {
            // �R���g���[���̃{�^�����
            controllerState.buttonMask = 0;
            // �R���g���[���̃^�b�`���
            controllerState.touchMask = 0;

            // �{�^�����͌��m
            if (OVRInput.Get(Button.One, controller))
            {
                controllerState.buttonMask |= CAPI.ovrAvatar2Button.One;
            }
            if (OVRInput.Get(Button.Two, controller))
            {
                controllerState.buttonMask |= CAPI.ovrAvatar2Button.Two;
            }
            if (OVRInput.Get(Button.Three, controller))
            {
                controllerState.buttonMask |= CAPI.ovrAvatar2Button.Three;
            }
            if (OVRInput.Get(Button.PrimaryThumbstick, controller))
            {
                controllerState.buttonMask |= CAPI.ovrAvatar2Button.Joystick;
            }

            // �^�b�`���͌��m
            if (OVRInput.Get(Touch.One, controller))
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.One;
            }
            if (OVRInput.Get(Touch.Two, controller))
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.Two;
            }
            if (OVRInput.Get(Touch.PrimaryThumbstick, controller))
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.Joystick;
            }
            if (OVRInput.Get(Touch.PrimaryThumbRest, controller))
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.ThumbRest;
            }

            // �g���K�[���͌��m
            controllerState.indexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
            if (OVRInput.Get(Touch.PrimaryIndexTrigger, controller))
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.Index;
            }
            else if (controllerState.indexTrigger <= 0f)
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.Pointing;
            }

            // �O���b�v���͌��m
            controllerState.handTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

            // �ȉ��̏����̎��C�e�w���グ��
            if ((controllerState.touchMask & (CAPI.ovrAvatar2Touch.One | CAPI.ovrAvatar2Touch.Two |
                                              CAPI.ovrAvatar2Touch.Joystick | CAPI.ovrAvatar2Touch.ThumbRest)) == 0)
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.ThumbUp;
            }
        }
    }
}