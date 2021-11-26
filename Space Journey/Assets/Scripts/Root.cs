using UnityEngine;

public class Root : MonoBehaviour
{
    Camera mainCamera;
    Light dirLight;

    GameObject spaceship;
    GameObject orbitalStation;
    GameObject asteroid;
    GameObject littlePlanet;
    GameObject arrowOre;
    GameObject arrowStation;
    GameObject battle;

    static int fieldSize = 40;
    int cubeSize = 100;
    int[,] fieldLogic = new int[fieldSize, fieldSize];

    private void Awake()
    {
        #region loading objects from resources
        mainCamera = Resources.Load<Camera>("Prefabs/Main Camera");
        dirLight = Resources.Load<Light>("Prefabs/Directional Light");

        spaceship = Resources.Load<GameObject>("Prefabs/Spaceship");
        orbitalStation = Resources.Load<GameObject>("Prefabs/Orbital Station");
        asteroid = Resources.Load<GameObject>("Prefabs/Asteroid");
        littlePlanet = Resources.Load<GameObject>("Prefabs/Little Planet");
        arrowOre = Resources.Load<GameObject>("Prefabs/Arrow Ore");
        arrowStation = Resources.Load<GameObject>("Prefabs/Arrow Station");
        battle = Resources.Load<GameObject>("Prefabs/Battle");
        #endregion
    }

    private void Start()
    {
        PlayerPrefs.DeleteKey("strength");
        PlayerPrefs.DeleteKey("damage");

        //creating gameplay field from with matrix
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                fieldLogic[i, j] = 0; //all the cells are 0 now
            }
        }

        for (int i = 1; i <= 4; i++)
        {
            //random 4 cells in the array get their numbers
            fieldLogic[Random.Range(0, fieldSize), Random.Range(0, fieldSize)] = i;
        }

        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                //if cycle will find the number that different from zero - object with this number will be instantiated;
                if (fieldLogic[i, j] == 1)
                {
                    Instantiate(spaceship, new Vector3(j * cubeSize, 50, i * cubeSize), Quaternion.identity);
                    Instantiate(mainCamera, new Vector3(j * cubeSize, 105, i * cubeSize - 5), Quaternion.Euler(new Vector3(80, 0, 0)));
                }
                else if (fieldLogic[i, j] == 2)
                {
                    Instantiate(orbitalStation, new Vector3(j * cubeSize, 0, i * cubeSize), Quaternion.identity);
                }
                else if (fieldLogic[i, j] == 3)
                {
                    Instantiate(littlePlanet, new Vector3(j * cubeSize, 0, i * cubeSize), Quaternion.identity);
                }
                else if (fieldLogic[i, j] == 4)
                {
                    Instantiate(littlePlanet, new Vector3(j * cubeSize, 0, i * cubeSize), Quaternion.identity);
                }
            }
        }

        Instantiate(dirLight, new Vector3(0, 300, 0), Quaternion.Euler(new Vector3(60, -20, -40)));
        Instantiate(arrowOre, Vector3.zero, Quaternion.identity);
        Instantiate(arrowStation, Vector3.zero, Quaternion.identity);
    }

    public void SpawnAsteroid()
    {
        Instantiate(asteroid, new Vector3(GameObject.FindGameObjectWithTag("Orbital Station").transform.position.x + Random.Range(-300, 300), 0, GameObject.FindGameObjectWithTag("Orbital Station").transform.position.z + Random.Range(-300, 300)), Quaternion.identity);
    }

    public void SpawnBattle()
    {
        Instantiate(battle, Vector3.zero, Quaternion.identity);
    }
}
