using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class HealthBarHandler : MonoBehaviour
    {
        public Image fillBar;

        private Quaternion iniRot;

        private void Awake()
        {
            iniRot = gameObject.transform.rotation;
        }

        public void Initialize(Unit unit)
        {
            if (unit.tag == "EnemyTeam")
                fillBar.color = new Color(1, 0, 0, 1);
            gameObject.SetActive(true);
        }

        public void SetFillAmount(float value)
        {
            fillBar.fillAmount = value;
        }

        private void LateUpdate()
        {
            gameObject.transform.rotation = iniRot;
        }
    }
}