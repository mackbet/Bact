using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_controller : MonoBehaviour
{

    public GameObject bacteria_prefab, food_prefab, food_container,predator_container,bact_container;
    public float max_view=0f;
    void Start()
    {
        StartCoroutine(instObj());
    }

    void Update()
    {
        GameObject[] mas = GameObject.FindGameObjectsWithTag("bact_predator");
        foreach(GameObject el in mas)
        {
            if(el.transform.parent!=predator_container)
            {
                el.transform.SetParent(predator_container.transform);
            }
        }
        
        mas = GameObject.FindGameObjectsWithTag("bact");
        foreach(GameObject el in mas)
        {
            if(el.transform.parent!=bact_container)
            {
                el.transform.SetParent(bact_container.transform);
            }
        }
    }

    IEnumerator instObj ()
    {
        while(true){

            GameObject food = Instantiate(food_prefab, new Vector3(Random.Range(-35f, 35f),Random.Range(-19.5f,19.5f),0), Quaternion.identity);
            food.transform.SetParent(food_container.transform);
            food = Instantiate(food_prefab, new Vector3(Random.Range(-35f, 35f),Random.Range(-19.5f, 19.5f),0), Quaternion.identity);
            food.transform.SetParent(food_container.transform);
            food = Instantiate(food_prefab, new Vector3(Random.Range(-35f, 35f), Random.Range(-19.5f, 19.5f), 0), Quaternion.identity);
            food.transform.SetParent(food_container.transform); food = Instantiate(food_prefab, new Vector3(Random.Range(-35f, 35f), Random.Range(-19.5f, 19.5f), 0), Quaternion.identity);
            food.transform.SetParent(food_container.transform);
            yield return new WaitForSeconds(0.3f);
        }
    }   

}
