using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Camera cam;


    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AfterScene()
    {
        if (BrickController.brickNumbers <= 0)
        {
        }
    }

    public void Retry()
    {
        
    }
}