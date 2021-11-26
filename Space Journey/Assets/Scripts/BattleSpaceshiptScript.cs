using UnityEngine;

public class BattleSpaceshiptScript : MonoBehaviour
{
    Spaceship spaceship;

    [HideInInspector] public float strength;
    [HideInInspector] public float damage;

    protected GameObject pfbBullet;

    void Awake()
    {
        spaceship = GameObject.FindGameObjectWithTag("Player").GetComponent<Spaceship>();
        strength = PlayerPrefs.GetFloat("strength");
        damage = PlayerPrefs.GetFloat("damage");
        pfbBullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    public void ShotSpaceship(PirateScript pirate)
    {
        spaceship.setEnergy(spaceship.getEnergy() - spaceship.energyPerShot);

        pirate.strength -= this.damage;

        GameObject bullet = Instantiate(pfbBullet);
        bullet.transform.position = this.transform.position;
        bullet.transform.forward = this.transform.forward;
    }
}
