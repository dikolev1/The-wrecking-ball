using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ball : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float boostSeconds;

    public GameObject ContactParticles;
    public GameObject BoostParticles;

    public AudioClip PongSound;
    public AudioClip BoostSound;

    private float _defaultSpeed;

    private AudioSource _audioSource;
    public Vector2 _direction;
    private Tilemap _tilemap;
    private LevelInfo _levelInfo;

    private void Awake()
    {
        EventManager.OnWin.AddListener(StopBall);
        EventManager.OnFail.AddListener(StopBall);

        _tilemap = FindObjectOfType<Tilemap>();
        _levelInfo = FindObjectOfType<LevelInfo>();
        _audioSource = GetComponent<AudioSource>();
        _defaultSpeed = speed;
    }
    private void Start()
    {
        NewAngle();
        //_direction = new Vector2(-1, 1).normalized;
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        _direction = Vector2.Reflect(_direction, normal);

        _audioSource.PlayOneShot(PongSound);
        Instantiate(ContactParticles, transform.position, transform.rotation);

        Vector2 collisionPoint = collision.contacts[0].point;
        Vector3Int tilePosition = _tilemap.WorldToCell(collisionPoint);
        
        TileBase tile = _tilemap.GetTile(tilePosition);
        if (tile != null)
        {
            if (tile is Block block)
            {
                block.TakeDamage(_tilemap, tilePosition);
                if (block.canBoost)
                    BoostSpeed();
                if (block.canKill)
                    _levelInfo.Fail();
                if (!block.IsImortal)
                    _levelInfo.BlockDestroyed();
            }
        }
    }

    private void OnEnable()
    {
        NewAngle();
    }

    public void NewAngle()
    {
        float radians = Random.Range(30f, 150f) * Mathf.Deg2Rad;
        _direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    private void BoostSpeed()
    {
        speed = maxSpeed;

        _audioSource.clip = BoostSound;
        _audioSource.PlayOneShot(BoostSound);

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Instantiate(BoostParticles, transform.position, Quaternion.Euler(0, 0, angle));

        StopAllCoroutines();
        StartCoroutine(BoostTimer());
    }
    private IEnumerator BoostTimer()
    {
        yield return new WaitForSeconds(boostSeconds);
        speed = _defaultSpeed;
    }

    private void StopBall()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        enabled = false;
    }
}
