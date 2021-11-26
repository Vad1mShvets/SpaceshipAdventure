using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private void Update()
    {
        this.transform.Rotate(new Vector3(0.05f, 0.1f, 0.025f));
    }
}