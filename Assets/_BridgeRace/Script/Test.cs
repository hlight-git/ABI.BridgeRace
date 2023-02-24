using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Test : MonoBehaviour
{
    public Transform param1;
    void Start()
    {
        //Vector2 vector2 = new Vector2(123, 456);
        //Vector3 vector3 = new Vector3(123, 456, 789);

        //// B?t ??u ?o th?i gian khi add 100000 element vào List
        //var startTime1 = DateTime.Now;
        //for (int i = 1; i <= 100000; i++)
        //{
        //    var tmp = vector3.magnitude;
        //}
        //var time1 = DateTime.Now.Subtract(startTime1).Milliseconds;
        //print("Time to add element into list is: " + time1);

        //// B?t ??u ?o th?i gian khi add 100000 element vào ArrayList
        //var startTime2 = DateTime.Now;
        //for (int i = 1; i <= 100000; i++)
        //{
        //    var tmp = Vector3.Distance(vector3, Vector3.zero);
        //}
        //var time2 = DateTime.Now.Subtract(startTime2).Milliseconds;
        //print("Time to add element array into list is: " + time2);

        //transform.rotation = param1.rotation * Vector3.forward;
        //transform.rotation = Quaternion.AngleAxis(60, Vector3.one);
        Vector3 dir = Quaternion.Euler(-15, 0, 0) * Vector3.forward;
        Debug.DrawRay(transform.position, dir, Color.red, 100f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Gen()
    {
        yield return new WaitForSeconds(2);
        Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Instantiate(transform.gameObject, transform.position, Quaternion.identity);
    }
}
