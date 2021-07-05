using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class peoplemove : MonoBehaviour
{
    float run = 0.01f; //�޸���ӵ�
    Vector3 rotation = new Vector3(0, 0, 0); //���̺�ٶ󺸴� ����
    int waypointIndex = 0;
    enum State { None, Moving, MoveToOrder, Order, Exit } //npc�� ����
    State state; //���� ����
    float position; //��ġ 
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
        if (state == State.None) //�������
        {

            if (waypointIndex == waypointManagiment.instance.waypointPosition.Count - 1)
            {
                waypointManagiment.instance.isOnWaypoint[waypointManagiment.instance.waypointPosition.Count - 1] = false;
                for (int i = 0; i < waypointManagiment.instance.isOnOrderpoint.Count; i++)
                {
                    // �ش� ������ ������� ��
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
        else if (state == State.Moving) //�����϶�
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
        else if (state == State.MoveToOrder) //�ֹ�����������
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
        else if (state == State.Order) //�ֹ� �Ҷ�
        {

           // state = State.Exit;
        }
        else if (state == State.Exit) //���������� ������
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

