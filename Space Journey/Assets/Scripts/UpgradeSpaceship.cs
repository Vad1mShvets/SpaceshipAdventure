using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSpaceship : MonoBehaviour
{
    public Spaceship spaceship;
    public int[,] upgradeItem = new int[5, 10];
    public GameObject[] shipItems;

    private void Start()
    {
        #region ID's
        for (int i = 0; i < 10; i++)
        {
            upgradeItem[1, i] = i;
        }
        #endregion

        #region price #1
        upgradeItem[1, 0] = 100;
        upgradeItem[1, 1] = 100;
        upgradeItem[1, 2] = 200;
        upgradeItem[1, 3] = 150;
        upgradeItem[1, 4] = 50;
        upgradeItem[1, 5] = 150;
        upgradeItem[1, 6] = 75;
        upgradeItem[1, 7] = 200;
        upgradeItem[1, 8] = 250;
        upgradeItem[1, 9] = 350;
        #endregion

        #region price #2
        upgradeItem[2, 0] = 300;
        upgradeItem[2, 1] = 250;
        upgradeItem[2, 2] = 300;
        upgradeItem[2, 3] = 300;
        upgradeItem[2, 4] = 65;
        upgradeItem[2, 5] = 270;
        upgradeItem[2, 6] = 131;
        upgradeItem[2, 7] = 270;
        upgradeItem[2, 8] = 388;
        upgradeItem[2, 9] = 438;
        #endregion

        #region price #3
        upgradeItem[3, 0] = 900;
        upgradeItem[3, 1] = 625;
        upgradeItem[3, 2] = 450;
        upgradeItem[3, 3] = 450;
        upgradeItem[3, 4] = 85;
        upgradeItem[3, 5] = 486;
        upgradeItem[3, 6] = 230;
        upgradeItem[3, 7] = 365;
        upgradeItem[3, 8] = 601;
        upgradeItem[3, 9] = 547;
        #endregion

        #region level
        for (int i = 0; i < 10; i++)
        {
            upgradeItem[4, i] = 0;
        }
        #endregion
    }

    public void UpgradeShip()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (spaceship.getCrypto() >= upgradeItem[1, ButtonRef.GetComponent<ButtonInfo>().itemID])
        {
            if (upgradeItem[4, ButtonRef.GetComponent<ButtonInfo>().itemID] < 3) //check if level < 3
            {
                int itemLevel = ButtonRef.GetComponent<ButtonInfo>().lvl;
                spaceship.setCrypto(spaceship.getCrypto() - upgradeItem[itemLevel + 1, ButtonRef.GetComponent<ButtonInfo>().itemID]);
                upgradeItem[4, ButtonRef.GetComponent<ButtonInfo>().itemID]++;

                spaceship.itemLevels[ButtonRef.GetComponent<ButtonInfo>().itemID] = upgradeItem[4, ButtonRef.GetComponent<ButtonInfo>().itemID];
                spaceship.UpdateShipParameters();

                if (upgradeItem[4, ButtonRef.GetComponent<ButtonInfo>().itemID] == 1)
                {
                    spaceship.setItemsCount(spaceship.getItemsCount() + 1); //unlock item
                }
            }
        }

        if (ButtonRef.GetComponent<ButtonInfo>().itemID >= 0)
        {
            shipItems[ButtonRef.GetComponent<ButtonInfo>().itemID].SetActive(true);
        }
    }
}
