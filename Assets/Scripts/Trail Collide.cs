using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrailCollide : MonoBehaviour
{
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           gameObject.SetActive(true);
        }
    }
}
