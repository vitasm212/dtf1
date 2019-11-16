using UnityEngine;

namespace DTF
{
    public class Cell
    {
        public int type;
        public int pos;
        private CellView view;

        public Cell(int type, int pos, Transform transformRoot)
        {
            view = GameObject.Instantiate(Resources.Load<CellView>("other/cell" + type.ToString()), transformRoot);
            view.transform.localPosition = new Vector3(pos, -3, 0);
        }
    }
}