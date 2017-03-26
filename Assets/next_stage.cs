using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next_stage : MonoBehaviour
{
    public GameObject bird;
    public static bool clicked = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bird.transform.position = new Vector3(-0.57f, 2.96f, 0);
            Instantiate(bird);
            bird.transform.position = new Vector3(4.09f, 2.96f, 0);
            Instantiate(bird);
            bird.transform.position = new Vector3(9.3f, 2.96f, 0);
            Instantiate(bird);
            Destroy(gameObject);
            clicked = true;
        }
    }
}
