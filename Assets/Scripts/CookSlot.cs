using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SlotState { None, Cook, Done, OverCook, Count }
public class CookSlot : MonoBehaviour
{
    SlotState state;
    CookType cookType;
    float currentTime;
    float time;
    Text text;
    public Image gauge;
    public GameObject button;
    Coroutine coroutine;

    void Awake()
    {
        cookType = CookType.None;
        state = SlotState.None;
        gauge.gameObject.SetActive(false);
    }

    public void StartCook(CookType type)
    {
        cookType = type;
        coroutine = StartCoroutine(Cook());
        gauge.gameObject.SetActive(true);
        state = SlotState.Cook;
    }

    void OnClick()
    {
        if (state == SlotState.None)
        {

        }
        else if (state == SlotState.Cook)
        {

        }
        else if (state == SlotState.Done)
        {
            state = SlotState.None;
            CookManager.instance.cookValue[cookType] += 1;
            cookType = CookType.None;
        }
        else if (state == SlotState.OverCook)
        {
            state = SlotState.None;

        }
    }

    void FixedUpdate()
    {
        
    }

    IEnumerator Cook()
    {
        currentTime = 0.0f;
        var value = 1.0f / time;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            gauge.fillAmount += currentTime;
            yield return null;
        }
        yield return null;
    }
}
