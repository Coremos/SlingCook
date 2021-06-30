using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

//게임플레이->상점
public class ShopCube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        { 
            SceneManager.LoadScene("Shop");
            DontDestroyOnLoad(other);
        }
    }
}
