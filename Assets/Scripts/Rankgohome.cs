using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//랭킹->게임플레이
public class Rankgohome : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SceneManager.LoadScene("gameplay_ing");
            DontDestroyOnLoad(other);
        }
    }
}
