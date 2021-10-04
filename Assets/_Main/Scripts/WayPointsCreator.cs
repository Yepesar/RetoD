using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsCreator : MonoBehaviour
{
    [SerializeField] private bool tiled = false;
    [Range(0,10)]
    [SerializeField] private int moves;
    [SerializeField] private GameEvent botSelected;
    [SerializeField] private AudioPlayer audioPlayer;

    private Bot actualBot;
    private GameObject routePrefab;
    private bool canRefund = false;

    public Bot ActualBot { get => actualBot; set => actualBot = value; }
    public int Moves { get => moves; set => moves = value; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (moves > 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    botSelected.Raise();

                    if (hit.collider)
                    {
                        Bot b = hit.collider.GetComponent<Bot>();
                        if (b != null)
                        {
                            ActualBot = b;
                            routePrefab = ActualBot.RoutePrefab;
                            b.PlaySelectedSound();
                            
                        }
                        else if (ActualBot)
                        {
                            if (ActualBot.Alive)
                            {
                                if (hit.collider.gameObject.layer == 9)
                                {
                                    Vector3 pos;

                                    if (tiled)
                                    {
                                        pos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.5f, hit.collider.transform.position.z);

                                    }
                                    else
                                    {
                                        pos = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
                                    }

                                    GameObject wayPoint = Instantiate(routePrefab, pos, Quaternion.identity);
                                    ActualBot.SetWayPoint(wayPoint.transform);
                                    Moves -= 1;
                                    canRefund = true;

                                    //Audio
                                    audioPlayer.PlayAudio(1);
                                }
                            }
                        }
                    }
                }
            }          
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (ActualBot)
            {
                if (ActualBot.Alive)
                {
                    ActualBot.RemoveLastPoint();
                    if (canRefund)
                    {
                        Moves += 1;
                        canRefund = false;
                    }
                }               
            }
        }
    }
}
