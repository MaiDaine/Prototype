using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class HealthBarHandler : MonoBehaviour
    {
        public GameObject healthBar;
        public Image fillBar;

        private Quaternion iniRot;

        private void Awake()
        {
            iniRot = healthBar.transform.rotation;
        }

        public void Initialize()
        {
            if (healthBar.tag == "EnemyTeam")
                fillBar.color = new Color(1, 0, 0, 1);
            healthBar.SetActive(true);
        }

        public void SetFillAmount(float value)
        {
            fillBar.fillAmount = value;
        }

        private void LateUpdate()
        {
            healthBar.transform.rotation = iniRot;
        }
    }
}