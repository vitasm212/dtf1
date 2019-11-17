using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DTF
{
    public class MobManager
    {
        private List<Unit> _units;

        public Unit[] GetUnits()
        {
            return _units.ToArray();
        }

        public MobManager(int round, Transform _board)
        {
            _units = new List<Unit>();
            _units.Add(new Unit(UnitType.player, 6, 0, 6, _board));

            switch (round)
            {
                case 1:
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 3, 1, 12, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 5, 1, 0, _board));
                    break;
                case 2:
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 4, 1, 10, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 12, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 2, _board));
                    break;
                case 3:
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 10, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 12, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 2, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 0, _board));
                    break;
                case 4:
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 10, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 12, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 2, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 0, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 4, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 6, 1, 8, _board));
                    break;
                default:
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 10, 1, round, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 12, 1, round, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 2, 1, round, _board));
                    _units.Add(new Unit((UnitType)UnityEngine.Random.Range(2, 4), 0, 1, round, _board));
                    break;
            }
        }
    }
}