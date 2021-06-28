using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookType { None = -1, Butter, Apple, Banana, Cat, Count }

[System.Serializable]
public class Recipes : SerializableDictionary<CookType, List<MaterialType>> { }

public class CookManager : MonoBehaviour
{
    public Dictionary<CookType, int> cookValue = new Dictionary<CookType, int>(); // 요리 개수
    public List<CookType> cookPool = new List<CookType>(); // 등장 요리
    public Dictionary<CookType, List<MaterialType>> recipes = new Dictionary<CookType, List<MaterialType>>(); // 조합법들

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
