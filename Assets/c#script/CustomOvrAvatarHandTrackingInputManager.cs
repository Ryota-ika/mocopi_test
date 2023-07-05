//6月21日
//https://xrdnk.hateblo.jp/entry/meta_avatars_sdk_for_unity_4
using Oculus.Avatar2;
using Node = UnityEngine.XR.XRNode;

namespace xrdnk
{
    /// <summary>
    /// OvrAvatarInputManager の実装クラス
    /// <para>ハンドトラッキングを前提としている</para>
    /// <remarks>Meta Avatar のボディトラッキングに必要</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarHandTrackingInputManager : OvrAvatarInputManager
    {
        void Start()
        {
            if (BodyTracking != null)
            {
                // IOvrAvatarInputTrackingDelegate カスタム実装の登録
                BodyTracking.InputTrackingDelegate = new CustomOvrAvatarHandInputTrackingDelegate();
                // IOvrAvatarHandTrackingDelegate カスタム実装の登録
                BodyTracking.HandTrackingDelegate = new CustomOvrAvatarHandTrackingDelegate();
            }
        }
    }

    /// <summary>
    /// OvrAvatarInputTrackingDelegate の実装クラス
    /// <para>ハンドトラッキングを前提としている</para>
    /// <remarks>手の位置を検知して，Meta Avatar の腕の動きを制御</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarHandInputTrackingDelegate : OvrAvatarInputTrackingDelegate
    {
        /// <summary>
        /// GetRawInputTrackingState のオーバーライド
        /// <remarks>手の位置情報を検知して状態を毎フレーム更新
        /// </remarks>
        /// </summary>
        /// <param name="inputTrackingState">OvrAvatarInputTrackingState</param>
        /// <returns>true:更新させる false:更新させない</returns>
        public override bool GetRawInputTrackingState(out OvrAvatarInputTrackingState inputTrackingState)
        {
            var leftHandActive = false;
            var rightHandActive = false;

            // ハンドトラッキングモードの場合
            if (OVRInput.GetActiveController() == OVRInput.Controller.Hands)
            {
                leftHandActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.LHand);
                rightHandActive = OVRInput.GetControllerOrientationTracked(OVRInput.Controller.RHand);
            }

            // HMD が存在する場合
            if (OVRNodeStateProperties.IsHmdPresent())
            {
                // OvrInputTrackingState の初期設定
                inputTrackingState = new OvrAvatarInputTrackingState
                {
                    headsetActive = true,
                    leftControllerActive = leftHandActive,
                    rightControllerActive = rightHandActive,
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
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
                // 左コントローラの Orientation 設定
                inputTrackingState.leftController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);

                // 右コントローラの Position 設定
                inputTrackingState.rightController.position =
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
                // 右コントローラの Orientation 設定
                inputTrackingState.rightController.orientation =
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

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
    /// IOvrAvatarHandTrackingDelegate の実装クラス
    /// <remarks>アバターの指・手の動きにアニメーションを追加するために必要</remarks>
    /// </summary>
    public sealed class CustomOvrAvatarHandTrackingDelegate : IOvrAvatarHandTrackingDelegate
    {
        /// <summary>
        /// GetHandData の実装
        /// <para>trueを返すことでハンドトラッキングの状態を毎フレーム更新できるようになる(デフォルトではfalse)</para>
        /// </summary>
        /// <param name="handData">OvrAvatarTrackingHandsState</param>
        /// <returns>true: 更新させる false: 更新させない</returns>
        public bool GetHandData(OvrAvatarTrackingHandsState handData) => true;
    }
}