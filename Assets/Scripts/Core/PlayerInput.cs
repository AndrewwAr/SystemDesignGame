using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Card card = hit.collider.GetComponent<Card>();
                if (card != null)
                {
                   
                }
            }
        }
    }
}