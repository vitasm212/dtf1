using UnityEngine;

namespace DTF
{
    public class Cell
    {
        public int type;
        public int pos;
        public CellView view;

        public Cell( int pos)
        {
            view = GameObject.Find("cell" + pos.ToString()).GetComponent<CellView>();
            //view = GameObject.Instantiate(Resources.Load<CellView>("other/cell" + type.ToString()), transformRoot);
            //view.transform.localPosition = new Vector3(pos, -3.449f, 0);
        }
    }
}