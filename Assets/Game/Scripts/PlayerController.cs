using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        GameManager.cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos =
            GameManager.cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y,
                transform.position.z));

        mousePos.x = Mathf.Clamp(mousePos.x, -2f, 2f);

        transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
    }
}