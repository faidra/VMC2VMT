using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMC2VMT
{
    public sealed class Transforms : MonoBehaviour
    {
        [SerializeField] Transform root;
        public IReadOnlyDictionary<HumanBodyBones, Transform> BoneTransforms;

        void Start()
        {
            InitializeBoneTransforms();
        }

        public void SetRootPose(PositionAndRotation pose)
        {
            // MrScaleがあるのでlocalである必要がある
            root.localPosition = pose.Position;
            root.localRotation = pose.Rotation;
        }

        public void SetBonePose(HumanBodyBones humanBodyBones, PositionAndRotation pose)
        {
            InitializeBoneTransforms();
            var transform = BoneTransforms[humanBodyBones];
            transform.localPosition = pose.Position;
            transform.localRotation = pose.Rotation;
        }

        void InitializeBoneTransforms()
        {
            if (BoneTransforms != null) return;
            var dictionary = new Dictionary<HumanBodyBones, Transform>(Enum.GetNames(typeof(HumanBodyBones)).Length);
            void AddRecursive(Transform transform)
            {
                foreach (Transform child in transform)
                {
                    if (Enum.TryParse<HumanBodyBones>(child.name, out var value))
                    {
                        dictionary.Add(value, child);
                        AddRecursive(child);
                    }
                }
            }
            
            AddRecursive(root);
            BoneTransforms = dictionary;
        }
    }
}
