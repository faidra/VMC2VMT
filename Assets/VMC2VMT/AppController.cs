using System;
using UniRx;
using UnityEngine;

namespace VMC2VMT
{
    public sealed class AppController : MonoBehaviour
    {
        [SerializeField] Settings settings;
        [SerializeField] VMCReceiver vmcReceiver;
        [SerializeField] VMTSender vmtSender;
        [SerializeField] Transform mrRoot;
        [SerializeField] Transforms transforms;

        void Start()
        {
            Application.targetFrameRate = 120;
            
            settings.ReadSettings();
            
            DelayIfTrue(vmcReceiver.Available).Subscribe(vmtSender.SetActive).AddTo(this); // Poseの設定完了を(雑に)待つ

            vmcReceiver.MrScaleAndOffset.Subscribe(v =>
            {
                mrRoot.localScale = v.scale;
                mrRoot.position = v.offset;
            }).AddTo(this);

            vmcReceiver.RootPose.Subscribe(transforms.SetRootPose).AddTo(this);
            vmcReceiver.BonePoses.Subscribe(kv => transforms.SetBonePose(kv.humanBodyBones, kv.pose)).AddTo(this);
        }

        static IObservable<bool> DelayIfTrue(IObservable<bool> source) =>
            source
                .Select(available => available ? Observable.Return(true).Delay(TimeSpan.FromSeconds(1f)) : Observable.Return(false))
                .Switch()
                .StartWith(false)
                .DistinctUntilChanged();
    }
}
