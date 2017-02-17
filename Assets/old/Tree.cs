using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tree {
    public float stemHeight = 1f;
    public float stemHeightMin;
    public float stemHeightMax;
    public float stemDia = 0.5f;
    public float crownHeight = 3f;
    public float crownHeightMin = 0.5f;
    public float crownHeightMax = 15f;
    public float crownDia = 3f;
    public float crownDiaMin = 1.5f;
    public float crownDiaMax = 10f;
    public GameObject tree;
    public GameObject stem;
    public GameObject crown;

    public Tree(float stemheight, float stemdiameter, float crownheight, float crowndiameter) {
        stemHeight = stemheight;
        stemDia = stemdiameter;
        crownHeight = crownheight;
        crownDia = crowndiameter;

        tree = new GameObject();
    }

    public Tree( float stemheight, float stemdiameter, float crowndiameter ) {
        stemHeight = stemheight;
        stemDia = stemdiameter;
        crownHeight = crowndiameter;
        crownDia = crowndiameter;

        tree = new GameObject();
    }

    public void newTree(string name, Vector3 worldposition) {
        tree.name = name;
        tree.transform.position = worldposition;
        genStem();
        genCrown();
    }

    public void updateStem( float stemheight, float stemdiameter ) {
        stemHeight = stemheight;
        stemDia = stemdiameter;
        stemTransform();
        crownTransform();
    }

    public void updateCrown() {
        crownTransform();
    }

    void stemTransform() {
        stem.transform.localScale = new Vector3( stemDia, stemHeight, stemDia );
        stem.transform.position = new Vector3( 0, stemHeight, 0 );
    }

    public void genStem() {
        stem = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
        stemTransform();
        stem.transform.SetParent( tree.transform );
    }

    void crownTransform() {
        crownHeight = Random.Range( crownHeightMin, crownHeightMax );
        crownDia = Random.Range( ( stemDia * crownDiaMin ), ( stemDia * crownDiaMax ) );
        crown.transform.localScale = new Vector3( crownDia, crownHeight, crownDia );
        crown.transform.position = new Vector3( 0, ( ( (stemHeight * 2) + ( crownHeight / 2 ) ) - ( crownHeight / 30 ) ), 0 );
    }

    public void genCrown() {
        crown = GameObject.CreatePrimitive( PrimitiveType.Sphere );
        crownTransform();
        crown.transform.SetParent( tree.transform );
    }

    public void setStemDiaRange( float min, float max ) {
        
    }

    public void setCrownDiaRange( float min, float max ) {
        crownDiaMin = min;
        crownDiaMax = max;
    }
}
