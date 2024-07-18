using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemykill : MonoBehaviour
{
    public bool collided;
    public GameObject gameoverpanel;
    
    // Start is called before the first frame update
    void Start()
    {
        gameoverpanel.SetActive(false);
    }
  // Update is called once per frame
    void Update()
    {
     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Destroy(gameObject, 3f);
            gameoverpanel.SetActive(true);
        }     
    }



}
