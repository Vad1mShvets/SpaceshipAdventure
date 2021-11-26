using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    Root root;
    Rigidbody rb;

    float crypto = 2500;
    float energy = 500000;
    float ore = 0;
    float damage = 50;
    float strength = 0;
    float energyPerBattle = 10;
    float energyFromOre = 5;
    float repairStrength = 20;
    float repairTimer = 600;

    [HideInInspector] public float energyPerHundredKm = 50;
    [HideInInspector] public float limitEnergy = 1000000;
    [HideInInspector] public float oreLimit = 2000;
    [HideInInspector] public float energyPerShot = 5;
    [HideInInspector] public float energyPerCollect = 1;
    [HideInInspector] public float orePerCollect = 20;

    Text txtCrypto;
    Text txtOre;
    Text txtEnergy;
    Text txtStrength;
    Text txtRepairTimer;
    Text txtGameOver;

    GameObject hint;
    GameObject canvasSpaceship;
    GameObject secondaryButtons;
    GameObject converterButton;
    GameObject repairerButton;
    GameObject gameOverPanel;

    int maxLevelItems = 0;
    int unlockedItems = 0;
    [HideInInspector] public int[] itemLevels = new int[10];

    [HideInInspector] public int countToSpawnBattle = 0;
    [HideInInspector] public float countBattleWins = 0;

    [HideInInspector] public bool isBattle = false;
    [HideInInspector] public bool isPlay = false;
    bool isRepairing = false;
    bool isGameOver = false;

    float turnSmoothVelocity;
    float updateTimer = 0;

    public void setCrypto(float crypto) { this.crypto = crypto; }
    public void setOre(float ore) { this.ore = ore; }
    public void setEnergy(float energy) { this.energy = energy; }
    public void setStrength(float strength) { this.strength = strength; }
    public void setItemsCount(int itemsCount) { this.unlockedItems = itemsCount; }

    public float getCrypto() { return crypto; }
    public float getOre() { return ore; }
    public float getEnergy() { return energy; }
    public float getStrength() { return strength; }
    public int getItemsCount() { return unlockedItems; }

    private void Awake()
    {
        root = GameObject.Find("Root").GetComponent<Root>();

        rb = this.GetComponent<Rigidbody>();

        hint = GameObject.Find("hintSpaceship");

        txtCrypto = GameObject.Find("cryptoCount").GetComponent<Text>();
        txtOre = GameObject.Find("oreCount").GetComponent<Text>();
        txtEnergy = GameObject.Find("energyCount").GetComponent<Text>();
        txtStrength = GameObject.Find("strengthCount").GetComponent<Text>();
        txtRepairTimer = GameObject.Find("repairTimer").GetComponent<Text>();
        txtGameOver = GameObject.Find("reason").GetComponent<Text>();

        canvasSpaceship = GameObject.Find("CanvasSpaceship");
        secondaryButtons = GameObject.Find("secondaryButtons");
        converterButton = GameObject.Find("converterButton");
        repairerButton = GameObject.Find("repairerButton");
        gameOverPanel = GameObject.Find("GameOverPanel");
    }

    private void Start()
    {
        secondaryButtons.SetActive(false);
        converterButton.SetActive(false);
        repairerButton.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isPlay)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.05f);

            if (direction.magnitude > 0)
            {
                energy -= 0.01f;
            }

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.AddForce(direction * 750000);

            if (hint != null)
            {
                hint.SetActive(false);
            }

            if (maxLevelItems == 10)
            {
                GameOver("you assembled the largest ship and pumped all modules to the maximum level!");
            }
            if (this.crypto >= 5000)
            {
                GameOver("congrats! you collect 5000 crypto!");
            }
            if (this.strength <= 0)
            {
                GameOver("your spaceship has been destroyed.");
            }
            if (this.energy <= 0 && this.ore <= 0)
            {
                GameOver("your spaceship is out of energy.");
            }
        }
        else
        {
            if (hint != false)
            {
                hint.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (isRepairing)
        {
            repairTimer -= Time.deltaTime;
            txtRepairTimer.text = Mathf.RoundToInt(repairTimer) + "";
        }

        if (repairTimer <= 0)
        {
            this.strength += repairStrength;
            isRepairing = false;
            repairTimer = 600;
        }

        updateTimer += Time.deltaTime;

        if (updateTimer > 1)
        {
            txtCrypto.text = "crypto: " + crypto;
            txtOre.text = "ore: " + ore;
            txtEnergy.text = "energy: " + energy;
            txtStrength.text = "strength: " + strength;

            if (countBattleWins >= 3)
            {
                countBattleWins = 0;
                root.SpawnAsteroid();
            }

            if (unlockedItems >= 7)
            {
                isPlay = true;
            }

            if (ore >= oreLimit)
            {
                txtOre.color = Color.red;
            }
            else
            {
                txtOre.color = Color.white;
            }

            if (energy >= limitEnergy)
            {
                txtEnergy.color = Color.red;
            }
            else
            {
                txtEnergy.color = Color.white;
            }

            if (unlockedItems == 7)
            {
                secondaryButtons.SetActive(true);
            }

            updateTimer = 0;
        }

        if (countToSpawnBattle >= 60)
        {
            isBattle = true;
            energy -= energyPerBattle;
            strength = PlayerPrefs.GetFloat("strength");
            root.SpawnBattle();
            ShowOtherCanvas(false);
            countToSpawnBattle = 0;
        }

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }

    public void ShowOtherCanvas(bool show)
    {
        canvasSpaceship.SetActive(show);
    }

    public void Repairer()
    {
        isRepairing = true;
    }

    public void Converter()
    {
        energy += ore / energyFromOre;
        ore = 0;
    }

    public void BuyEnergy()
    {
        if (crypto >= 1 && crypto <= 100)
        {
            do
            {
                energy++;
                crypto -= 0.5f;
            } while (crypto >= 0.5f);
        }
        if (crypto >= 100 && crypto <= 500)
        {
            do
            {
                energy++;
                crypto -= 0.4f;
            } while (crypto >= 0.4f);
        }
        if (crypto >= 500 && crypto <= 1500)
        {
            do
            {
                energy++;
                crypto -= 0.3f;
            } while (crypto >= 0.3f);
        }
        if (crypto >= 1500)
        {
            do
            {
                energy++;
                crypto -= 0.1f;
            } while (crypto >= 0.1f);
        }
    }

    void GameOver(string reason)
    {
        isGameOver = true;

        foreach (var canvas in GameObject.FindGameObjectsWithTag("Canvas"))
        {
            canvas.SetActive(false);
        }

        gameOverPanel.SetActive(true);

        txtGameOver.text = reason;

        Time.timeScale = 0;
    }

    public void SellOre()
    {
        if (this.getOre() >= 1 && this.getOre() < 100)
        {
            this.setCrypto(this.getCrypto() + this.getOre() * 0.12f);
            this.setOre(this.getOre() - this.getOre());
        }
        if (this.getOre() >= 100 && this.getOre() < 500)
        {
            this.setCrypto(this.getCrypto() + this.getOre() * 0.1f);
            this.setOre(this.getOre() - this.getOre());
        }
        if (this.getOre() >= 500 && this.getOre() < 1500)
        {
            this.setCrypto(this.getCrypto() + this.getOre() * 0.08f);
            this.setOre(this.getOre() - this.getOre());
        }
        if (this.getOre() >= 1500)
        {
            this.setCrypto(this.getCrypto() + this.getOre() * 0.06f);
            this.setOre(this.getOre() - this.getOre());
        }
    }

    public void UpdateShipParameters()
    {
        //body 
        if (itemLevels[0] == 1)
        {
            strength += 100;
        }
        if (itemLevels[0] == 2)
        {
            strength += 100;
        }
        if (itemLevels[0] == 3)
        {
            strength += 100;
            maxLevelItems += 1;
        }

        //command center
        if (itemLevels[1] == 1)
        {
            strength += 10;
        }
        if (itemLevels[1] == 2)
        {
            strength += 10;
        }
        if (itemLevels[1] == 3)
        {
            strength += 10;
            maxLevelItems += 1;
        }

        //battery
        if (itemLevels[2] == 1)
        {
            strength += 10;
            limitEnergy = 1000000;
        }
        if (itemLevels[2] == 2)
        {
            strength += 5;
            limitEnergy = 2000000;
        }
        if (itemLevels[2] == 3)
        {
            strength += 5;
            limitEnergy = 3000000;
            maxLevelItems += 1;
        }

        //storage
        if (itemLevels[3] == 1)
        {
            strength += 10;
            oreLimit = 2000;
        }
        if (itemLevels[3] == 2)
        {
            strength += 5;
            oreLimit = 3000;
        }
        if (itemLevels[3] == 3)
        {
            strength += 5;
            oreLimit = 4000;
            maxLevelItems += 1;
        }

        //guns
        if (itemLevels[4] == 1)
        {
            strength -= 5;
            damage = 50;
        }
        if (itemLevels[4] == 2)
        {
            strength += 2;
            damage = 60;
        }
        if (itemLevels[4] == 3)
        {
            strength += 2;
            damage = 75;
            maxLevelItems += 1;
        }

        //collector
        if (itemLevels[5] == 1)
        {
            strength += 10;
            orePerCollect = 20;
        }
        if (itemLevels[5] == 2)
        {
            strength += 12;
            orePerCollect = 30;
        }
        if (itemLevels[5] == 3)
        {
            strength += 15;
            orePerCollect = 40;
            maxLevelItems += 1;
        }

        //engine
        if (itemLevels[6] == 1)
        {
            strength -= 10;
            energyPerBattle = 10;
            energyPerHundredKm = 50;
        }
        if (itemLevels[6] == 2)
        {
            strength += 2;
            energyPerBattle = 8;
            energyPerHundredKm = 48;
        }
        if (itemLevels[6] == 3)
        {
            strength += 3;
            energyPerBattle = 6;
            energyPerHundredKm = 45;
            maxLevelItems += 1;
        }

        //converter
        if (itemLevels[7] == 1)
        {
            strength -= 5;
            energyFromOre = 5;

            converterButton.SetActive(true);
        }
        if (itemLevels[7] == 2)
        {
            strength += 2;
            energyFromOre = 4;
        }
        if (itemLevels[7] == 3)
        {
            strength += 3;
            energyFromOre = 3;
            maxLevelItems += 1;
        }

        //generator
        if (itemLevels[8] == 1)
        {
            strength += 5;
        }
        if (itemLevels[8] == 2)
        {
            strength += 3;
        }
        if (itemLevels[8] == 3)
        {
            strength += 2;
            maxLevelItems += 1;
        }

        //repairer
        if (itemLevels[9] == 1)
        {
            strength += 5;
            repairStrength = 20;

            repairerButton.SetActive(true);
        }
        if (itemLevels[9] == 2)
        {
            strength += 3;
            repairStrength += 5;
        }
        if (itemLevels[9] == 3)
        {
            strength += 2;
            repairStrength += 5;
            maxLevelItems += 1;
        }

        PlayerPrefs.SetFloat("strength", strength);
        PlayerPrefs.SetFloat("damage", damage);
    }
}
