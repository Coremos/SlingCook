using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointManagiment : MonoBehaviour
{
    public static waypointManagiment instance;
    public List<bool> isOnWaypoint = new List<bool>(); //웨이포인트가 비어있는지
    public List<Vector3> waypointPosition = new List<Vector3>(); //웨이포인트포지션의 벡터값을 리스트화
    public GameObject positionGameObject; //음식점까지 줄서는 웨이포인트의 부모


    public GameObject orderPosition; //주문할때 웨이포인트
    public List<bool> isOnOrderpoint = new List<bool>(); 
    public List<Vector3> orderpointPosition = new List<Vector3>();

    public GameObject exitPosition;  //주문이 끝나고 나갈때의 웨이포인트
    public List<bool> isOnExitpoint = new List<bool>(); 
    public List<Vector3> exitpointPosition = new List<Vector3>();

    private void Awake()
    {
        instance = this;
        var count = positionGameObject.transform.childCount; //웨이포인트의 자식 벡터값을 저장
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
