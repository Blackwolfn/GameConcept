using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMouseHexTexture : MonoBehaviour {

    LineDrawer lineDrawer = new LineDrawer();
    float ir = 48f;
    float or = 64f;
    Color icol = Color.white;
    Color ocol = Color.white;

    public void SetRadius(float innerRadius, float outerRadius) {
        ir = innerRadius;
        or = outerRadius;
    }

    public void SetColor( Color color ) {
        icol = color;
        ocol = color;
    }

    public void SetColor( Color innerColor, Color outerColor ) {
        icol = innerColor;
        ocol = outerColor;
    }

    public void Generate( Texture2D tex, LineDrawer.LineWidth innerLineWidth, LineDrawer.LineWidth outerLineWidth ) {
        float x, y;
        float cx = (tex.width / 2)-1;
        float cy = (tex.height / 2)-1;

        switch( outerLineWidth ) {
            case LineDrawer.LineWidth.Thin:
                if( or > cx ) or = cx-1;
                if( or > cy ) or = cy-1;
                break;
            case LineDrawer.LineWidth.Medium:
                if( or > cx-1 ) or = cx-2;
                if( or > cy-1 ) or = cy-2;
                break;
            case LineDrawer.LineWidth.Thick:
                if( or > cx - 2 ) or = cx - 3;
                if( or > cy - 2 ) or = cy - 3;
                break;
        }

        Vector2[] pInner = new Vector2[6];
        Vector2[] pOuter = new Vector2[6];

        for( int i = 0; i < 6; i++ ) {
            y = cx + ir * Mathf.Cos( 2 * Mathf.PI * i / 6 );
            x = cy + ir * Mathf.Sin( 2 * Mathf.PI * i / 6 );
            //Debug.Log("Point "+i+": "+x+", "+y);
            pInner[i].x = x;
            pInner[i].y = y;
        }
        
        for( int i = 0; i < 6; i++ ) {
            y = cx + or * Mathf.Cos( 2 * Mathf.PI * i / 6 );
            x = cy + or * Mathf.Sin( 2 * Mathf.PI * i / 6 );
            //Debug.Log("Point "+i+": "+x+", "+y);
            pOuter[i].x = x;
            pOuter[i].y = y;
        }

        for( int i = 0; i < 5; i++ ) {
            // set linewidth;
            lineDrawer.lw = outerLineWidth;
            lineDrawer.DrawLine( tex, pInner[i], pOuter[i], ocol, false );
            // set linewidth;
            lineDrawer.lw = innerLineWidth;
            lineDrawer.DrawLine( tex, pInner[i], pInner[i+1], icol );
        }
        // set linewidth;
        lineDrawer.lw = outerLineWidth;
        lineDrawer.DrawLine( tex, pInner[5], pOuter[5], ocol, false );
        // set linewidth;
        lineDrawer.lw = innerLineWidth;
        lineDrawer.DrawLine( tex, pInner[5], pInner[0], icol );

        lineDrawer.Apply( tex );

    }
}
