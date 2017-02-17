using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile {

    Helper helper;
    float rowOffset = Mathf.Sqrt( Mathf.Pow( 1, 2 ) - Mathf.Pow( 0.5f, 2 ) );

    HexWorld hexWorld;
    int x;
    int y;
    public Vector3 tilePosition;
    public Vector3[] neighbours = new Vector3[6];


    public HexTile ( HexWorld hexGen, int x, int y) {
        helper = new Helper();
        this.hexWorld = hexGen;
        this.x = x;
        this.y = y;
        tilePosition = getTransform();
        PopulateNeighbours();
    }

    Vector3 getTransform() {
        Vector3 xyz = new Vector3();

        // set x based on the row
        if( helper.isOdd( y ) ) { xyz.x = x + 0.5f; }
        else { xyz.x = x; }
        // set y based on het row
        xyz.z = y * rowOffset;
        // get height based on the terrain
        xyz.y = getTerrainHeight( xyz.x, xyz.z );

        return xyz;
    }

    float getTerrainHeight( float x, float y ) {
        float height = 0;
        // add raycast
        return height;
    }

    void getNeighbour(int index, int x, int y) {
        if( hexWorld.IsInGrid( x, y ) ) {
            neighbours[index] = new Vector3( x - 1, y + 1, getTerrainHeight( x - 1, y + 1 ) );
        }
        else {
            neighbours[index] = new Vector3(-1,-1,-1);
        }
    }

    void PopulateNeighbours() {
        // Neighbours Vector3( (int) grid x position, (int) grid y position, (float) world height position )
        
        getNeighbour(0, x - 1, y + 1 );
        getNeighbour(1, x + 1, y + 1 );
        getNeighbour(2, x + 1, y );
        getNeighbour(3, x + 1, y - 1 );
        getNeighbour(4, x - 1, y - 1 );
        getNeighbour(5, x - 1, y );        
    }

}
