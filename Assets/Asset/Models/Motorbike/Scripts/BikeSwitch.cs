using UnityEngine;
using System.Collections;

public class BikeSwitch : MonoBehaviour
{

    public Transform[] Bikes;
    public Transform MyCamera;


    public void CurrentBikeActive(int current)
    {

        int amount = 0;

        foreach (Transform Bike in Bikes)
        {
            if (current == amount)
            {
                MyCamera.GetComponent<BikeCamera>().target = Bike;

                MyCamera.GetComponent<BikeCamera>().Switch = 0;
                MyCamera.GetComponent<BikeCamera>().cameraSwitchView = Bike.GetComponent<BikeControl>().bikeSetting.cameraSwitchView;
                MyCamera.GetComponent<BikeCamera>().BikerMan = Bike.GetComponent<BikeControl>().bikeSetting.bikerMan;

                Bike.GetComponent<BikeControl>().activeControl = true;
            }
            else
            {
                Bike.GetComponent<BikeControl>().activeControl = false;
            }

            amount++;
        }
    }




}
