using UnityEngine;
using UnityEngine.UI;

public class LittlePlanetScript : MonoBehaviour
{
    protected Spaceship spaceship;
    protected float ore = 9999999999;
    protected string planetName = "";
    protected float collectTimer = 0;
    protected bool doCollect = false;

    private string[] jupiterSattelites = { "Io", "Ganymede", "Callisto", "Amaltea", "Himalia", "Elara", "Pasiphae", "Sinope", "Lysithea", "Carme", "Ananke", "Leda", "Thebe", "Adrastea", "Metis", "Callirrhoe" };

    public Text txtPlanetName;
    public GameObject canvas;

    public Slider slider;

    private void Start()
    {
        planetName = jupiterSattelites[Random.Range(0, jupiterSattelites.Length)];
        txtPlanetName.text = planetName;

        canvas = GameObject.Find("CanvasLittlePlanet");
        spaceship = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>();

        canvas.SetActive(false);
        txtPlanetName.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (doCollect == true)
        {
            slider.gameObject.SetActive(true);

            collectTimer += Time.deltaTime;
            slider.value = collectTimer / 2;

            if (collectTimer > 2)
            {
                collectTimer = 0;

                if (spaceship.getOre() <= spaceship.oreLimit)
                {
                    spaceship.countToSpawnBattle = Random.Range(0, 100);

                    spaceship.setEnergy(spaceship.getEnergy() - spaceship.energyPerCollect);
                    spaceship.setOre(spaceship.getOre() + spaceship.orePerCollect);

                    this.ore -= spaceship.orePerCollect;
                }

                slider.gameObject.SetActive(false);
                doCollect = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (spaceship.isBattle == false)
            {
                canvas.SetActive(true);
                txtPlanetName.gameObject.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
                txtPlanetName.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        txtPlanetName.gameObject.SetActive(false);
    }

    public void Mine()
    {
        doCollect = true;
    }
}
