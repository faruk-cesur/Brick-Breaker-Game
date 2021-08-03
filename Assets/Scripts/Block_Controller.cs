using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Controller : MonoBehaviour
{
    public int healt;
    public static int blockNumbers;
    public bool breakingBlocks;
    private Game_Manager gm;
    void Start()
    {
        breakingBlocks = (this.tag == "Block");
        if(breakingBlocks) {
            blockNumbers++;
        }

        gm = GameObject.FindObjectOfType<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D col) {
        healt -= 1;
        if(healt <= 0) {
            blockNumbers -= 1;
            Destroy(gameObject);
            gm.afterScene();
        }
    }
}
