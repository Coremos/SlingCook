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
    public Order(List<CookType> cookPool, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int value = Random.Range(0, 10);
            cooks.Add(cookPool[Random.Range(0, cookPool.Count)], Random.Range(1, 4));
            money += 10;
        }
    }
}

public class OrderManager : Singleton<OrderManager>
{
    public GameObject orderSlot;
    List<GameObject> orderSlots = new List<GameObject>();
    int minCooks = 3;
    int maxCooks = 5;
    int maxOrderCount = 4;
    int maxWaitSecond = 5;
    List<Order> orderList = new List<Order>();
    WaitForSeconds wait = new WaitForSeconds(1.0f);

    new void Awake()
    {
        for (int i = 0; i < maxOrderCount; i++)
        {
            var slot = Instantiate(orderSlot);
            slot.transform.SetParent(transform);
            orderSlots.Add(slot);
        }
    }

    void Update()
    {
        StartCoroutine(UpdateOrder());
    }

    IEnumerator UpdateOrder()
    {
        int waitCount;
        while (true)
        {
            if (orderList.Count < maxOrderCount)
            {
                CreateOrder();
                waitCount = Random.Range(0, maxWaitSecond + 1);
                for (int count = 0; count < waitCount; count++)
                {
                    yield return wait;
                }
            }
        }
    }

    // 주문 생성 함수
    void CreateOrder()
    {
        orderList.Add(new Order(CookManager.instance.cookPool, Random.Range(minCooks, maxCooks + 1)));
    }

    // 주문 달성 함수
    bool AchieveOrder(Order order)
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
        orderList.Remove(order);
        return true;
    }
}