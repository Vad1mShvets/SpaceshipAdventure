using UnityEngine;

public class OrbitalStation : MonoBehaviour
{
    Spaceship spaceship;

    GameObject canvas;
    GameObject viewUpgradeShip;
    GameObject viewOreToSell;

    private void Start()
    {
        spaceship = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>();
        canvas = GameObject.Find("CanvasOrbital");
        viewOreToSell = GameObject.Find("Buy/Sell Resources");
        viewUpgradeShip = GameObject.Find("Upgrade Ship");

        viewOreToSell.gameObject.SetActive(false);
        viewUpgradeShip.gameObject.SetActive(false);
        canvas.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (viewOreToSell != null)
            {
                viewOreToSell.SetActive(false);
            }
            if (viewUpgradeShip != null)
            {
                viewUpgradeShip.SetActive(false);
            }
            
            canvas.SetActive(true);
        }
    }

    public void ShowMenu(int menu)
    {
        if (menu == 1)
        {
            if (viewUpgradeShip.activeSelf == false)
            {
                viewUpgradeShip.SetActive(true);
                canvas.SetActive(false);
            }
        }
        else
        {
            if (viewOreToSell.activeSelf == false)
            {
                viewOreToSell.SetActive(true);
                canvas.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
