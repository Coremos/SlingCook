using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookType { None = -1, Butter, Apple, Banana, Cat, Count }

public class Recipe
{
    List<CookMaterial> materials = new List<CookMaterial>();
    CookType type;
}

public class CookManager : MonoBehaviour
{
    public Dictionary<CookType, int> cookValue = new Dictionary<CookType, int>(); // ��� ����
    Dictionary<CookType, List<MaterialType>> recipes = new Dictionary<CookType, List<MaterialType>>();
    public List<CookType> cookPool = new List<CookType>(); // ���� ���
    private void Awake()
    {

    }

    void InitializeRecipe()
    {
        for (int index = 0; index < (int)CookType.Count; index++)
        {
            recipes.Add((CookType)index, new List<MaterialType>());
        }

    }

    void InitializeCookValue()
    {
        cookValue.Clear(); // ��� ���� �ʱ�ȭ
        for (int i = 0; i < cookPool.Count; i++) // ������������ ����ϴ� ������ ������ŭ �ݺ�
        {
            cookValue.Add(cookPool[i], 0); // �丮 ������ 0���� �ʱ�ȭ
        }
    }

    void Cook(CookType type)
    {

    }
}