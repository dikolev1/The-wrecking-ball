using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool canCatch;

    public float minX;
    public float maxX;

    public float radius;
    public float height;

    public float speed;

    private Ball ball;

    private void Awake()
    {
        EventManager.OnWin.AddListener(() => enabled = false);
        EventManager.OnFail.AddListener(() => enabled = false);

        ball = FindObjectOfType<Ball>();
    }
    private void Start()
    {
        radius = Mathf.Abs(radius); // На случай отрицательного радиуса
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("IsMouse", 1) == 0)
        {
            float moving = Input.GetAxis("Horizontal");
            transform.position += new Vector3(moving * speed, 0, 0) * Time.deltaTime;

            if (transform.position.x > maxX - radius)
            {
                transform.position = new Vector3(maxX - radius, height, 0);
            }
            else if (transform.position.x < minX + radius)
            {
                transform.position = new Vector3(minX + radius, height, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ball.transform.SetParent(null);
                ball.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                canCatch = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                canCatch = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                ball.transform.SetParent(null);
                ball.enabled = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                canCatch = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                canCatch = false;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0;
            mousePosition.y = height;

            if (mousePosition.x > maxX - radius)
            {
                mousePosition.x = maxX - radius;
            }
            else if (mousePosition.x < minX + radius)
            {
                mousePosition.x = minX + radius;
            }

            transform.position = mousePosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball") || !canCatch || !ball.enabled)
        {
            return;
        }

        BallCatched();
    }

    public void BallCatched()
    {
        ball.enabled = false;
        ball.transform.SetParent(transform);
    }
}
