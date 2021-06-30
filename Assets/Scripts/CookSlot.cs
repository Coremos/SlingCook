using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SlotState { None, Cook, Done, OverCook, Count }
public class CookSlot : MonoBehaviour
{
    public SlotState state
    {
        get;
        private set;
    }

    CookType cookType;
    float currentTime;
    float time = 10;
    Text text;
    GameObject button;
    Image image;
    Image gauge;
    GameObject model;
    Coroutine coroutine;

    void Awake()
    {
        button = transform.GetChild(0).gameObject;
        image = transform.GetChild(1).gameObject.GetComponent<Image>();
        gauge = transform.GetChild(2).gameObject.GetComponent<Image>();

        cookType = CookType.None;
        state = SlotState.None;
        SetActive(false);
    }

    void SetActive(bool value)
    {
        button.SetActive(value);
        image.gameObject.SetActive(value);
        gauge.gameObject.SetActive(value);
    }

    public void StartCook(CookType type)
    {
        cookType = type;
        gauge.fillAmount = 0.0f;
        SetActive(true);
        state = SlotState.Cook;
        coroutine = StartCoroutine(Cook());
    }

    public void OnClick()
    {
        if (state == SlotState.None)
        {

        }
        else if (state == SlotState.Cook)
        {

        }
        else if (state == SlotState.Done)
        {
            CookManager.instance.cookValue[cookType] += 1;
        }
        else if (state == SlotState.OverCook)
        {

        }
        cookType = CookType.None;
        state = SlotState.None;
        SetActive(false);
        StopCoroutine(coroutine);
    }

    IEnumerator Cook()
    {
        currentTime = 0.0f;
        var value = 1.0f / time;
        image.color = Color.white;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            gauge.fillAmount = currentTime * value;
            yield return null;
        }
        state = SlotState.Done;
        image.color = Color.green;

        currentTime = 0.0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        state = SlotState.OverCook;
        image.color = Color.red;
        yield return null;
    }
}