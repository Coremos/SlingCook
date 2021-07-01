using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class other_player_spwan : MonoBehaviour
{
    public float timer = 0.0f; //시작타이머
    public int waitingTime = 5; //리스폰 시간
    public List<GameObject> otherplayers = new List<GameObject>();
    private int randspwan; //랜덤스폰을 하기위한 랜덤값저장
    public int maxpeople = 5; //최대 손님수
    public static int count = 0; //손님수 카운트
    private void Start()
    {
        Spwan();
    }
    void Update()
    {
        Spwan();
    }
    void Spwan()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            randspwan = Random.Range(0, otherplayers.Count); //저장되어 있는 프리펩의 랜덤리스폰

            if (count < maxpeople)
            {
                Instantiate(otherplayers[randspwan]); //오브젝트 스폰
               
                count++;
            }

            
           
                timer = 0;
            
        }
    }
}
