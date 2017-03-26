using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_ball : MonoBehaviour
{

    // Use this for initialization
    private Vector3 ball_rear;
    public float pulling_range;
    private float dis;
    private Vector3 initalPosition;
    private Vector3 initPos;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody2D rigid;
    static public bool holding = false;
    static public bool shot = false;
    public static byte life = 3;
    public static bool game_over = false;
    private AudioSource throw_sound;
    void Start()
    {
        throw_sound = GetComponent<AudioSource>();
        shot = false;
        holding = false;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        initPos = new Vector3(-5.43f, 1.23f, 0);
        initalPosition = Camera.main.WorldToScreenPoint(initPos);
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && holding && !game_over)
        {
            rigid.AddForce((initPos - transform.position) * 300f);
            rigid.gravityScale = 1;
            holding = false;
            shot = true;
            life--;
            throw_sound.Play();
        }
    }

    void OnMouseDown()
    {
        holding = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        dis = Mathf.Sqrt(Mathf.Pow(Input.mousePosition.x - initalPosition.x, 2) + Mathf.Pow(Input.mousePosition.y - initalPosition.y, 2));
        if (dis < pulling_range)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        else
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x * pulling_range / dis + initalPosition.x * (1 - pulling_range / dis),
                Input.mousePosition.y * pulling_range / dis + initalPosition.y * (1 - pulling_range / dis), screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            transform.position = curPosition;
        }
    }
}
