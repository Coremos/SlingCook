using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CookButton : MonoBehaviour
{
    public Text text;
    public CookType type;

    public void Start()
    {
        text.text = type.ToString() + "\n";
        var recipes = CookManager.instance.recipes[type].ToList();
        for (int index = 0; index < recipes.Count; index++)
        {
            text.text += recipes[index] + " ";
        }
    }

    public void Cook()
    {
        CookManager.instance.Cook(type);
    }
}
