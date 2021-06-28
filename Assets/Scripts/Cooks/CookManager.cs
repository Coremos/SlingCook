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
    public Dictionary<CookType, int> cookValue = new Dictionary<CookType, int>(); // 재료 개수
    Dictionary<CookType, List<MaterialType>> recipes = new Dictionary<CookType, List<MaterialType>>();
    public List<CookType> cookPool = new List<CookType>(); // 등장 재료
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
        cookValue.Clear(); // 재료 개수 초기화
        for (int i = 0; i < cookPool.Count; i++) // 스테이지에서 사용하는 재료들의 종류만큼 반복
        {
            cookValue.Add(cookPool[i], 0); // 요리 개수를 0으로 초기화
        }
    }

    void Cook(CookType type)
    {

    }
}
