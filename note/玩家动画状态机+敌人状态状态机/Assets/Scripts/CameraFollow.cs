﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //角色位置
    private Transform player;
    //相机与角色位置的偏移量
    private Vector3 offest;
    private Vector3 newPos;
    private float moveSpeed = 10;
    private float rotateSpeed = 5;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        //初始化偏移量为角色到相机的向量
        offest = transform.position - player.position;
    }
    private void Update()
    {
        //理想情况下相机的位置
        Vector3 standardPos = offest + player.position;
        //玩家最上方的相机位置
        Vector3 abovePos = player.position + Vector3.up * offest.magnitude;
        //选取从理想位置到玩家最上方位置的六个点位
        Vector3[] checkPoints = new Vector3[6];
        checkPoints[0] = standardPos;
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.2f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.4f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.6f);
        checkPoints[4] = Vector3.Lerp(standardPos, abovePos, 0.8f);
        checkPoints[5] = abovePos;
        //检测相机最佳位置
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (CheckPoint(checkPoints[i]))
                break;
        }
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveSpeed);
        LookAtSmooth();
    }
    //平滑相机朝向
    void LookAtSmooth()
    {
        Vector3 dir = player.position - transform.position;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, qua, Time.deltaTime * rotateSpeed);
    }
    bool CheckPoint(Vector3 point)
    {
        RaycastHit hitInfo;
        //从相机位置发射射线检测是否有遮挡
        if (Physics.Raycast(point, player.position-point+Vector3.up, out hitInfo))
        {
            //如果遮挡物不是角色，则更新相机位置
            Debug.DrawRay(point, player.position-point , Color.red);
            
            if (hitInfo.transform != player)
                return false;
            newPos = point;
        }
        return true;
    }
}
