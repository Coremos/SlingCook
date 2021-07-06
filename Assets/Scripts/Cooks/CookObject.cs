using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookObject : MonoBehaviour
{
    CookType type;

    void Cook()
    {
        CookManager.instance.Cook(type);
    }
}
