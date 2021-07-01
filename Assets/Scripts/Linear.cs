using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linear : Singleton<Linear>
{
    public List<bool> isOnWaypoint = new List<bool>();
    public List<Vector3> waypointPosition = new List<Vector3>();
    public GameObject positionGameObject;


    public GameObject orderPosition;
    public List<bool> isOnOrderpoint = new List<bool>();
    public List<Vector3> orderpointPosition = new List<Vector3>();

    private new void Awake()
    {
        var count = positionGameObject.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            isOnWaypoint.Add(false);
            waypointPosition.Add(positionGameObject.transform.GetChild(i).transform.position);
        }

        count = orderPosition.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            isOnOrderpoint.Add(false);
            orderpointPosition.Add(orderPosition.transform.GetChild(i).transform.position);
        }
    }
}

public class PeopleTest : MonoBehaviour
{
    int waypointIndex = 0;
    enum State { None, Moving, MoveToOrder, Order, Exit }
    State state;
    float position;

    void Spawn()
    {
        gameObject.SetActive(true);
        waypointIndex = 0;
        transform.position = Linear.instance.waypointPosition[0];
        Linear.instance.isOnWaypoint[0] = true;
        state = State.None;
    }

    private void Update()
    {
        if (state == State.None)
        {
            if (waypointIndex == Linear.instance.waypointPosition.Count - 1)
            {
                
                for (int i = 0; i < Linear.instance.isOnOrderpoint.Count; i++)
                {
                    // 해당 발판이 비어있을 때
                    if (!Linear.instance.isOnOrderpoint[i])
                    {
                        Linear.instance.isOnOrderpoint[i] = true;
                        waypointIndex = i;
                        state = State.MoveToOrder;
                        break;
                    }
                }
            }
            else
            {
                if (!Linear.instance.isOnWaypoint[waypointIndex + 1])
                {
                    Linear.instance.isOnWaypoint[waypointIndex++] = false;
                    Linear.instance.isOnWaypoint[waypointIndex] = true;
                    state = State.Moving;
                }
            }
        }
        else if (state == State.Moving)
        {
            if (Vector3.Distance(transform.position, Linear.instance.waypointPosition[waypointIndex]) < 1.0f)
            {
                state = State.None;
                
            }
            else
            {
                Vector3.Lerp(transform.position, Linear.instance.waypointPosition[waypointIndex], position);
                position += Time.deltaTime;
            }
            
        }
        else if (state == State.MoveToOrder)
        {
            if (Vector3.Distance(transform.position, Linear.instance.orderpointPosition[waypointIndex]) < 1.0f)
            {
                state = State.Order;
            }
            else
            {
                Vector3.Lerp(transform.position, Linear.instance.orderpointPosition[waypointIndex], position);
                position += Time.deltaTime;
            }
        }
        
    }
}