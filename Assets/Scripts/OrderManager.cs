using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 주문 정보 클래스
public class Order
{
    // 주문한 음식 개수
    public List<CookType> cooks = new List<CookType>();
    public int money;
    public Order(List<CookType> cookPool, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int value = Random.Range(0, 10);
            cooks.Add(cookPool[Random.Range(0, cookPool.Count)]);
            money += 10;
        }
    }
}

public class OrderManager : Singleton<OrderManager>
{
    int minCooks = 3;
    int maxCooks = 5;
    int maxOrderCount = 4;
    int maxWaitSecond = 5;
    List<Order> orderList = new List<Order>();
    WaitForSeconds wait = new WaitForSeconds(1.0f);

    void Update()
    {
        StartCoroutine(UpdateOrder());
    }

    IEnumerator UpdateOrder()
    {
        int waitCount;
        while(true)
        {
            if (orderList.Count < maxOrderCount)
            {
                CreateOrder();
                waitCount = Random.Range(0, maxWaitSecond + 1);
                for (int count = 0; count < waitCount;count++)
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
    void AchieveOrder(Order order)
    {
        Dictionary<CookType, int> targetCount = new Dictionary<CookType, int>();
        for (int index = 0; index < order.cooks.Count; index++)
        {
            if (targetCount.ContainsKey(order.cooks[index]))
            {
                targetCount[order.cooks[index]] += 1;
            }
            else
            {
                targetCount.Add(order.cooks[index], 1);
            }
        }
        if (CanAchieve(CookManager.instance.cookValue, targetCount))
        {
            LevelManager.instance.money += order.money;
            orderList.Remove(order);
        }
    }

    bool CanAchieve(Dictionary<CookType, int> count, Dictionary<CookType, int> target)
    {
        var keys = target.Keys.ToList();
        for (int index = 0; index < target.Count; index++)
        {
            count.TryGetValue(keys[index], out int value);
            if (value < target[keys[index]])
            {
                return false;
            }
        }
        return true;

        //foreach (CookType type in target.Keys)
        //{
        //    count.TryGetValue(type, out int value);
        //    if (value < target[type])
        //    {
        //        return false;
        //    }
        //    count[type] -= target[type];
        //    //if (count.ContainsKey(type))
        //    //{
                
        //    //}
        //}
        //return true;
    }
}
