using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class peoplemove : MonoBehaviour
{
    float run = 0.01f; //달리기속도
    Vector3 rotation = new Vector3(0, 0, 0); //테이블바라보는 방향
    int waypointIndex = 0;
    enum State { None, Moving, MoveToOrder, Order, Exit } //npc의 상태
    State state; //현재 상태
    float position; //위치 
    private void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        gameObject.SetActive(true);
        waypointIndex = 0;
        transform.position = waypointManagiment.instance.waypointPosition[0];
        waypointManagiment.instance.isOnWaypoint[0] = true;
        state = State.None;
    }

    private void Update()
    {
        if (state == State.None) //보통상태
        {

            if (waypointIndex == waypointManagiment.instance.waypointPosition.Count - 1)
            {
                waypointManagiment.instance.isOnWaypoint[waypointManagiment.instance.waypointPosition.Count - 1] = false;
                for (int i = 0; i < waypointManagiment.instance.isOnOrderpoint.Count; i++)
                {
                    // 해당 발판이 비어있을 때
                    if (!waypointManagiment.instance.isOnOrderpoint[i])
                    {
                        waypointManagiment.instance.isOnOrderpoint[i] = true;
                        waypointIndex = i;
                        state = State.MoveToOrder;
                        position = 0;
                        break;
                    }
                }
            }
            else
            {
                if (!waypointManagiment.instance.isOnWaypoint[waypointIndex + 1])
                {
                    waypointManagiment.instance.isOnWaypoint[waypointIndex++] = false;
                    waypointManagiment.instance.isOnWaypoint[waypointIndex] = true;
                    state = State.Moving;
                    position = 0;
                }
            }
        }
        else if (state == State.Moving) //움직일때
        {
            if (Vector3.Distance(transform.position, waypointManagiment.instance.waypointPosition[waypointIndex]) < 0.1f)
            {
                state = State.None;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, waypointManagiment.instance.waypointPosition[waypointIndex], position);
                position += Time.deltaTime * run;
               
            }

        }
        else if (state == State.MoveToOrder) //주문받으러갈때
        {
            
            if (Vector3.Distance(transform.position, waypointManagiment.instance.orderpointPosition[waypointIndex]) < 0.1f)
            {
                state = State.Order;
                StartCoroutine("rotationpeople");
            }
            else
            {
                
                transform.position = Vector3.MoveTowards(transform.position, waypointManagiment.instance.orderpointPosition[waypointIndex], position);
                position += Time.deltaTime * run;
            }
        }
        else if (state == State.Order) //주문 할때
        {

           // state = State.Exit;
        }
        else if (state == State.Exit) //음식점에서 나갈때
        {
            if (Vector3.Distance(transform.position, waypointManagiment.instance.exitpointPosition[waypointIndex]) < 0.1f)
            {
                if(waypointIndex == waypointManagiment.instance.exitpointPosition.Count - 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, waypointManagiment.instance.exitpointPosition[waypointIndex], position);
                position += Time.deltaTime * run;
                
            }
        }

    }
    IEnumerator rotationpeople()
    {
        while(transform.rotation != Quaternion.Euler(rotation))
        {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation), 3.5f * Time.deltaTime);

        yield return null;
        }
            
    }
}

