using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_dead : MonoBehaviour
{
    public ParticleSystem ps;
    public static byte bird_num = 0;
    public static byte stage = 1;
    public GameObject next;
    public GameObject you_win;
    public static bool win = false;
    private AudioSource[] hit_sound;
    // Use this for initialization
    void Start()
    {
        hit_sound = GetComponents<AudioSource>();
        bird_num++;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -5)
        {
            StartCoroutine(die());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("bird"))
        {
            hit_sound[0].Play();
            StartCoroutine(die());
            StartCoroutine(die_sound());
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(2);
        
        ps.transform.position = transform.position + new Vector3(0, 0, -0.2f);
        Renderer r = ps.GetComponent<Renderer>();
        r.sortingLayerName = "ball";
        gameObject.SetActive(false);
        bird_num--;

        Instantiate(ps);
        if (bird_num == 0)
        {
            if (stage < 2)
            {
                Instantiate(next);
                stage++;
            }
            else
            {
                Instantiate(you_win);
                win = true;
                drag_ball.game_over = true;
            }
        }
    }
    IEnumerator die_sound()
    {
        yield return new WaitForSeconds(1);
        hit_sound[1].Play();
    }
}
