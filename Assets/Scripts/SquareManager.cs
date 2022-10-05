using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : PiecesMoveManager
{
    // Start is called before the first frame update
    public bool tur;
    public int katman;
    public int[] indexler;
    public int ust_index;
    public bool ustu_dolu = false;

    public void Create(bool tur, int katman, int[] indexler, int ust_index)
    {

        this.tur = tur;
        this.katman = katman;
        this.indexler = indexler;
        this.ust_index = ust_index;

        if (places[ust_index].yerdeki_top != null)
        {
            ustu_dolu=true;
        }
    }
    void Start()
    {

    }
}
