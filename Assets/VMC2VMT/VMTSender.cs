using System;
using UniRx;
using UnityEngine;
using uOSC;

namespace VMC2VMT
{
    public sealed class VMTSender : MonoBehaviour
    {
        [SerializeField] uOscClient uOscClient;
        [SerializeField] Settings settings;
        [SerializeField] Transforms transforms;

        bool isActive;

        // OSC自体はActiveに関わらず常に回す
        void Start()
        {
            uOscClient.StartClient();
            Disposable.Create(() =>
            {
                if (uOscClient != null) uOscClient.StopClient();
            }).AddTo(this);
        }

        public void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }

        void Update()
        {
            if (!isActive) return;
            if (!uOscClient.isRunning) return;
            SendDeviceTransforms();
        }

        void SendDeviceTransforms()
        {
            foreach (var humanBodyBone in Enum.GetNames(typeof(HumanBodyBones)))
            {
                if (settings.TryGet(humanBodyBone, out var index))
                {
                    var transform = transforms.BoneTransforms[Enum.Parse<HumanBodyBones>(humanBodyBone)];
                    SendDeviceTransform(transform, index);
                }
            }
        }

        void SendDeviceTransform(Transform transform, int index)
        {
            var pos = transform.position;
            var rot = transform.rotation;
            uOscClient.Send(
                "/VMT/Room/Unity",
                index, 1 /* enabled */, 0f,
                pos.x, pos.y, pos.z,
                rot.x, rot.y, rot.z, rot.w);
        }
    }
}