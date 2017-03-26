using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_dead : MonoBehaviour
{
    private Vector3 stick_front = new Vector3(-5.9f, 1.35f, 0);
    private Vector3 stick_back = new Vector3(-4.89f, 1.46f, 0);
    public GameObject ball;
    public GameObject prefab;
    private Vector3 initPos;
    private GameObject myLine;
    private GameObject myLine2;
    private Vector3 ball_rear;
    private float line_offset = 0.8f;
    private float dis;
    private float dis2;
    private Vector3 cam_initPos;
    public GameObject fig1;
    public GameObject fig2;
    private bool waited = false;
    private int counter = 0;
    public GameObject game_over;
    // Use this for initialization
    void Start()
    {
        cam_initPos = transform.position;
        initPos = new Vector3(-5.43f, 1.23f, 0);
        dis2 = Mathf.Sqrt(Mathf.Pow(initPos.x - stick_front.x, 2) + Mathf.Pow(initPos.y - stick_front.y, 2));
        myLine = new GameObject();
        myLine2 = new GameObject();
        myLine.AddComponent<LineRenderer>();
        myLine2.AddComponent<LineRenderer>();
        ball_rear = (initPos - stick_front) / dis2 * (dis2 + line_offset) + stick_front;
    }

    // Update is called once per frame
    void Update()
    {
        if (drag_ball.holding)
        {
            dis2 = Mathf.Sqrt(Mathf.Pow(ball.transform.position.x - stick_front.x, 2) + Mathf.Pow(ball.transform.position.y - stick_front.y, 2));
            ball_rear = (ball.transform.position - stick_front) / dis2 * (dis2 + line_offset) + stick_front;
        }
        if (drag_ball.shot)
        {
            if(next_stage.clicked && waited)
            {
                next_stage.clicked = false;
                waited = false;
            }
            
            counter++;
            if (counter > 480)
            {
                waited = true;
                counter = 0;
            }
            transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, -10);
            ball_rear = (initPos - stick_front) / dis2 * (dis2 + line_offset) + stick_front;
            if (ball.transform.position.y < -5 || waited || next_stage.clicked)
            {
                StartCoroutine("RespwanBall");
            }
        }
        DrawLineFront(ball_rear, stick_front, Color.black);
        DrawLineBack(ball_rear, stick_back, Color.black);
    }

    IEnumerator RespwanBall()
    {
        Destroy(ball.gameObject);
        ball = Instantiate(prefab);
        transform.position = cam_initPos;
        if (drag_ball.life == 2)
        {
            Destroy(fig1);
        }
        if (drag_ball.life == 1)
        {
            Destroy(fig2);
        }

        if (drag_ball.life == 0 && !bird_dead.win)
        {
            Instantiate(game_over);
            drag_ball.game_over = true;
        }
        next_stage.clicked = false;
        counter = 0;
        yield return null;
    }

    void DrawLineFront(Vector3 start, Vector3 end, Color color)
    {
        myLine.transform.position = start;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.sortingLayerName = "rubberband front";
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.3f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void DrawLineBack(Vector3 start, Vector3 end, Color color)
    {
        myLine2.transform.position = start;
        LineRenderer lr = myLine2.GetComponent<LineRenderer>();
        lr.sortingLayerName = "rubberband back";
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.2f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
