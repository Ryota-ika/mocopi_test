/*
 * Copyright 2022 Sony Corporation
 */
using System.Collections.Generic;
using UnityEngine;

namespace Mocopi.Receiver
{
    /// <summary>
    /// A class for controlling avatars
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public sealed class MocopiAvatar : MocopiAvatarBase
    {
        #region --Fields--
        /// <summary>
        /// Table of mocopi sensor bone information
        /// </summary>
        public static readonly Dictionary<string, int> HUMAN_BONE_NAME_TO_MOCOPI_BONE_ID = new Dictionary<string, int>()
        {
            { "Hips",           0 },
            { "Spine",          3 },
            { "Chest",          5 },
            { "UpperChest",     6 },
            { "Neck",           8 },
            { "Head",           10 },
            { "LeftShoulder",   11 },
            { "LeftUpperArm",   12 },
            { "LeftLowerArm",   13 },
            { "LeftHand",       14 },
            { "RightShoulder",  15 },
            { "RightUpperArm",  16 },
            { "RightLowerArm",  17 },
            { "RightHand",      18 },
            { "LeftUpperLeg",   19 },
            { "LeftLowerLeg",   20 },
            { "LeftFoot",       21 },
            { "LeftToeBase",    22 },
            { "RightUpperLeg",  23 },
            { "RightLowerLeg",  24 },
            { "RightFoot",      25 },
            { "RightToeBase",   26 }
        };

        /// <summary>
        /// Table of mocopi sensor bone information
        /// </summary>
        public static readonly Dictionary<string, int> MOCOPI_BONE_NAME_TO_MOCOPI_BONE_ID = new Dictionary<string, int>()
        {
            { "root",       0 },
            { "torso_1",    1 },
            { "torso_2",    2 },
            { "torso_3",    3 },
            { "torso_4",    4 },
            { "torso_5",    5 },
            { "torso_6",    6 },
            { "torso_7",    7 },
            { "neck_1",     8 },
            { "neck_2",     9 },
            { "head",       10 },
            { "l_shoulder", 11 },
            { "l_up_arm",   12 },
            { "l_low_arm",  13 },
            { "l_hand",     14 },
            { "r_shoulder", 15 },
            { "r_up_arm",   16 },
            { "r_low_arm",  17 },
            { "r_hand",     18 },
            { "l_up_leg",   19 },
            { "l_low_leg",  20 },
            { "l_foot",     21 },
            { "l_toes",     22 },
            { "r_up_leg",   23 },
            { "r_low_leg",  24 },
            { "r_foot",     25 },
            { "r_toes",     26 }
        };

        /// <summary>
        /// Avatar list
        /// </summary>
        public static readonly List<MocopiAvatar> Avatars = new List<MocopiAvatar>();

        /// <summary>
        /// Remove AnimatorController on update or not
        /// </summary>
        private bool IsRemoveAnimatorControllerOnUpdate = true;

        /// <summary>
        /// Properties for adjusting the smoothness of motion movements
        /// </summary>
        [Range(0, 1f)] public float MotionSmoothness = 0;

        /// <summary>
        /// Bones list
        /// </summary>
        private readonly List<MocopiBone> bones = new List<MocopiBone>();

        /// <summary>
        /// Bones position
        /// </summary>
        private readonly Dictionary<MocopiBone, Vector3> bonePositions = new Dictionary<MocopiBone, Vector3>();

        /// <summary>
        /// bones rotation
        /// </summary>
        private readonly Dictionary<MocopiBone, Quaternion> boneRotations = new Dictionary<MocopiBone, Quaternion>();

        /// <summary>
        /// List of frame arrival times
        /// </summary>
        private readonly List<float> frameArrivalTimes = new List<float>();

        /// <summary>
        /// Frame counter
        /// </summary>
        private int fpsFrameCounter = 0;

        /// <summary>
        /// Whether skeleton initialization is reserved
        /// </summary>
        private bool isSkeletonInitializeReserved = false;

        /// <summary>
        /// Whether the skeleton has been initialized
        /// </summary>
        private bool isSkeletonInitialized = false;

        /// <summary>
        /// Was the skeleton updated
        /// </summary>
        private bool isSkeletonUpdated = false;

        /// <summary>
        /// Avatar definition
        /// </summary>
        private Avatar avatar;

        /// <summary>
        /// HumanPose before update
        /// </summary>
        private HumanPoseHandler humanPoseHandlerSrc;

        /// <summary>
        /// HumanPose after update
        /// </summary>
        private HumanPoseHandler humanPoseHandlerDst;

        /// <summary>
        /// Human pose
        /// </summary>
        private HumanPose pose = new HumanPose();

        /// <summary>
        /// Skeleton definition data
        /// </summary>
        private SkeletonDefinitionData skeletonDefinitionData;

        /// <summary>
        /// Skeleton update data
        /// </summary>
        private SkeletonData skeletonData;

        /// <summary>
        /// Avatar object
        /// </summary>
        private GameObject avatarRootObj;

        #endregion --Fields--

        #region --Properties--
        /// <summary>
        /// Animator
        /// </summary>
        public Animator Animator { get; private set; }

        /// <summary>
        /// Whether or not to stop updating the skeleton
        /// </summary>
        public bool IsLockSkeletonUpdate { get; set; }

        /// <summary>
        /// Number of frames arriving
        /// </summary>
        public float FrameArrivalRate { get; private set; }
        #endregion --Properties--

        #region --Methods--
        /// <summary>
        /// Initialize avatar bone information
        /// </summary>
        /// <param name="boneIds">mocopi Avatar bone id list</param>
        /// <param name="parentJointIds">List of IDs of parent bones for each bone</param>
        /// <param name="rotationsX">Rotation angle of each bone in initial posture</param>
        /// <param name="rotationsY">Rotation angle of each bone in initial posture</param>
        /// <param name="rotationsZ">Rotation angle of each bone in initial posture</param>
        /// <param name="rotationsW">Rotation angle of each bone in initial posture</param>
        /// <param name="positionsX">Position of each bone in initial pose</param>
        /// <param name="positionsY">Position of each bone in initial pose</param>
        /// <param name="positionsZ">Position of each bone in initial pose</param>
        /// <remarks><see cref="MocopiAvatarBase.InitializeSkeleton(int[], int[], float[], float[], float[], float[], float[], float[], float[])"/></remarks>
        public override void InitializeSkeleton(
            int[] boneIds, int[] parentBoneIds,
            float[] rotationsX, float[] rotationsY, float[] rotationsZ, float[] rotationsW,
            float[] positionsX, float[] positionsY, float[] positionsZ
        )
        {
            this.skeletonDefinitionData.BoneIds = boneIds;
            this.skeletonDefinitionData.ParentBoneIds = parentBoneIds;
            this.skeletonDefinitionData.RotationsX = rotationsX;
            this.skeletonDefinitionData.RotationsY = rotationsY;
            this.skeletonDefinitionData.RotationsZ = rotationsZ;
            this.skeletonDefinitionData.RotationsW = rotationsW;
            this.skeletonDefinitionData.PositionsX = positionsX;
            this.skeletonDefinitionData.PositionsY = positionsY;
            this.skeletonDefinitionData.PositionsZ = positionsZ;

            this.isSkeletonInitializeReserved = true;
        }

        /// <summary>
        /// Update avatar bone information
        /// </summary>
        /// <param name="boneIds">mocopi Avatar bone id list</param>
        /// <param name="rotationsX">Rotation angle of each bone</param>
        /// <param name="rotationsY">Rotation angle of each bone</param>
        /// <param name="rotationsZ">Rotation angle of each bone</param>
        /// <param name="rotationsW">Rotation angle of each bone</param>
        /// <param name="positionsX">Position of each bone</param>
        /// <param name="positionsY">Position of each bone</param>
        /// <param name="positionsZ">Position of each bone</param>
        /// <remarks><see cref="MocopiAvatarBase.UpdateSkeleton(int[], float[], float[], float[], float[], float[], float[], float[])"/></remarks>
        public override void UpdateSkeleton(
            int[] boneIds,
            float[] rotationsX, float[] rotationsY, float[] rotationsZ, float[] rotationsW,
            float[] positionsX, float[] positionsY, float[] positionsZ
        )
        {
            this.skeletonData.BoneIds = boneIds;
            this.skeletonData.RotationsX = rotationsX;
            this.skeletonData.RotationsY = rotationsY;
            this.skeletonData.RotationsZ = rotationsZ;
            this.skeletonData.RotationsW = rotationsW;
            this.skeletonData.PositionsX = positionsX;
            this.skeletonData.PositionsY = positionsY;
            this.skeletonData.PositionsZ = positionsZ;

            this.isSkeletonUpdated = true;
        }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (!Avatars.Contains(this))
            {
                Avatars.Add(this);
            }

            this.Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (this.isSkeletonInitializeReserved)
            {
                this.InvokeInitializeSkeleton();
                this.isSkeletonInitializeReserved = false;
            }

            if (this.isSkeletonInitialized)
            {
                // if the skeleton needs to be updated
                if (this.isSkeletonUpdated)
                {
                    this.fpsFrameCounter++;
                    this.InvokeUpdateSkeleton();
                    this.isSkeletonUpdated = false;

                    // update avatar pose
                    this.UpdateAvatarPose();
                }

            }
        }

        /// <summary>
        /// LateUpdate
        /// </summary>
        private void LateUpdate()
        {
            if (this.isSkeletonInitialized)
            {
                this.UpdateFrameArrivalRate();
            }
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            if (Avatars.Contains(this))
            {
                Avatars.Remove(this);
            }
        }

        /// <summary>
        /// Update frame arrivals
        /// </summary>
        private void UpdateFrameArrivalRate()
        {
            int removeCount = 0;
            foreach (float time in this.frameArrivalTimes)
            {
                if (time + 1f < Time.time)
                {
                    removeCount++;
                }
                else
                {
                    break;
                }
            }

            this.frameArrivalTimes.RemoveRange(0, removeCount);

            for (int i = 0; i < this.fpsFrameCounter; i++)
            {
                this.frameArrivalTimes.Add(Time.time);
            }

            this.FrameArrivalRate = this.frameArrivalTimes.Count;

            this.fpsFrameCounter = 0;
        }

        /// <summary>
        /// Initialize skeleton
        /// </summary>
        private void InvokeInitializeSkeleton()
        {
            this.isSkeletonInitialized = false;

            // Regenerate when new skeleton definition data comes
            Destroy(this.avatarRootObj);
            this.avatarRootObj = new GameObject("SkeletonRoot");
            this.avatarRootObj.transform.SetParent(transform);
            this.avatarRootObj.transform.localPosition = Vector3.zero;
            this.avatarRootObj.transform.localRotation = Quaternion.identity;
            this.avatarRootObj.transform.localScale = Vector3.one;

            foreach (MocopiBone bone in this.bones)
            {
                Destroy(bone.Transform.gameObject);
            }

            this.bones.Clear();

            this.bonePositions.Clear();
            this.boneRotations.Clear();

            for (int i = 0; i < this.skeletonDefinitionData.BoneIds.Length; i++)
            {
                string boneName = this.GetMocopiBoneNameByBoneId(this.skeletonDefinitionData.BoneIds[i]);

                if (string.IsNullOrEmpty(boneName))
                {
                    continue;
                }

                GameObject obj = new GameObject(boneName);

                // align with Unity's coordinate space
                Vector3 position = this.ConvertPluginDataToVector3(
                    this.skeletonDefinitionData.PositionsX[i],
                    this.skeletonDefinitionData.PositionsY[i],
                    this.skeletonDefinitionData.PositionsZ[i]
                );
                Quaternion rotation = this.ConvertPluginDataToQuaternion(
                    this.skeletonDefinitionData.RotationsX[i],
                    this.skeletonDefinitionData.RotationsY[i],
                    this.skeletonDefinitionData.RotationsZ[i],
                    this.skeletonDefinitionData.RotationsW[i]
                );

                obj.transform.localPosition = position;
                obj.transform.localRotation = rotation;

                MocopiBone bone = new MocopiBone
                {
                    BoneName = boneName,
                    Id = this.skeletonDefinitionData.BoneIds[i],
                    ParentId = this.skeletonDefinitionData.ParentBoneIds[i],
                    Transform = obj.transform
                };

                this.bones.Add(bone);

                this.bonePositions.Add(bone, position);
                this.boneRotations.Add(bone, rotation);
            }

            foreach (MocopiBone bone in this.bones)
            {
                MocopiBone parent = this.bones.Find(_ => _.Id == bone.ParentId);

                Vector3 position = bone.Transform.localPosition;
                Quaternion rotation = bone.Transform.localRotation;

                if (parent != null)
                {
                    bone.Transform.SetParent(parent.Transform);
                }

                if (bone.Transform.parent == null)
                {
                    bone.Transform.SetParent(this.avatarRootObj.transform);
                }

                bone.Transform.localPosition = position;
                bone.Transform.localRotation = rotation;
            }

            HumanBone[] humanBones = new HumanBone[this.bones.Count];
            SkeletonBone[] skeletonBones = new SkeletonBone[this.bones.Count + 1];

            int humanBoneIndex = 0;
            foreach (string name in HumanTrait.BoneName)
            {
                int id = this.GetBoneIdByHumanBoneName(name);

                if (id >= 0)
                {
                    HumanBone humanBone = new HumanBone
                    {
                        humanName = name,
                        boneName = this.GetMocopiBoneNameByBoneId(id)
                    };
                    humanBone.limit.useDefaultValues = true;

                    humanBones[humanBoneIndex++] = humanBone;
                }
            }

            SkeletonBone baseSkeletonBone = new SkeletonBone
            {
                name = this.avatarRootObj.name,
                position = Vector3.zero,
                rotation = Quaternion.identity,
                scale = Vector3.one
            };

            skeletonBones[0] = baseSkeletonBone;

            for (int i = 0; i < this.bones.Count; i++)
            {
                MocopiBone bone = this.bones[i];

                SkeletonBone skeletonBone = new SkeletonBone
                {
                    name = bone.BoneName,
                    position = bone.Transform.localPosition,
                    rotation = bone.Transform.localRotation,
                    scale = Vector3.one
                };

                skeletonBones[i + 1] = skeletonBone;
            }

            HumanDescription humanDescription = new HumanDescription
            {
                human = humanBones,
                skeleton = skeletonBones,
                upperArmTwist = 0.5f,
                lowerArmTwist = 0.5f,
                upperLegTwist = 0.5f,
                lowerLegTwist = 0.5f,
                armStretch = 0.05f,
                legStretch = 0.05f,
                feetSpacing = 0.0f,
                hasTranslationDoF = false
            };

            Destroy(this.avatar);
            this.avatar = AvatarBuilder.BuildHumanAvatar(this.avatarRootObj, humanDescription);

            if (this.humanPoseHandlerSrc != null)
            {
                this.humanPoseHandlerSrc.Dispose();
            }

            if (this.humanPoseHandlerDst != null)
            {
                this.humanPoseHandlerDst.Dispose();
            }

            this.humanPoseHandlerSrc = new HumanPoseHandler(this.avatar, this.avatarRootObj.transform);
            this.humanPoseHandlerDst = new HumanPoseHandler(this.Animator.avatar, transform);

            this.isSkeletonInitialized = true;
        }

        /// <summary>
        /// Update skeleton
        /// </summary>
        private void InvokeUpdateSkeleton()
        {
            if (this.IsLockSkeletonUpdate)
            {
                return;
            }

            // If there is an AnimatorController, remove it because it interferes with updating the skeleton
            if (this.IsRemoveAnimatorControllerOnUpdate && this.Animator.runtimeAnimatorController != null)
            {
                this.Animator.runtimeAnimatorController = null;
            }

            for (int i = 0; i < this.skeletonData.BoneIds.Length; i++)
            {
                MocopiBone bone = this.bones.Find(_ => _.Id == this.skeletonData.BoneIds[i]);
                if (bone == null)
                {
                    continue;
                }

                // align with Unity's coordinate space
                Vector3 position = this.ConvertPluginDataToVector3(
                    this.skeletonData.PositionsX[i],
                    this.skeletonData.PositionsY[i],
                    this.skeletonData.PositionsZ[i]
                );
                Quaternion rotation = this.ConvertPluginDataToQuaternion(
                    this.skeletonData.RotationsX[i],
                    this.skeletonData.RotationsY[i],
                    this.skeletonData.RotationsZ[i],
                    this.skeletonData.RotationsW[i]
                );

                if (bone.ParentId < 0)
                {
                    this.bonePositions[bone] = position;
                }

                this.boneRotations[bone] = rotation;
            }
        }

        /// <summary>
        /// Updated avatar pose
        /// </summary>
        private void UpdateAvatarPose()
        {
            if (this.MotionSmoothness > 0)
            {
                float fps = Application.targetFrameRate > 0 ? Application.targetFrameRate : 60f;
                float lerpValue = Mathf.Lerp(1f, 0.3f, Mathf.Clamp01(Time.deltaTime * fps * this.MotionSmoothness));

                foreach (MocopiBone bone in this.bones)
                {
                    bone.Transform.localPosition = Vector3.Lerp(bone.Transform.localPosition, this.bonePositions[bone], lerpValue);
                    bone.Transform.localRotation = Quaternion.Lerp(bone.Transform.localRotation, this.boneRotations[bone], lerpValue);
                }
            }
            else
            {
                foreach (MocopiBone bone in this.bones)
                {
                    bone.Transform.localPosition = this.bonePositions[bone];
                    bone.Transform.localRotation = this.boneRotations[bone];
                }
            }

            this.humanPoseHandlerSrc.GetHumanPose(ref this.pose);
            this.humanPoseHandlerDst.SetHumanPose(ref this.pose);
        }

        /// <summary>
        /// Get name of bone
        /// </summary>
        /// <param name="boneId">mocopi Avatar bone ID</param>
        /// <returns>mocopi Sensor bone name</returns>
        private string GetMocopiBoneNameByBoneId(int boneId)
        {
            foreach (KeyValuePair<string, int> pair in MOCOPI_BONE_NAME_TO_MOCOPI_BONE_ID)
            {
                if (pair.Value == boneId)
                {
                    return pair.Key;
                }
            }

            return "";
        }

        /// <summary>
        /// Get id of bone
        /// </summary>
        /// <param name="humanBoneName">mocopi Sensor bone name</param>
        /// <returns>mocopi Avatar bone ID</returns>
        private int GetBoneIdByHumanBoneName(string humanBoneName)
        {
            if (HUMAN_BONE_NAME_TO_MOCOPI_BONE_ID.ContainsKey(humanBoneName))
            {
                return HUMAN_BONE_NAME_TO_MOCOPI_BONE_ID[humanBoneName];
            }

            return -1;
        }

        /// <summary>
        /// Convert the obtained location information to Unity coordinates
        /// </summary>
        /// <param name="x">bone position</param>
        /// <param name="y">bone position</param>
        /// <param name="z">bone position</param>
        /// <returns>Converted Unity coordinates</returns>
        private Vector3 ConvertPluginDataToVector3(double x, double y, double z)
        {
            return new Vector3(
                -(float)x,
                (float)y,
                (float)z
            );
        }

        /// <summary>
        /// Convert the obtained rotation angle to Unity coordinates
        /// </summary>
        /// <param name="x">bone rotation</param>
        /// <param name="y">bone rotation</param>
        /// <param name="z">bone rotation</param>
        /// <param name="w">bone rotation</param>
        /// <returns>Converted Unity Coordinates Quaternion</returns>
        private Quaternion ConvertPluginDataToQuaternion(double x, double y, double z, double w)
        {
            return new Quaternion(
                -(float)x,
                (float)y,
                (float)z,
                -(float)w
            );
        }
        #endregion --Methods--

        #region --Structs--
        /// <summary>
        /// skeleton definition data structure
        /// </summary>
        private struct SkeletonDefinitionData
        {
            /// <summary>
            /// mocopi Avatar bone id list
            /// </summary>
            public int[] BoneIds;

            /// <summary>
            /// List of IDs of parent bones for each bone
            /// </summary>
            public int[] ParentBoneIds;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsX;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsY;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsZ;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsW;

            /// <summary>
            /// Position of each bone in initial pose
            /// </summary>
            public float[] PositionsX;

            /// <summary>
            /// Position of each bone in initial pose
            /// </summary>
            public float[] PositionsY;

            /// <summary>
            /// Position of each bone in initial pose
            /// </summary>
            public float[] PositionsZ;
        }

        /// <summary>
        /// Skeleton update data structure
        /// </summary>
        private struct SkeletonData
        {
            /// <summary>
            /// mocopi Avatar bone id list
            /// </summary>
            public int[] BoneIds;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsX;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsY;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsZ;

            /// <summary>
            /// Rotation angle of each bone in initial posture
            /// </summary>
            public float[] RotationsW;

            /// <summary>
            /// Position of each bone in initial pose
            /// </summary>
            public float[] PositionsX;

            /// <summary>
            /// Position of each bone in initial pose
            /// </summary>
            public float[] PositionsY;

            /// <summary>
            /// Position of each bone in initial pose
            /// </summary>
            public float[] PositionsZ;
        }
        #endregion --Structs--

        #region --Classes--
        /// <summary>
        /// Class that manages mocopi bone information
        /// </summary>
        private sealed class MocopiBone
        {
            /// <summary>
            /// Bone name
            /// </summary>
            public string BoneName;

            /// <summary>
            /// Bone id
            /// </summary>
            public int Id;

            /// <summary>
            /// Parent bone id
            /// </summary>
            public int ParentId;

            /// <summary>
            /// Transform
            /// </summary>
            public Transform Transform;
        }
        #endregion --Classes--
    }
}
