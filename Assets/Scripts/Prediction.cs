using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prediction : Singleton<Prediction>
{
    public GameObject obstacles;
    public int maxIteration;
    public int multiplyIteration;

    Scene currentScene;
    Scene predictionScene;
    PhysicsScene currentPhysicsScene;
    PhysicsScene predictionPhysicsScene;
    LineRenderer lineRenderer;
    GameObject dummy;
    List<GameObject> dummyObstacles = new List<GameObject>();

    void Start()
    {
        Physics.autoSimulation = false;

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();
        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        lineRenderer = GetComponent<LineRenderer>();
        CopyAllObstacles();
    }

    public void CopyAllObstacles()
    {
        foreach (Transform trans in obstacles.transform)
        {
            if (trans.gameObject.GetComponent<Collider>() != null)
            {
                GameObject fakeT = Instantiate(trans.gameObject);
                fakeT.transform.position = trans.position;
                fakeT.transform.rotation = trans.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if (fakeR)
                {
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                
                dummyObstacles.Add(fakeT);
            }
        }
    }

    void DestroyAllObstacles()
    {
        foreach (var o in dummyObstacles)
        {
            Destroy(o);
        }
        dummyObstacles.Clear();
    }

    public void Predict(GameObject subject, Vector3 currentPosition, Vector3 force)
    {
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid())
        {
            if (dummy == null)
            {
                dummy = Instantiate(subject);
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }

            dummy.transform.position = currentPosition;
            dummy.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = maxIteration;

            for (int i = 0; i < maxIteration; i++)
            {
                lineRenderer.SetPosition(i, dummy.transform.position);
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime * multiplyIteration);
            }

            Destroy(dummy);
        }
    }

    public void OffPredict()
    {
        lineRenderer.enabled = false;
    }

    void FixedUpdate()
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.deltaTime);
        }
    }

    void Update()
    {
        
    }
}
