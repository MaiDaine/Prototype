using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    public GameObject healtbar;
    public Image fillBar;

    private Vector3 direction;

    public void Initialize()
    {
        Unit unit = GetComponentInParent<Unit>();
        if (unit.gameObject.tag == "EnemyTeam")
            fillBar.color = new Color(1, 0, 0, 1);
        healtbar.SetActive(true);
    }

    public void SetFillAmount(float value)
    {
        fillBar.fillAmount = value;
    }

    private void Update()
    {
        healtbar.transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
    }
}
