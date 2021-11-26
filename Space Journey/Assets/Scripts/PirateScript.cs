using UnityEngine;

public class PirateScript : MonoBehaviour
{
    private GameObject spaceship;

    [HideInInspector] public float strength;
    [HideInInspector] public float damage;

    protected GameObject pfbBullet;

    private void Awake()
    {
        spaceship = GameObject.Find("Spaceship Battle");
        pfbBullet = Resources.Load<GameObject>("Prefabs/Bullet");
        strength = Random.Range(50, 100);
        damage = Random.Range(1, 10);
    }

    private void Update()
    {
        this.transform.LookAt(spaceship.transform.position);
    }

    public void ShotPirate()
    {
        spaceship.GetComponent<BattleSpaceshiptScript>().strength -= this.damage;

        GameObject bullet = Instantiate(pfbBullet);
        bullet.transform.position = this.transform.position;
        bullet.transform.forward = this.transform.forward;
    }
}
