using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    public GameObject healthBar;
    public Image fillBar;

    private Vector3 direction;

    public void Initialize()
    {
        Unit unit = GetComponentInParent<Unit>();
        if (unit.gameObject.tag == "EnemyTeam")
            fillBar.color = new Color(1, 0, 0, 1);
        healthBar.SetActive(true);
        direction = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    public void SetFillAmount(float value)
    {
        fillBar.fillAmount = value;
    }

    private void Update()
    {
        healthBar.transform.LookAt(new Vector3(transform.position.x, direction.y, direction.z));
    }
}
