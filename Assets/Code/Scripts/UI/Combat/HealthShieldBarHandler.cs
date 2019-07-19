using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class HealthShieldBarHandler : HealthBarHandler
    {
        public Image shieldFillBar;

        private RectTransform shieldRect;
        private float barLenght;

        protected override void Awake()
        {
            base.Awake();
            shieldRect = shieldFillBar.GetComponent<RectTransform>();
            barLenght = shieldRect.rect.width * shieldRect.localScale.x;
        }

        public void SetShieldFillAmount(float value)
        {
            float tmp = healthFillBar.fillAmount + value;
            if (tmp > 1f)
                tmp = 1f - value;
            else
                tmp = healthFillBar.fillAmount;
            shieldFillBar.fillAmount = value;
        }
    }
}