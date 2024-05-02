using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class TreeList {
    public List<Tree> trees;

}



[Serializable]
public class Tree {
    public string tree_name;
    public string user_id;
    public double position_x;
    public double position_y;
    public double position_z;
    

    public Tree(){}

    public Tree(Vector3 position, string treeName){
        user_id = GameManager.i.GetUser().GetId();
        position_x = position.x;
        position_y = position.y;
        position_z = position.z;
        this.tree_name = treeName;
    }

    public double getx()=>position_x;

    public double gety()=>position_y;

    public double getz()=>position_z;
    
}

