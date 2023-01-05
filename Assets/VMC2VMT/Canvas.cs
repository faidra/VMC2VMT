using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace VMC2VMT
{
    public sealed class Canvas : MonoBehaviour
    {
        [SerializeField] VMCReceiver vmcReceiver;
        [SerializeField] Logger logger;

        [SerializeField] Text vmcStatus;
        [SerializeField] Text log;

        void Start()
        {
            vmcReceiver.VmcStatus.SubscribeToText(vmcStatus).AddTo(this);
            logger.Log.SubscribeToText(log).AddTo(this);
        }
    }
}
