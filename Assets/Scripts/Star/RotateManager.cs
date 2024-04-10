using StarRotate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Star = new List<GameObject>();
    float[,] StarData ={
            { 1,1,1,1},//Sun
            { 2,2,2,2},//Watch
            { 3,3,3,3},//Venus
            { 4,4,4,4},//Earth
            { 5,5,5,5},//Moon
            { 6,6,6,6},//Mars
            { 7,7,7,7} //Wood
        };

    void Start()
    {
        for (int i = 0; i < Star.Count; i++)
        {
            Rotate rotate = Star[i].GetComponent<Rotate>();
            rotate.centerPoint = Star[0].transform;
            if (i == 4){rotate.centerPoint = Star[3].transform;}
            rotate.rotationSpeed = StarData[i, 1];
            rotate.orbitSpeed = StarData[i, 2];
            rotate.distanceFromCenter = StarData[i, 3];
        }
    }
};

