using StarRotate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Star = new List<GameObject>();
    float[,] StarData ={
            { 1,10,0,0},//Sun
            { 2,58,47.87f,6},//Water
            { 3,243,35.02f,8},//Venus
            { 4,1,29.78f,10},//Earth
            { 5,30,1,2},//Moon
            { 6,1,24.13f,12},//Mars
            { 7,0.375f,13.07f,15} //Wood
        };

    void Start()
    {
        for (int i = 0; i < Star.Count; i++)
        {
            Rotate rotate = Star[i].GetComponent<Rotate>();
            rotate.centerPoint = Star[0].transform;
            if (i == 4){rotate.centerPoint = Star[3].transform;}
            rotate.rotationSpeed = 24/StarData[i, 1];
            rotate.orbitSpeed = StarData[i, 2]/10;
            rotate.distanceFromCenter = StarData[i, 3];
        }
    }
};

