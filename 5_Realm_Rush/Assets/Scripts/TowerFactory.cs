using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab = null;
    [SerializeField] int towerLimit = 5;

    Queue<Tower> towerQueue = new Queue<Tower>();
    
    //Create an empty queue of towers
    
    //Find the number of twoers and compare it to tower limit to allow limited tower spawns
    public void PlaceTower(Waypoint baseWaypoint) {
        int TowerCount = towerQueue.Count;
        // FindObjectsOfType<Tower>().Length;
        
        if(TowerCount < towerLimit) {
            InstantiateNewTower(baseWaypoint);
        } else {
            MoveExistingTower(baseWaypoint);
        }
    }

    //Create New Tower
    private void InstantiateNewTower(Waypoint baseWaypoint) {
        //set the placeable flags
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        

        //set the baseWaypoints
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        //Put new tower on the queue
        towerQueue.Enqueue(newTower);
    }

    //Move the oldest tower to new tower position
    private void MoveExistingTower(Waypoint newBaseWaypoint) {
        // Debug.Log("Max towers reach!");
        //take bottom tower off queue
        var OldTower = towerQueue.Dequeue();

        //set the placeable flags
        OldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        //set the baseWaypoints
        OldTower.baseWaypoint = newBaseWaypoint;

        //move tower
        OldTower.transform.position = newBaseWaypoint.transform.position;

        //put the old tower on top of the queue
        towerQueue.Enqueue(OldTower);
        
    }
    
}
