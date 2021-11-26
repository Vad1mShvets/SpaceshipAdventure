using UnityEngine;

public class ClickOnArrow : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<Arrow>() != null)
                    {
                        hit.collider.GetComponent<Arrow>().TeleportToTarget();
                    }
                    if (hit.collider.GetComponent<ArrowWithSort>() != null)
                    {
                        hit.collider.GetComponent<ArrowWithSort>().TeleportToTarget();
                    }
                }
            }
        }
    }
}
