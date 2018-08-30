using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfinityManager : MonoBehaviour {

    public enum Event { None, Move, Shoot }//енум  дій

    public List<UnitManager> unit = new List<UnitManager>();// список усіх юнітів 
    public Event curEvent;// що робить вибранй юніт

    public UnitManager firstUnit, secondUnit;// використовуються при розчота

    public Transform objRange;// обєкт для показу радіусу дальності

    private int curNumUnit = 0;
    
	void Start () {
        firstUnit = unit[0];

        Debug.Log(Shance.Chance(10));
    }
	
	void Update () {
    }

    public void Move()// при нажатії на кнопку двигатися
    {
        curEvent = Event.Move;

        objRange.GetComponent<SetRadius>().curUnit = firstUnit;
        objRange.localScale = new Vector3(firstUnit.curRangeMove * 2, firstUnit.curRangeMove * 2, 0);
        objRange.position = new Vector3(firstUnit.transform.position.x, firstUnit.transform.position.y, 0);
    }

    public void Shoot()// при нажатії на кнопку стріляти
    {
        curEvent = Event.Shoot;
    }

    public void PrepareToDamage()// подготовка
    {
        if (CheckCollider(firstUnit.transform, secondUnit.transform))// якщо лінія зору чиста
        {
            if (secondUnit.transform.position.x < firstUnit.transform.position.x) // розвмртаєм себе до ворога
                firstUnit.GetComponent<SpriteRenderer>().flipX = true;
            else firstUnit.GetComponent<SpriteRenderer>().flipX = false;

            if(firstUnit.transform.position.x < secondUnit.transform.position.x)// розвмртаєм ворога до себе
                secondUnit.GetComponent<SpriteRenderer>().flipX = true;
            else secondUnit.GetComponent<SpriteRenderer>().flipX = false;

            firstUnit.SetAnimation("shoot");// включаэм анімацію стрельби
        }
    }

    public void GetDamageEnemy()// нанесення урону
    {
        secondUnit.curHealth -= firstUnit.damage;
        Debug.Log(string.Format("{0} наніс {1} урона {2}", firstUnit.name, firstUnit.damage, secondUnit.name));
        if (secondUnit.curHealth <= 0)
        {
            secondUnit.SetAnimation("die");
            Debug.Log(secondUnit.name + " здох");
            //unit.Remove(secondUnit);
        }
    }

    public bool CheckCollider(Transform obj1, Transform obj2)// перевіряєм чи нема препятствія між двома стінами
    {
        RaycastHit hit;
        Ray ray = new Ray(obj1.transform.position, obj2.transform.position - obj1.transform.position);

        bool isHit = Physics.Raycast(ray, out hit, 1000);
        if (isHit)
        {
            if (hit.transform.name == "wall")
            {
                Debug.Log("Через стіни не можна стріляти");
                return false;
            }
            else
            {
                Debug.Log(hit.transform.parent.name);
                Debug.Log("дистанция :" + hit.distance);
                return true;
            }
        }
        return false;
    }

    public void NextUnit()// передаєм хід іншому юніту
    {
        objRange.position = new Vector2(20, 20);
        curEvent = Event.None;
        firstUnit.curRangeMove = firstUnit.maxRangeMove;
        curNumUnit++;
        firstUnit = unit[curNumUnit == unit.Count ? curNumUnit = 0 : curNumUnit];

        secondUnit = null;

        firstUnit.curRangeMove = firstUnit.maxRangeMove;
        firstUnit.distanceToTarget = 5;// костиль
        
        Debug.Log("Ход " + firstUnit.name + "(" + curNumUnit + ")");
    }
}
