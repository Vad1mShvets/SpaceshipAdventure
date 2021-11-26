using UnityEngine;
using UnityEngine.UI;

public class ArrowWithSort : MonoBehaviour
{
	GameObject fromWho;
	GameObject[] targets;
	GameObject closest;

	Vector3 direction;

	Text txtMagnitudeToTarget;
	Text arrowHint;

	float radius = 30;

    private void Awake()
    {
		SearchTargets();
		txtMagnitudeToTarget = GameObject.Find("Magnitude To Ore").GetComponent<Text>();
		arrowHint = GameObject.Find("Arrow Hint").GetComponent<Text>();
	}

	void SearchTargets()
    {
		targets = GameObject.FindGameObjectsWithTag("Ore");
		fromWho = GameObject.FindGameObjectWithTag("Player");
	}

    void Start()
	{
		this.transform.position = fromWho.transform.position;
	}

	GameObject FindClosestOre()
	{
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in targets)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	void Update()
	{
        if (fromWho.GetComponent<Spaceship>().isPlay)
        {
			foreach (GameObject target in targets)
			{
				if (target != null)
				{
					direction = FindClosestOre().transform.position - fromWho.transform.position;
					txtMagnitudeToTarget.text = "distance to the nearest source of ore: " + Mathf.Round(direction.magnitude - 50);
					this.transform.forward = direction.normalized;

					Vector3 t = fromWho.transform.position + direction.normalized * radius;
					this.transform.position = Vector3.Lerp(this.transform.position, t, 10f * Time.deltaTime);

					SearchTargets();
				}
				else
				{
					Destroy(this.gameObject);
				}
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
		arrowHint.gameObject.SetActive(false);
		fromWho.transform.position = FindClosestOre().transform.position + new Vector3(0, 50, -30);
		fromWho.GetComponent<Spaceship>().setEnergy(Mathf.Round(fromWho.GetComponent<Spaceship>().getEnergy() - (direction.magnitude / 100 * 50)));
	}
}