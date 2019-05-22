using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    [RequireComponent(typeof(Image))]
    public class CardCooldown : MonoBehaviour
    {
        public Text cooldownText;

        private Image mask;
        private bool onCooldown;
        private float cooldown;
        private float maxCooldown;

        public void Initialize(float cooldownTime)
        {
            maxCooldown = cooldownTime;
            cooldown = cooldownTime;
        }

        public bool IsOnCooldown() { return onCooldown; }

        public void StartCooldown()
        {
            onCooldown = true;
            mask.enabled = true;
            cooldownText.enabled = true;
        }

        public void ResetCooldown()
        {
            mask.enabled = false;
            mask.fillAmount = 1;

            cooldownText.enabled = false;
            cooldownText.text = "";

            cooldownText.text = maxCooldown.ToString();

            onCooldown = false;
        }

        private void Start()
        {
            mask = this.gameObject.GetComponent<Image>();
        }

        private void FixedUpdate()
        {
            if (onCooldown)
            {
                cooldown -= Time.deltaTime;
                cooldownText.text = cooldown.ToString();

                mask.fillAmount -= 1 / maxCooldown * Time.deltaTime;

                if (mask.fillAmount <= 0)
                    ResetCooldown();
            }
        }
    }
}