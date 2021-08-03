using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Controller : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            transform.position.y,
            transform.position.z
        ));

        mousePos.x = Mathf.Clamp(mousePos.x, -2.09f, 2.09f);
        
        transform.position = new Vector3(
            mousePos.x,
            transform.position.y,
            transform.position.z
        );
    }
}
