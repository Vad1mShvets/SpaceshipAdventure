using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
	GameObject fromWho;
	GameObject target;
	Vector3 direction;

	Text txtMagnitudeToTarget;

	float radius = 33;

    private void Awake()
	{
		target = GameObject.FindGameObjectWithTag("Orbital Station");
		fromWho = GameObject.FindGameObjectWithTag("Player");
		txtMagnitudeToTarget = GameObject.Find("Magnitude To Home").GetComponent<Text>();
	}

    void Start()
	{
		this.transform.position = fromWho.transform.position;
		TeleportToTarget();
	}

	void Update()
	{
        if (fromWho.GetComponent<Spaceship>().isPlay)
        {
			if (target != null)
			{
				direction = target.transform.position - fromWho.transform.position;
				txtMagnitudeToTarget.text = "distance to orbital station: " + Mathf.Round(direction.magnitude - 50);
				this.transform.forward = direction.normalized;

				Vector3 t = fromWho.transform.position + direction.normalized * radius;
				this.transform.position = Vector3.Lerp(this.transform.position, t, 10f * Time.deltaTime);
			}
			else
			{
				Destroy(this.gameObject);
			}

			txtMagnitudeToTarget.gameObject.SetActive(true);
		}
		else
        {
			txtMagnitudeToTarget.gameObject.SetActive(false);
		}
	}

	public void TeleportToTarget()
    {
		fromWho.transform.position = target.transform.position + new Vector3(0, 50, -30);
		fromWho.GetComponent<Spaceship>().setEnergy(Mathf.Round(fromWho.GetComponent<Spaceship>().getEnergy() - (direction.magnitude / 100 * 50)));
    }
}
