using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class UnitStatusManager : MonoBehaviour
    {
        private List<UnitStatus> status = new List<UnitStatus>();
        private List<UnitStatus> timedStatus = new List<UnitStatus>();

        private Unit unit;

        private void Start()
        {
            unit = GetComponent<Unit>();
        }

        public void RegisterStatus(UnitStatus status)
        {
            this.status.Add(status);
        }

        public void RegisterTimedStatus(UnitStatus status)
        {
            timedStatus.Add(status);
        }

        public void UnRegisterStatus(UnitStatus status)
        {
            this.status.Remove(status);
        }

        public void UnRegisterTimedStatus(UnitStatus status)
        {
            timedStatus.Remove(status);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            int count = timedStatus.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                timedStatus[i].duration -= deltaTime;
                if (timedStatus[i].duration <= 0f)
                {
                    timedStatus[i].OnStatusEnd(unit);
                    timedStatus.RemoveAt(i);
                }
                else
                    timedStatus[i].Update();
            }
        }

        private void OnDestroy()
        {
            status.Clear();
            timedStatus.Clear();
        }
    }
}