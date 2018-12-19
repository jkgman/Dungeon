using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMap : MonoBehaviour {
    [SerializeField]
    private DungeonData dungeonData;
    int currentSelection = 0;
    Material mapMat;
    [SerializeField]
    GameObject stamp;
    [SerializeField]
    GameObject line;
    BoxCollider box;
    private void Start()
    {
        mapMat = GetComponent<Renderer>().material;
        box = GetComponent<BoxCollider>();
        SetMap();
    }
    void SetMap() {
        mapMat.mainTexture = dungeonData.mapTex;
        for(int i = 0; i < dungeonData.levels.Length; i++)
        {
            GameObject currentStamp = Instantiate(stamp);
            currentStamp.transform.position = box.center + new Vector3(-box.size.x/2, 0, 0) + dungeonData.levels[i].mapPosition;
            currentStamp.GetComponent<Renderer>().material.mainTexture = dungeonData.levels[i].levelTex;
            if(i+1 < dungeonData.levels.Length)
            {
                LineRenderer currentLine = Instantiate(line).GetComponent<LineRenderer>();
                currentLine.SetPosition(0, box.center + new Vector3(-box.size.x / 2, 0, 0) + dungeonData.levels[i].mapPosition);
                currentLine.SetPosition(1, box.center + new Vector3(-box.size.x / 2, 0, 0) + dungeonData.levels[i + 1].mapPosition);
            }
        }
    }
    /*Set Map
     for dungeon.data.levels
        put model on level spot for each enemy, essential item, and one general chest(show looted essential items if gotten already)
    Select(level = 0 or last level completed)
     */

    /*Select(level#)
        moving = true
        lastpos = currentpos
        nextpos = level#.pos
        currentselection = level#
     */

    /*input
     if !moving{
    save
    exit
    select
    }
     */

    /*update
     * if moving{
     * lerp lastpos nextpos
     * if  pos = next pos
     *      moving = false
     * }
     */
}
