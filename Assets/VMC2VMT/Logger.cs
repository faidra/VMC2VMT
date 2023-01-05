using System.Text;
using UniRx;
using UnityEngine;

namespace VMC2VMT
{
    public sealed class Logger : MonoBehaviour
    {
        readonly ReactiveProperty<string> log = new();
        public IReadOnlyReactiveProperty<string> Log => log;
        readonly StringBuilder stringBuilder = new();

        const int maxLength = 5000;

        public void AddLog(string str)
        {
            if (stringBuilder.Length > maxLength) stringBuilder.Clear(); // 大量にログが溜まっているということは、たぶん常に出続けてると思うので雑に消す
            stringBuilder.Insert(0, "\n");
            stringBuilder.Insert(0, str);
            log.Value = stringBuilder.ToString();
        }

        public void AddLogError(string str)
        {
            AddLog($"<color=red>{str}</color>");
        }
    }
}
