using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * 0.045f);
        Destroy(this.gameObject, 1);
    }
}
