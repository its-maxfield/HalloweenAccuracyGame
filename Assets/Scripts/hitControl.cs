using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class hitControl : MonoBehaviour
{
    //The target object to spawn in the scene.
    public GameObject targetPrefab;

    //Count the number of missed shots
    private int missCounter = 0;
    private int targetsHit = 0;

    private List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    public int numTargets = 3;

    private float height;
    private float width;

    private Vector3 origin;
    public GameObject gameOver;

    void Start()
    {
      Camera cam = Camera.main;
      origin = cam.GetComponent<Transform>().position;
      height = Mathf.Ceil(((2f * cam.orthographicSize) % 5.0f)) * 1.75f;
      width = Mathf.Ceil(((height * cam.aspect) % 5.0f)) * 2.5f;

      for(int i = 0; i < numTargets; ++i)
      {
        makeTarget();
      }
    }

    //Used for the Physics calculations.
    void Update()
    {
      if(Input.GetMouseButtonDown(0) && gameOver.activeSelf == false)
      {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        try
        {
          //Determine if it is a target layer.
          if(hit.collider.gameObject.layer == 8)
          {
            foreach(Transform child in hit.collider.gameObject.transform)
            {
              if(child.gameObject.name.Contains("Hit"))
              {
                child.gameObject.SetActive(true);
              }
              else
              {
                child.gameObject.SetActive(false);
              }
            }
            ++targetsHit;

            StartCoroutine(ReplaceTarget(hit.collider.gameObject));
            makeTarget();
          }
        }
        catch (NullReferenceException)
        {
          Debug.Log("Target not hit!");
          ++missCounter;
        }
      }
    }

    private void makeTarget()
    {
      float targetX = UnityEngine.Random.Range(origin.x - width, origin.x + width);
      float targetY = UnityEngine.Random.Range(origin.y - height, origin.y + height);

      Instantiate(targetPrefab, new Vector3(targetX, targetY, -1), Quaternion.identity);
    }

    IEnumerator ReplaceTarget(GameObject toRemove)
    {
      yield return new WaitForSeconds(0.2f);
      Destroy(toRemove);
    }

    public int returnMisses()
    {
      return missCounter;
    }

    public int returnTargets()
    {
      return targetsHit;
    }
}
