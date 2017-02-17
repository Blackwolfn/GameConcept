using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer {
    
    public LineWidth lw = new LineWidth();

    //##############################################
    //  Constructors
    //##############################################

    public enum LineWidth { Thin, Medium, Thick};

    public void DrawLine( Texture2D tex, Vector2 p1, Vector2 p2 ) {
        bool fade = false;
        Color col = Color.white;
        drawLine( tex, p1, p2, col, fade );
    }

    public void DrawLine( Texture2D tex, Vector2 p1, Vector2 p2, bool fade ) {
        Color col = Color.white;
        drawLine( tex, p1, p2, col, fade );
    }

    public void DrawLine( Texture2D tex, Vector2 p1, Vector2 p2, Color col ) {
        bool fade = false;
        drawLine( tex, p1, p2, col, fade );
    }

    public void DrawLine( Texture2D tex, Vector2 p1, Vector2 p2, Color col, bool fade ) {
        drawLine( tex, p1, p2, col, fade );
    }

    //##############################################
    // Functions
    //##############################################

    void drawLine( Texture2D tex, Vector2 p1, Vector2 p2, Color col, bool fade ) {
        Vector2 t = p1;
        float frac = 1 / Mathf.Sqrt( Mathf.Pow( p2.x - p1.x, 2 ) + Mathf.Pow( p2.y - p1.y, 2 ) );
        float ctr = 0;

        while( ( int ) t.x != ( int ) p2.x || ( int ) t.y != ( int ) p2.y ) {
            if( fade )
                col.a = ctr * -1;
            t = Vector2.Lerp( p1, p2, ctr );
            ctr += frac;

            switch( lw ) {
                case LineWidth.Medium:
                    // Set a grid of 3x3 pixels
                    Color[] currentColors9 = tex.GetPixels( ( int ) t.x - 1, ( int ) t.y - 1, 3, 3 );
                    Color[] newColors9 = setAlpha( currentColors9, col );
                    Vector2[] pos9 = new Vector2[9];
                    int count9 = 0;
                    for( int x = -1; x < 2; x++ ) {
                        for( int y = -1; y < 2; y++ ) {
                            pos9[count9] = new Vector2(t.x + x, t.y + y);
                            count9++;
                        }
                    }

                    for( int i = 0; i < newColors9.Length; i++ ) {
                        tex.SetPixel( (int) pos9[i].x, ( int ) pos9[i].y, newColors9[i] );
                    }
                    
                    break;

                case LineWidth.Thick:
                    // Set a grid of 5x5 pixels
                    Color[] currentColors25 = tex.GetPixels( ( int ) t.x - 2, ( int ) t.y - 2, 5, 5 );
                    Color[] newColors25 = setAlpha( currentColors25, col );
                    Vector2[] pos25 = new Vector2[25];
                    int count25 = 0;
                    for( int x = -2; x < 3; x++ ) {
                        for( int y = -2; y < 3; y++ ) {
                            pos25[count25] = new Vector2( t.x + x, t.y + y );
                            count25++;
                        }
                    }

                    for( int i = 0; i < newColors25.Length; i++ ) {
                        tex.SetPixel( ( int ) pos25[i].x, ( int ) pos25[i].y, newColors25[i] );
                    }

                    break;

                default:
                    tex.SetPixel( ( int ) t.x, ( int ) t.y, col );
                    break;
            }
            
        }
    }

    Color[] setAlpha(Color[] current, Color newCol) {
        Color[] colors = current;
        float tmpAlpha;
        float[] f = new float[] {0.48f, 0.93f, 0.48f,
                                 0.93f, 1f, 0.93f,
                                 0.48f, 0.93f, 0.48f};

        if(lw == LineWidth.Thick) {
            f = new float[] {0.15f, 0.75f, 0.98f, 0.75f, 0.15f,
                             0.75f, 0.99f, 1f, 0.99f, 0.75f,
                             0.98f, 1f, 1f, 1f, 0.98f,
                             0.75f, 0.99f, 1f, 0.99f, 0.75f,
                             0.15f, 0.75f, 0.98f, 0.75f, 0.15f};
        }

        // Color all pixels
        for( int i = 0; i < colors.Length; i++ ) {
            tmpAlpha = colors[i].a;
            colors[i] = newCol;
            tmpAlpha += ( newCol.a * f[i] );
            if( tmpAlpha > 1 )
                tmpAlpha = 1;
            colors[i].a = tmpAlpha;
        }   

        return colors;
    }

    public void Apply(Texture2D tex ) {
        tex.Apply();
    }
}
