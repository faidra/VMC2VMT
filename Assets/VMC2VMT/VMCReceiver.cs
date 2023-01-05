using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using uOSC;

namespace VMC2VMT
{
    public sealed class VMCReceiver : MonoBehaviour
    {
        [SerializeField] uOscServer uOscServer;

        readonly ReactiveProperty<bool> available = new();
        public IReadOnlyReactiveProperty<bool> Available => available;

        readonly ReactiveProperty<PositionAndRotation> rootPose = new();
        public IReadOnlyReactiveProperty<PositionAndRotation> RootPose => rootPose;

        readonly ReactiveProperty<(Vector3 scale, Vector3 offset)> mrScaleAndOffset = new((Vector3.one, Vector3.zero));
        public IReadOnlyReactiveProperty<(Vector3 scale, Vector3 offset)> MrScaleAndOffset => mrScaleAndOffset;

        IReadOnlyDictionary<HumanBodyBones, ReactiveProperty<PositionAndRotation>> bonePoses;

        public IObservable<(HumanBodyBones humanBodyBones, PositionAndRotation pose)> BonePoses
        {
            get
            {
                InitializeBonePoses();
                return bonePoses.Select(kv => kv.Value.Select(v => (kv.Key, v))).Merge();
            }
        }

        void Start()
        {
            InitializeBonePoses();

            uOscServer.onDataReceived.AsObservable().Subscribe(OnDataReceived).AddTo(this);

            uOscServer.StartServer();
            Disposable.Create(() =>
            {
                if (uOscServer != null) uOscServer.StopServer();
            }).AddTo(this);
        }

        void OnDataReceived(Message message)
        {
            var values = message.values;
            try
            {
                switch (message.address)
                {
                    case "/VMC/Ext/OK":
                        if (values.Length == 1)
                        {
                            OnOkReceived((int) values[0]);
                            return;
                        }
                        else if (values.Length == 2)
                        {

                            OnOkReceived((int) values[0], (int) values[1]);
                            return;
                        }
                        else if (values.Length >= 3)
                        {

                            OnOkReceived((int) values[0], (int) values[1], (int) values[2]);
                            return;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    case "/VMC/Ext/Root/Pos":
                        if (values.Length == 8)
                        {
                            OnRootPosReceived((string) values[0], (float) values[1], (float) values[2], (float) values[3], (float) values[4], (float) values[5], (float) values[6], (float) values[7]);
                            return;
                        }
                        else if (values.Length >= 14)
                        {

                            OnRootPosReceived((string) values[0], (float) values[1], (float) values[2], (float) values[3], (float) values[4], (float) values[5], (float) values[6], (float) values[7], (float) values[8], (float) values[9], (float) values[10], (float) values[11], (float) values[12], (float) values[13]);
                            return;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    case "/VMC/Ext/Bone/Pos":
                        OnBonePosReceived((string) values[0], (float) values[1], (float) values[2], (float) values[3], (float) values[4], (float) values[5], (float) values[6], (float) values[7]);
                        return;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        void OnOkReceived(int loaded)
        {
            available.Value = loaded == 1 /* 読み込み後 */;
        }

        void OnOkReceived(int loaded, int calibrationState)
        {
            available.Value = loaded == 1  /* 読み込み後 */ &&
                calibrationState == 3 /* Calibrated */;
        }

        void OnOkReceived(int loaded, int calibrationState, int trackingStatus)
        {
            available.Value = loaded == 1  /* 読み込み後 */ &&
                calibrationState == 3 /* Calibrated */ &&
                trackingStatus == 1 /* 正常 */;
        }

        void OnRootPosReceived(string name, float px, float py, float pz, float qx, float qy, float qz, float qw)
        {
            if (name != "root") throw new ArgumentOutOfRangeException();
            rootPose.Value = new PositionAndRotation(new Vector3(px, py, pz), new Quaternion(qx, qy, qz, qw));
        }

        void OnRootPosReceived(string name, float px, float py, float pz, float qx, float qy, float qz, float qw, float sx, float sy, float sz, float ox, float oy, float oz)
        {
            if (name != "root") throw new ArgumentOutOfRangeException();
            rootPose.Value = new PositionAndRotation(new Vector3(px, py, pz), new Quaternion(qx, qy, qz, qw));
            mrScaleAndOffset.Value = (new Vector3(sx, sy, sz), new Vector3(ox, oy, oz));
        }

        void OnBonePosReceived(string name, float px, float py, float pz, float qx, float qy, float qz, float qw)
        {
            bonePoses[Enum.Parse<HumanBodyBones>(name)].Value =
                new PositionAndRotation(new Vector3(px, py, pz), new Quaternion(qx, qy, qz, qw));
        }

        void InitializeBonePoses()
        {
            if (bonePoses == null)
            {
                bonePoses = Enum.GetValues(typeof(HumanBodyBones)).Cast<HumanBodyBones>()
                    .ToDictionary(v => v, _ => new ReactiveProperty<PositionAndRotation>(PositionAndRotation.Default));
            }
        }
    }
}
