using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointManagiment : MonoBehaviour
{
    public static waypointManagiment instance;
    public List<bool> isOnWaypoint = new List<bool>(); //��������Ʈ�� ����ִ���
    public List<Vector3> waypointPosition = new List<Vector3>(); //��������Ʈ�������� ���Ͱ��� ����Ʈȭ
    public GameObject positionGameObject; //���������� �ټ��� ��������Ʈ�� �θ�


    public GameObject orderPosition; //�ֹ��Ҷ� ��������Ʈ
    public List<bool> isOnOrderpoint = new List<bool>(); 
    public List<Vector3> orderpointPosition = new List<Vector3>();

    public GameObject exitPosition;  //�ֹ��� ������ �������� ��������Ʈ
    public List<bool> isOnExitpoint = new List<bool>(); 
    public List<Vector3> exitpointPosition = new List<Vector3>();

    private void Awake()
    {
        instance = this;
        var count = positionGameObject.transform.childCount; //��������Ʈ�� �ڽ� ���Ͱ��� ����
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
        count = exitPosition.transform.childCount;
        for (int i = 0; i<count; i++)
        {
            isOnExitpoint.Add(false);
            exitpointPosition.Add(exitPosition.transform.GetChild(i).transform.position);
        }
    }
}
