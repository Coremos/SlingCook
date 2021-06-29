using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum CookType { None = -1, 
    Cupcake, 
    ChocolateDonut, StrawberryDonut, 
    StrawberryCupcake, 
    ChocolateCake, BlueberryCake, LimeCake, StrawberryCake, 
    Count }
//public enum CookType { None = -1, Butter, Apple, Banana, Cat, MilkChoco, Count }

[System.Serializable]
public class Recipes : SerializableDictionary<CookType, List<MaterialType>> { }

public class CookManager : Singleton<CookManager>
{
    public Text text;
    public GameObject canvas;
    public GameObject cookButton;
    public Dictionary<CookType, int> cookValue = new Dictionary<CookType, int>(); // �丮 ����
    public List<CookType> cookPool = new List<CookType>(); // ���� �丮
    //public Dictionary<CookType, List<MaterialType>> recipes = new Dictionary<CookType, List<MaterialType>>(); // ���չ���
    public Dictionary<CookType, Dictionary<MaterialType, int>> recipes = new Dictionary<CookType, Dictionary<MaterialType, int>>(); // ���չ���
    public Vector2 rightBottomPosition;
    List<CookSlot> cookSlots = new List<CookSlot>();

    new void Awake()
    {
        base.Awake();
        cookPool.Clear();
        cookPool.Add(CookType.Cupcake);
        cookPool.Add(CookType.ChocolateDonut);
        cookPool.Add(CookType.ChocolateCake);
        cookPool.Add(CookType.BlueberryCake);
        cookPool.Add(CookType.LimeCake);
        cookPool.Add(CookType.StrawberryCake);

        InitializeCookValue();
        InitializeRecipe();

        for (int i = 0; i < cookPool.Count; i++)
        {
            var button = Instantiate(cookButton);
            button.transform.SetParent(canvas.transform);
            button.GetComponent<CookButton>().type = cookPool[i];
            button.transform.localPosition = new Vector3(rightBottomPosition.x, rightBottomPosition.y + button.GetComponent<RectTransform>().rect.height * i, 0.0f);
        }
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {

        string dummyText = "";
        for (int i = 0; i < cookPool.Count; i++)
        {
            dummyText += cookPool[i] + " * " + cookValue[cookPool[i]] + " ";
        }
        text.text = dummyText;

    }

    void InitializeRecipe()
    {
        recipes.Clear();
        // �丮 ������ �߰��ϱ�
        recipes.Add(CookType.Cupcake, new Dictionary<MaterialType, int> { { MaterialType.Dough, 1 }, { MaterialType.Chocolate, 1 } });
        recipes.Add(CookType.StrawberryCupcake, new Dictionary<MaterialType, int> { { MaterialType.Dough, 1 }, { MaterialType.Strawberry, 1 } });
        recipes.Add(CookType.ChocolateDonut, new Dictionary<MaterialType, int> { { MaterialType.Dough, 2 }, { MaterialType.Chocolate, 3 } });
        recipes.Add(CookType.StrawberryDonut, new Dictionary<MaterialType, int> { { MaterialType.Dough, 2 }, { MaterialType.Strawberry, 1 } });
        recipes.Add(CookType.ChocolateCake, new Dictionary<MaterialType, int> { { MaterialType.Dough, 2 }, { MaterialType.Cream, 2 }, { MaterialType.Chocolate, 2 } });
        recipes.Add(CookType.LimeCake, new Dictionary<MaterialType, int> { { MaterialType.Dough, 2 }, { MaterialType.Cream, 2 }, { MaterialType.Lime, 2 } });
        recipes.Add(CookType.StrawberryCake, new Dictionary<MaterialType, int> { { MaterialType.Dough, 2 }, { MaterialType.Cream, 2 }, { MaterialType.Strawberry, 2 } });
        recipes.Add(CookType.BlueberryCake, new Dictionary<MaterialType, int> { { MaterialType.Dough, 2 }, { MaterialType.Cream, 2 }, { MaterialType.Blueberry, 2 } });
    }

    void InitializeCookValue()
    {
        cookValue.Clear(); // ��� ���� �ʱ�ȭ
        for (int i = 0; i < cookPool.Count; i++) // ������������ ����ϴ� ������ ������ŭ �ݺ�
        {
            cookValue.Add(cookPool[i], 0); // �丮 ������ 0���� �ʱ�ȭ
        }
    }

    void InitializeCookSlot()
    {

    }

    public bool Cook(CookType type)
    {
        if (recipes.ContainsKey(type))
        {
            var materials = recipes[type];
            var materialKeys = materials.Keys.ToList();
            // recipes = new Dictionary<CookType, Dictionary<MaterialType, int>>
            for (int i = 0; i < materialKeys.Count; i++)
            {
                // �������� ��ᰡ �ִ��� Ȯ��
                if (LevelManager.instance.materialValue.ContainsKey(materialKeys[i]))
                {
                    // ���� ��� ������ �������� ��� �������� ������ ��ȯ
                    if (LevelManager.instance.materialValue[materialKeys[i]] < materials[materialKeys[i]])
                    {
                        Debug.LogError(type + "�� " + materialKeys[i] + " ��ᰡ ����");
                        return false;
                    }
                }
                else
                {
                    Debug.LogError(type + " ��ᰡ ����");
                    return false;
                }
            }

            // ���� ��� ���� ����
            for (int i = 0; i < materialKeys.Count; i++)
            {
                LevelManager.instance.materialValue[materialKeys[i]] -= materials[materialKeys[i]];
            }
            cookValue[type] += 1;

            Debug.LogWarning(type + "�丮�� �ϼ��Ǿ����ϴ�." + cookValue[type]);
            return true;
        }
        Debug.LogError(type + " �����ǰ� ����");
        return false;
    }
}
