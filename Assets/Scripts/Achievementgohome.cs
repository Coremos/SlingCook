using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//����->�����÷���
public class Achievementgohome : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SceneManager.LoadScene("gameplay_last");
            DontDestroyOnLoad(other);
        }
    }
}
