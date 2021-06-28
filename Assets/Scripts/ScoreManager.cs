using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue; // 점수
    Text text; 
    void Awake()
    {
        text = GetComponent<Text>(); //텍스트 컴포넌트 불러오기
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + scoreValue; //컴포넌트를 이용해 현재점수를 보여준다
    }
}
