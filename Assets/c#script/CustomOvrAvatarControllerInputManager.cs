//6月21日

using Oculus.Avatar2;
using Node = UnityEngine.XR.XRNode;
using Button = OVRInput.Button;
using Touch = OVRInput.Touch;

namespace xrdnk
{
    /// <summary>
    /// OvrAvatarInputManager の実装クラス
    /// <para>コントローラによるトラッキングを前提としている</para>
    /// <remarks>Meta Avatar のボディトラッキングに必要</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarControllerInputManager : OvrAvatarInputManager
    {
        void Start()
        {
            if (BodyTracking != null)
            {
                // IOvrAvatarInputTrackingDelegate カスタム実装の登録
                BodyTracking.InputTrackingDelegate = new CustomOvrAvatarControllerInputTrackingDelegate();
                // IOvrAvatarInputControlDelegate カスタム実装の登録
                BodyTracking.InputControlDelegate = new CustomOvrAvatarControllerInputControlDelegate();
            }
        }
    }

    /// <summary>
    /// OvrAvatarInputTrackingDelegate の実装クラス
    /// <para>ハンドトラッキングではなくコントローラによるトラッキングを前提としている</para>
    /// <remarks>コントローラの位置を検知して，Meta Avatar の腕の動きを制御</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarControllerInputTrackingDelegate : OvrAvatarInputTrackingDelegate
    {
        /// <summary>
        /// GetRawInputTrackingState のオーバーライド
        /// <remarks>コントローラの位置情報を検知して状態を毎フレーム更新
        /// </remarks>
        /// </summary>
        /// <param name="inputTrackingState">OvrAvatarInputTrackingState</param>
        /// <returns>true:更新させる false:更新させない</returns>
        public override bool GetRawInputTrackingState(out OvrAvatarInputTrackingState inputTrackingState)
        {
            var leftControllerActive = false;
            var rightControllerActive = false;

            // ハンドトラッキングモードではない場合
            if (OVRInput.GetActiveController() != OVRInput.Controller.Hands)
            {
                leftControllerActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.LTouch);
                rightControllerActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.RTouch);
            }

            // HMD が存在する場合
            if (OVRNodeStateProperties.IsHmdPresent())
            {
                // OvrInputTrackingState の初期設定
                inputTrackingState = new OvrAvatarInputTrackingState
                {
                    headsetActive = true,
                    leftControllerActive = leftControllerActive,
                    rightControllerActive = rightControllerActive,
                    leftControllerVisible = true,
                    rightControllerVisible = true
                };

                // 頭部の Position 設定
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

                // 頭部の Orientation 設定
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

                // 左コントローラの Position 設定
                inputTrackingState.leftController.position =
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                // 左コントローラの Orientation 設定
                inputTrackingState.leftController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

                // 右コントローラの Position 設定
                inputTrackingState.rightController.position =
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                // 右コントローラの Orientation 設定
                inputTrackingState.rightController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

                // 状態を毎フレーム更新させる
                return true;
            }
            // HMD が存在しない場合
            else
            {
                inputTrackingState = default;
                // 状態を毎フレーム更新させない
                return false;
            }
        }
    }

    /// <summary>
    /// OvrAvatarInputControlDelegate の実装クラス
    /// <para>ハンドトラッキングではなくコントローラによるトラッキングを前提としている</para>
    /// <remarks>コントローラで入力したボタンを検知して，Meta Avatar の指の動きを制御</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarControllerInputControlDelegate : OvrAvatarInputControlDelegate
    {
        /// <summary>
        /// GetInputControlState のオーバーライド
        /// <remarks>ボタン・タッチ入力を検知して状態を毎フレーム更新</remarks>
        /// </summary>
        /// <param name="inputControlState">OvrAvatarInputControlState</param>
        /// <returns>true:更新させる false:更新させない</returns>
        public override bool GetInputControlState(out OvrAvatarInputControlState inputControlState)
        {
            // OvrAvatarInputControlState の初期設定
            inputControlState = new OvrAvatarInputControlState
            {
                // GetControllerType の実装を各自確認
                // UNITY_ANDROID && !UNITY_EDITOR であれば Touch，それ以外であれば Rift のコントローラタイプになる
                type = GetControllerType()
            };

            // 左コントローラの状態を検知
            UpdateControllerInput(ref inputControlState.leftControllerState, OVRInput.Controller.LTouch);
            // 右コントローラの状態を検知
            UpdateControllerInput(ref inputControlState.rightControllerState, OVRInput.Controller.RTouch);

            return true;
        }

        /// <summary>
        /// OVRInput の入力を検知して，コントローラのボタン・タッチ状態を更新
        /// </summary>
        /// <param name="controllerState">OvrAvatarControllerState</param>
        /// <param name="controller">OVRInput.Controller</param>
        void UpdateControllerInput(ref OvrAvatarControllerState controllerState, OVRInput.Controller controller)
        {
            // コントローラのボタン状態
            controllerState.buttonMask = 0;
            // コントローラのタッチ状態
            controllerState.touchMask = 0;

            // ボタン入力検知
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

            // タッチ入力検知
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

            // トリガー入力検知
            controllerState.indexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
            if (OVRInput.Get(Touch.PrimaryIndexTrigger, controller))
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.Index;
            }
            else if (controllerState.indexTrigger <= 0f)
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.Pointing;
            }

            // グリップ入力検知
            controllerState.handTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

            // 以下の条件の時，親指を上げる
            if ((controllerState.touchMask & (CAPI.ovrAvatar2Touch.One | CAPI.ovrAvatar2Touch.Two |
                                              CAPI.ovrAvatar2Touch.Joystick | CAPI.ovrAvatar2Touch.ThumbRest)) == 0)
            {
                controllerState.touchMask |= CAPI.ovrAvatar2Touch.ThumbUp;
            }
        }
    }
}