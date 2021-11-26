using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{

    public int itemID;
    public Text priceText;
    public int price;
    public int lvl = 0;
    public Text lvlText;

    public GameObject UpgradeShip;

    void Update()
    {
        lvl = UpgradeShip.gameObject.GetComponent<UpgradeSpaceship>().upgradeItem[4, itemID];
        price = UpgradeShip.gameObject.GetComponent<UpgradeSpaceship>().upgradeItem[lvl + 1, itemID];
        priceText.text = "price: " + price;
        lvlText.text = "lvl: " + lvl;
        if (lvl == 3)
        {
            priceText.gameObject.SetActive(false);
        }
    }
}
