using UnityEngine;
using UnityEngine.UI;

public class AsteroidScript : LittlePlanetScript
{
    private char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public Text txtAsteroidOre;

    private void Awake()
    {
        spaceship = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>();
    }

    private void Start()
    {
        ore = Random.Range(100, 1000);

        for (int i = 0; i < 8; i++)
        {
            if (i < Random.Range(3, 6))
            {
                planetName += alphabet[Random.Range(0, alphabet.Length)].ToString();
            }
            else
            {
                planetName += numbers[Random.Range(0, numbers.Length)].ToString();
            }
        }

        txtAsteroidOre.text = this.ore + " - available ore";
        txtPlanetName.text = planetName;

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
            slider.value = collectTimer / 1;

            if (collectTimer > 1)
            {
                collectTimer = 0;

                if (spaceship.getOre() <= spaceship.oreLimit)
                {
                    spaceship.setEnergy(spaceship.getEnergy() - spaceship.energyPerCollect);
                    spaceship.setOre(spaceship.getOre() + spaceship.orePerCollect);

                    this.ore -= spaceship.orePerCollect;
                    txtAsteroidOre.text = this.ore + " - available ore";

                    if (this.ore <= 0)
                    {
                        this.gameObject.SetActive(false);
                    }
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
                txtAsteroidOre.gameObject.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
                txtPlanetName.gameObject.SetActive(false);
                txtAsteroidOre.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        txtPlanetName.gameObject.SetActive(false);
    }
}
