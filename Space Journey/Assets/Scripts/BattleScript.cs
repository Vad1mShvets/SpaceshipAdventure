using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    Spaceship spaceship;

    Vector3[] position = { new Vector3(0, -5, 30), new Vector3(5, 0, 40), new Vector3(-7, -1.5f, 40) };
    public PirateScript[] pirates = new PirateScript[3];
    int[] index = { 0, 1, 2 };

    public GameObject[] battleElements;

    int attackIndex = 0;

    float timer = 0;
    float piratesStrength;
    float spaceshipStrength;

    Text txtStrengthPirates;
    Text txtStrengthSpaceship;

    private void Awake()
    {
        txtStrengthPirates = GameObject.Find("piratesStrength").GetComponent<Text>();
        txtStrengthSpaceship = GameObject.Find("spaceshipStrength").GetComponent<Text>();
        spaceship = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>();
    }

    private void Start()
    {
        spaceshipStrength = battleElements[0].GetComponent<BattleSpaceshiptScript>().strength;

        for (int i = 0; i < index.Length; i++)
        {
            int j = Random.Range(0, i + 1);
            var temp = index[j];
            index[j] = index[i];
            index[i] = temp;
        }

        for (int i = 0; i < index.Length; i++)
        {
            pirates[i].transform.position = position[index[i]];
            pirates[i].transform.localScale *= Random.Range(0.85f, 1.35f);
        }

        Attack(attackIndex);
    }

    private void Update()
    {
        piratesStrength = pirates[0].strength + pirates[1].strength + pirates[2].strength;
        spaceshipStrength = battleElements[0].GetComponent<BattleSpaceshiptScript>().strength;

        txtStrengthPirates.text = "strength of pirates: " + piratesStrength;
        txtStrengthSpaceship.text = "your strength: " + spaceshipStrength;

        if (piratesStrength <= 0)
        {
            EndBattle(true);
        }
        else if (spaceshipStrength <= 0)
        {
            EndBattle(false);
        }

        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            if (attackIndex < battleElements.Length - 1)
            {
                attackIndex++;
                Attack(attackIndex);
                timer = 0;
            }
            else
            {
                attackIndex = 0;
                Attack(attackIndex);
                timer = 0;
            }
        }
    }

    void Attack(int i)
    {
        int index = Random.Range(0, 3);
        if (battleElements[i].GetComponent<PirateScript>() != null)
        {
            battleElements[i].GetComponent<PirateScript>().ShotPirate();
        }
        else if (battleElements[i].GetComponent<BattleSpaceshiptScript>() != null)
        {
            battleElements[i].GetComponent<BattleSpaceshiptScript>().transform.forward = pirates[index].transform.position;
            battleElements[i].GetComponent<BattleSpaceshiptScript>().ShotSpaceship(pirates[index]);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>().setEnergy(spaceship.getEnergy() - spaceship.energyPerShot);
        }
    }

    void EndBattle(bool isWin)
    {
        spaceship.isBattle = false;
        spaceship.ShowOtherCanvas(true);
        PlayerPrefs.SetFloat("strength", battleElements[0].GetComponent<BattleSpaceshiptScript>().strength);

        //saving the result in txt file
        if (isWin)
        {
            Destroy(this.gameObject);
            spaceship.setStrength(PlayerPrefs.GetFloat("strength"));
            spaceship.countBattleWins++;

            MakeLog("win"); //log win;
        }
        else
        {
            spaceship.setStrength(PlayerPrefs.GetFloat("strength"));

            MakeLog("lose"); //log lose;
        }
    }

    void MakeLog(string log)
    {
        PlayerPrefs.SetInt("battles", PlayerPrefs.GetInt("battles") + 1);

        string fileName = "/Logs/Battle" + PlayerPrefs.GetInt("battles") + ".txt";
        string path = Application.dataPath + fileName;

        File.WriteAllText(path, log + " - " + System.DateTime.Now + "\n");
    }
}
