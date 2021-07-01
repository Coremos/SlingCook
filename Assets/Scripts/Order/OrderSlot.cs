using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderSlot : MonoBehaviour
{
    public Order order;
    public Text text;

    public void SetOrder(Order order)
    {
        this.order = order;
        var keys = order.cooks.Keys.ToList();
        text.text = "";
        for (int i = 0; i < keys.Count; i++)
        {
            text.text += keys[i] + " " + order.cooks[keys[i]] + "\n";
        }
        gameObject.SetActive(true);
    }

    public void SubmitOrder()
    {
        if (OrderManager.instance.AchieveOrder(order))
        {
            gameObject.SetActive(false);
        }
    }
}
