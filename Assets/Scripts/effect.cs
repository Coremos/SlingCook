using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().duration + 1f); //자동으로 삭제가 안되는 파티클을 1초뒤에 강제로 삭제시킨다.
    }
}
