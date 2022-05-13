using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacteria_controller : MonoBehaviour
{
    GameObject[] food_mas;
    GameObject closest;
    Coroutine hungry;
    public float speed=2f;
    public float view=3f;
    public int mass=2;
    bool sleeping = true;
    void Start()
    {
        StartCoroutine(wake_up());
        hungry=StartCoroutine(hunger());
    }

    void Update()
    {
        if (!sleeping)
        {
            if(gameObject.tag=="bact")
            {
                GameObject food=find("food");
                GameObject predator=find("bact_predator");
                if(predator!=null && (predator.transform.position-gameObject.transform.position).sqrMagnitude<view)
                {
                    if(speed>0)
                        speed*=-1;
                    go(predator);
                }
                else
                {
                    if(speed<0)
                        speed*=-1;
                    go(food);
                }
            }
            else if(gameObject.tag=="bact_predator")
                go(find("bact"));
        }
        setSpeed();
        border();
    }

    private GameObject find(string str)
    {
        GameObject[] mas = GameObject.FindGameObjectsWithTag(str);
        if(mas.Length==0)
            return null;
        float distance=Mathf.Infinity;
        Vector3 pos = transform.position;
        foreach(GameObject el in mas)
        {
            Vector3 diff = el.transform.position - pos;
            float curDis = diff.sqrMagnitude;
            if(curDis<distance)
            {
                closest = el;
                distance = curDis;
            }
        }
        return closest;
    }

    private void go(GameObject food)
    {
        if(food!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, food.transform.position, speed*Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!sleeping)
        {
            if(gameObject.tag=="bact" && collision.gameObject.tag=="food")
            {
                Destroy(collision.gameObject);
                mass++;
                if(mass==4)
                    reproduction();
            }
            else if(gameObject.tag=="bact_predator" && collision.gameObject.tag=="bact")
            {
                mass+=collision.gameObject.GetComponent<bacteria_controller>().mass;
                Destroy(collision.gameObject);
                if(mass>=6)
                {
                    mass=6;
                    reproduction();
                }
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!sleeping)
        {
            if(gameObject.tag=="bact" && collision.gameObject.tag=="food")
            {
                Destroy(collision.gameObject);
                mass++;
                if(mass==4)
                    reproduction();
            }
            else if(gameObject.tag=="bact_predator" && collision.gameObject.tag=="bact")
            {
                mass+=collision.gameObject.GetComponent<bacteria_controller>().mass;
                Destroy(collision.gameObject);
                if(mass>=6)
                {
                    mass=6;
                    reproduction();
                }
            }
        }
    }
    IEnumerator hunger()
    {
        while(true)
        {
            if(gameObject.tag=="bact")
            {
                yield return new WaitForSeconds(5f-speed);
                mass--;
                if(mass==0)
                {
                    Destroy (gameObject);
                }
            }
            else{
                yield return new WaitForSeconds(3f);
                mass--;
                if(mass==0)
                {
                    Destroy (gameObject);
                }
            }
        }
    }
    IEnumerator wake_up()
    {
        yield return new WaitForSeconds(1f);
        sleeping=false;
        if(view > GameObject.FindGameObjectWithTag("GameController").GetComponent<main_controller>().max_view)
            GameObject.FindGameObjectWithTag("GameController").GetComponent<main_controller>().max_view=view;
    }
    private void reproduction()
    {
        if(gameObject.tag=="bact")
        {
            mass=2;
            sleeping=true;
            GameObject new_bact = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
            if(Random.Range(0,100)>96)
            {
                new_bact.gameObject.GetComponent<SpriteRenderer>().color=Color.red;
                new_bact.gameObject.tag="bact_predator";
            }
            else
            {
                new_bact.GetComponent<bacteria_controller>().view = Random.Range(view-1f,view+1f);
                new_bact.GetComponent<bacteria_controller>().speed = Random.Range(speed-1f,speed+1f);;
            }
            sleeping=false;
        }
        else if(gameObject.tag=="bact_predator")
        {
            mass=3;
            GameObject new_bact = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
        }
        //new_bact.GetComponent<bacteria_controller>().speed=Random.Range(speed,speed+1f);
    }

    private void border()
    {
        if(gameObject.transform.position.x> 35f)
            gameObject.transform.position = new Vector3(35f, gameObject.transform.position.y,0);
        else if(gameObject.transform.position.x<-35f)
            gameObject.transform.position = new Vector3(-35f, gameObject.transform.position.y,0);

        if(gameObject.transform.position.y> 19.5f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 19.5f, 0);
        else if(gameObject.transform.position.y<-19.5f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -919.5f, 0);
    }

    private void setSpeed()
    {
        if(gameObject.tag=="bact_predator")
            speed = 2f + 0.5f*(3f-mass);
    }

}
