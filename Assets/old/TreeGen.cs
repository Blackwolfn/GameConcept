using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGen : MonoBehaviour {
    Tree tree;

    void Start () {
        tree = new Tree( 2, 0.5f, 5 );
        tree.newTree( "hallo", new Vector3(0,0,0) );

    }

    void Update () {
        if(Input.GetKeyDown("space")) {
            //tree.updateStem( Random.Range( ( 0.5f ), 10f ), Random.Range(( 0.2f ), 2f ));
            tree.updateCrown();
        }
    }


}
