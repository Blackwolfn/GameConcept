using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTile : MonoBehaviour{
    Helper helper = new Helper();
    public int x, y;
    public Vector3 hexagonPosition;
    public List<float> neighbors = new List<float>();
    public Vector3[] borderWaypoints;

    float rowOffset = Mathf.Sqrt( Mathf.Pow( 1, 2 ) - Mathf.Pow( 0.5f, 2 ) );

    public HexagonTile(int _x, int _y) {

        x = _x;
        y = _y;
        Debug.Log( "hexagonPosition" );
        hexagonPosition = getTransform();
        Debug.Log( "getNeighbours()" );
        getNeighbours();



        // generate a cylinder
        GameObject hex = GameObject.CreatePrimitive( PrimitiveType.Cylinder );
        hex.transform.position = hexagonPosition;
        hex.transform.localScale = new Vector3(1,0.1f,1);

    }

    Vector3 getTransform() {
        Vector3 xyz = new Vector3();

        // set x based on the row
        if(helper.isOdd(y)) { xyz.x = x + 0.5f; }
        else { xyz.x = x; }
        // set y based on het row
        xyz.z = y * rowOffset;
        // get height based on the terrain
        xyz.y = getTerrainHeight( xyz.x, xyz.z );

        return xyz;
    }

    float getTerrainHeight( float _x, float _y ) {
        float height=0;
        // add raycast
        return height;
    }

    void getNeighbours() {
        // calculate the height of the neighbours based on the terrain
        neighbors.Add( getTerrainHeight( hexagonPosition.x - 0.5f, hexagonPosition.z + rowOffset ));
        neighbors.Add( getTerrainHeight( hexagonPosition.x + 0.5f, hexagonPosition.z + rowOffset ));
        neighbors.Add( getTerrainHeight( hexagonPosition.x +1, hexagonPosition.z ));
        neighbors.Add( getTerrainHeight( hexagonPosition.x + 0.5f, hexagonPosition.z - rowOffset ));
        neighbors.Add( getTerrainHeight( hexagonPosition.x - 0.5f, hexagonPosition.z - rowOffset ));
        neighbors.Add( getTerrainHeight( hexagonPosition.x - 1, hexagonPosition.z ));
    }

}
