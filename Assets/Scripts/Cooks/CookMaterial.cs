using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum MaterialType { Butter, Chocolate, Cheese, Egg, Flour, Milk, Count };
public enum MaterialType { Blueberry, Chocolate, Cream, Dough, Lime, Strawberry, Count };

[System.Serializable]
public class Materials : SerializableDictionary<MaterialType, Material> { }

public class CookMaterial : MonoBehaviour
{
    public Materials materials;
    public MaterialType type;
    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Initialize();
    }

    void Initialize()
    {
        //type = (MaterialType)Random.Range(0, (int)MaterialType.Count);
        // 재료의 종류를 현재 레벨의 재료 종류에서 랜덤으로 가져옴
        type = LevelManager.instance.materialPool[Random.Range(0, LevelManager.instance.materialPool.Count)];
        meshRenderer.material = materials[type];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == SortingLayer.NameToID("Projectile"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        LevelManager.instance.ReturnBlock(gameObject);
        LevelManager.instance.materialValue[type] += 1;
    }
}