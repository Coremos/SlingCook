using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linear : Singleton<Linear>
{
    public List<bool> isOnWaypoint = new List<bool>();
    public List<Vector3> waypointPosition = new List<Vector3>();
}

public class People : MonoBehaviour
{
    int waypointIndex = 0;
    enum State { None, Moving, Order }
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
            if (!Linear.instance.isOnWaypoint[waypointIndex + 1])
            {
                Linear.instance.isOnWaypoint[waypointIndex++] = false;
                Linear.instance.isOnWaypoint[waypointIndex] = true;
                state = State.Moving;
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
        
    }
}