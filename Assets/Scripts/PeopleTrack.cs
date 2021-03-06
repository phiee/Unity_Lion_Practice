﻿using UnityEngine;
using System.Linq; //引用 系統.查詢語言 Lin Query

/// <summary>
/// 追蹤人類
/// </summary>

public class PeopleTrack : People
{
    /// <summary>
    /// 目標
    /// </summary>
    protected Transform target;

    /// <summary>
    /// 人類陣列
    /// </summary>
    public People[] people;

    /// <summary>
    /// 距離
    /// </summary>
    public float[] distance;


    protected virtual void Start()
    {
        // 人類陣列 = 透過類型尋找物件<泛型>()
        people = FindObjectsOfType<People>();
        // 距離陣列的數量 = 人類陣列的數量
        distance = new float[people.Length];
    }

    private void Update()
    {
        Track();
    }
       
    /// <summary>
    /// 追蹤方法
    /// </summary>

    protected virtual void Track()
    {
        // 儲存所有人跟此物件的距離

        for (int i = 0; i < people.Length; i++)
        {
            if (people[i].transform.name == "殭屍" || people[i].transform.name == "警察")
            {
                distance[i] = 999; //與殭屍物件的距離改為999
                continue; //繼續-跳過並執行下一次迴圈
            }
            distance[i] = Vector3.Distance(transform.position, people[i].transform.position);
        }

        // 判斷最近的目標
        float min = distance.Min();                     // 最小值 = 距離.最小值
        int index = distance.ToList().IndexOf(min);     // 索引值 = 距離.轉清單.取得索引值(最小值)
        target = people[index].transform;               // 目標 = 人類[索引值].變形
        // 追蹤最近的目標
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="火球")
        {
            Dead();
        }
    }
}
