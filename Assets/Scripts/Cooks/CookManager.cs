using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CookType { None = -1, Butter, Apple, Banana, Cat, MilkChoco, Count }

[System.Serializable]
public class Recipes : SerializableDictionary<CookType, List<MaterialType>> { }

public class CookManager : Singleton<CookManager>
{
    public GameObject canvas;
    public GameObject cookButton;
    public Dictionary<CookType, int> cookValue = new Dictionary<CookType, int>(); // �丮 ����
    public List<CookType> cookPool = new List<CookType>(); // ���� �丮
    //public Dictionary<CookType, List<MaterialType>> recipes = new Dictionary<CookType, List<MaterialType>>(); // ���չ���
    public Dictionary<CookType, Dictionary<MaterialType, int>> recipes = new Dictionary<CookType, Dictionary<MaterialType, int>>(); // ���չ���
    public Vector2 rightBottomPosition;

    new void Awake()
    {
        base.Awake();
        cookPool.Add(CookType.Apple);
        cookPool.Add(CookType.Banana);
        cookPool.Add(CookType.Cat);
        cookPool.Add(CookType.MilkChoco);

        InitializeCookValue();
        InitializeRecipe();

        for (int i = 0; i < cookPool.Count; i++)
        {
            var button = Instantiate(cookButton);
            button.transform.parent = canvas.transform;
            button.GetComponent<CookButton>().type = cookPool[i];
            button.transform.localPosition = new Vector3(rightBottomPosition.x, rightBottomPosition.y + button.GetComponent<RectTransform>().rect.height * i, 0.0f);
        }
    }

    void InitializeRecipe()
    {
        //for (int index = 0; index < (int)CookType.Count; index++)
        //{
        //    recipes.Add((CookType)index, null);
        //}
        
        //recipes[CookType.Apple].AddRange(new List<MaterialType> { MaterialType.Butter, MaterialType.Cheese });
        
        // �丮 �߰��ϱ�
        recipes.Add(CookType.Apple, new Dictionary<MaterialType, int> { { MaterialType.Butter, 2 }, { MaterialType.Cheese, 1 } });
        recipes.Add(CookType.Banana, new Dictionary<MaterialType, int> { { MaterialType.Chocolate, 5 }, { MaterialType.Flour, 3 } });
        recipes.Add(CookType.Cat, new Dictionary<MaterialType, int> { { MaterialType.Butter, 2 }, { MaterialType.Cheese, 1 } });
        recipes.Add(CookType.MilkChoco, new Dictionary<MaterialType, int> { { MaterialType.Butter, 2 }, { MaterialType.Cheese, 1 } });
    }

    void InitializeCookValue()
    {
        cookValue.Clear(); // ��� ���� �ʱ�ȭ
        for (int i = 0; i < cookPool.Count; i++) // ������������ ����ϴ� ������ ������ŭ �ݺ�
        {
            cookValue.Add(cookPool[i], 0); // �丮 ������ 0���� �ʱ�ȭ
        }
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
