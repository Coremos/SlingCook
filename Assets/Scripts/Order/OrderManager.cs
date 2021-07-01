using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 주문 정보 클래스
public class Order
{
    // 주문한 음식 개수
    public Dictionary<CookType, int> cooks = new Dictionary<CookType, int>();
    public int money;
    public PeopleMove orderer;
    public Order(PeopleMove people, List<CookType> cookPool, int count)
    {
        orderer = people;
        for (int i = 0; i < count; i++)
        {
            int value = Random.Range(0, 10);
            var type = cookPool[Random.Range(0, cookPool.Count)];
            if (cooks.ContainsKey(type))
            {
                i -= 1;
                continue;
            }
            cooks.Add(type, Random.Range(1, 4));
            money += 10;
        }
    }
}

public class OrderManager : Singleton<OrderManager>
{
    public GameObject canvas;
    public Vector3 offset;
    public GameObject orderSlot;
    List<OrderSlot> orderSlots = new List<OrderSlot>();
    int minCooks = 1;
    int maxCooks = 3;
    int maxOrderCount = 4;
    List<Order> orderList = new List<Order>();

    new void Awake()
    {
        for (int i = 0; i < maxOrderCount; i++)
        {
            var slot = Instantiate(orderSlot);
            slot.transform.SetParent(canvas.transform);
            slot.transform.localPosition = new Vector3(0.0f, 100.0f * i, 0.0f) + offset;
            slot.SetActive(false);
            orderSlots.Add(slot.GetComponent<OrderSlot>());
        }
    }

    // 주문 생성 함수
    public void CreateOrder(PeopleMove orderer)
    {
        var order = new Order(orderer, CookManager.instance.cookPool, Random.Range(minCooks, maxCooks + 1));
        orderList.Add(order);
        for (int i = 0; i < orderSlots.Count; i++)
        {
            if (!orderSlots[i].gameObject.activeInHierarchy)
            {
                orderSlots[i].SetOrder(order);
                break;
            }
        }
    }

    // 주문 달성 함수
    public bool AchieveOrder(Order order)
    {
        var keys = order.cooks.Keys.ToList();
        for (int index = 0; index < keys.Count; index++)
        {
            if (CookManager.instance.cookValue.ContainsKey(keys[index]))
            {
                if (CookManager.instance.cookValue[keys[index]] < order.cooks[keys[index]])
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        for (int index = 0; index < keys.Count; index++)
        {
            CookManager.instance.cookValue[keys[index]] -= order.cooks[keys[index]];
        }
        LevelManager.instance.money += order.money;
        order.orderer.AchieveOrder();
        orderList.Remove(order);
        return true;
    }
}