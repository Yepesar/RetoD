using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private string botName;
    [SerializeField] private float speed;
    [SerializeField] private float stopDistance;
    [SerializeField] private int resistance;
    [SerializeField] private Transform wayPoints;
    [SerializeField] private GameObject routePrefab;
    [SerializeField] private Color nameColor;
    [SerializeField] private BotAnimations animations;
    [SerializeField] private SpriteRenderer faceRenderer;
    [SerializeField] private Sprite deathFace;
    [SerializeField] private int winPoints = 100;
    [SerializeField] private int deathPoints = 50;
    [SerializeField] private GameEvent onAddPoints;
    [SerializeField] private GameEvent onRemovePoints;
    [SerializeField] private GameEvent botComplete;
    [Header("Sounds")]
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private AudioClip selectedBotSound;
    [SerializeField] private AudioClip deathSound;

    private Rigidbody rb;
    private Transform waypoint;
    private bool canMove = false;
    private bool alive = true;
    private int index;
    private List<Vector3> positions = new List<Vector3>();

    public GameObject RoutePrefab { get => routePrefab; set => routePrefab = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool Alive { get => alive; set => alive = value; }
    public int Resistance { get => resistance; set => resistance = value; }
    public Color NameColor { get => nameColor; set => nameColor = value; }
    public string BotName { get => botName; set => botName = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        positions.Add(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }

        animations.OnWalk(canMove);
    }

    public void Move()
    {
        if (waypoint)
        {
            float distance = Vector3.Distance(transform.position, waypoint.position);

            if (distance <= stopDistance)
            {
                GetNewWaypoint();
            }

            transform.position = Vector3.MoveTowards(transform.position,waypoint.position, Time.deltaTime * Speed);
            //transform.rotation = Quaternion.LookRotation(waypoint.position);
            transform.LookAt(waypoint.position);
            animations.OnWalk(true);
        }
    }

    public void SetWayPoint(Transform newWaypoint)
    {
        newWaypoint.parent = wayPoints;
        positions.Add(newWaypoint.position);
        DisplayPosition();
    }

    private void ClearPath()
    {
        LineRenderer lr = wayPoints.GetComponent<LineRenderer>();
        lr.positionCount = 0;
        for (int i = 0; i < wayPoints.childCount; i++)
        {
            Destroy(wayPoints.GetChild(i).gameObject);
            positions.Clear();
        }
    }

    private void ValidateTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.collider)
            {
                Objective o = hit.collider.GetComponentInParent<Objective>();
                if (o != null)
                {
                    o.OnComplete();
                    onAddPoints.Raise(winPoints);
                    audioPlayer.PlayAudio(2);
                }
            }
        }

        botComplete.Raise();
    }

    public void RemoveLastPoint()
    {
        if (wayPoints.childCount > 0)
        {
            positions.Remove(positions[positions.Count - 1]);
            Destroy(wayPoints.GetChild(wayPoints.childCount - 1).gameObject);
            LineRenderer lr = wayPoints.GetComponent<LineRenderer>();
            lr.positionCount = wayPoints.childCount;
        }      
    }

    private void DisplayPosition()
    {
        if (wayPoints.childCount > 0)
        {
            LineRenderer lr = wayPoints.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            lr.positionCount = wayPoints.childCount + 1;
            for (int i = 0; i < positions.Count; i++)
            {
                lr.SetPosition(i, positions[i]);
            }
        }        
    }

    [ContextMenu("Start Bot")]
    public void Play()
    {
        if (wayPoints.childCount> 0)
        {
            canMove = true;
            Transform w = wayPoints.GetChild(0);
            if (w != null)
            {
                waypoint = w;
            }
        }   
        else
        {
            botComplete.Raise();
        }
    }

    public void PlaySelectedSound()
    {
        audioPlayer.PlayAudio(selectedBotSound);
    }

    private void GetNewWaypoint()
    {
        index++;
        if (index <= wayPoints.childCount - 1)
        {
            Transform w = wayPoints.GetChild(index);
            waypoint = w;
        }      
        else
        {
            canMove = false;
            alive = false;
            ClearPath();
            ValidateTile();
        }
    }

    private void AchieveObjective()
    {
        canMove = false;
    }

    private void Deactivate(Vector3 direction,float hitSpeed)
    {
        canMove = false;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
        Vector3 hitDirection = (transform.position - direction) + Vector3.right + Vector3.back + Vector3.left ;
        rb.AddForce(hitDirection * (hitSpeed), ForceMode.Impulse);
        alive = false;
        ClearPath();
        faceRenderer.sprite = deathFace;
        onRemovePoints.Raise(deathPoints);
        botComplete.Raise();
        audioPlayer.PlayAudio(deathSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alive)
        {
            if (collision.gameObject.layer == 10) // prop hit
            {
                Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();
                if (otherRB)
                {
                    otherRB.AddForce((otherRB.position + Vector3.back), ForceMode.Impulse);
                }

                Resistance -= 1;
                if (Resistance <= 0)
                {
                    Deactivate(collision.transform.position, speed / 2);
                }
            }
            else if (collision.gameObject.layer == 7) // other bots
            {
                Bot b = collision.gameObject.GetComponent<Bot>();
                Deactivate(collision.transform.position, b.speed);
            }
        }       
    }
}
