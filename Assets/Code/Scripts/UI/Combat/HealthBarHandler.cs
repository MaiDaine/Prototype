using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class HealthBarHandler : MonoBehaviour
    {
        public Image healthFillBar;

        private Quaternion iniRot;

        protected virtual void Awake()
        {
            iniRot = gameObject.transform.rotation;
        }

        public void Initialize(Unit unit)
        {
            if (unit.tag == "EnemyTeam")
                healthFillBar.color = new Color(1, 0, 0, 1);
            gameObject.SetActive(true);
        }

        public void SetHealthFillAmount(float value)
        {
            healthFillBar.fillAmount = value;
        }

        private void LateUpdate()
        {
            gameObject.transform.rotation = iniRot;
        }
    }
}