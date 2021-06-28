using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DestroyTime = 7.0f; // 자동으로 총알이 사라지는 시간
    public GameObject explosion;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
    
   
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "target") //target이라는 태그 오브젝틍 충돌할경우
        {
            Debug.Log("충돌");
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(coll.gameObject); //충돌된 객체를 없앤다
            Destroy(gameObject); //충돌된 총알을 없앤다
            ScoreManager.scoreValue += 10; //ScoreManager파일의 scoreValue변수를 불러와 10을 추가한다.
        }
    }
}
