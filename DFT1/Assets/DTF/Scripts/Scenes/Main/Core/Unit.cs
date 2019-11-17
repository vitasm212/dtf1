using System;
using UnityEngine;

namespace DTF
{
    public class Unit
    {
        public UnitType type;
        public int damageDist;
        public SetDamage damage;
        public int powerPlus;
        public int hp;
        public int maxHp;
        public int pos;
        public int newPos;
        public int direction;
        public UnitView view;
        private UnitState _state = UnitState.Idle;
        public Action<Unit, UnitState> onChangeState;
        public UnitState State
        {
            set
            {
                _state = value;
                onChangeState?.Invoke(this, value);
            }
        }
        public float stateProgres;

        public Unit(UnitType unitType, int startHp, int power, int unitPos, Transform transformRoot)
        {
            type = unitType;


            if (unitType == UnitType.mob1)
                damageDist = 1;
            else if (unitType == UnitType.mob2)
                damageDist = 2;

            hp = startHp;
            maxHp = startHp;
            pos = unitPos;
            powerPlus = power - 1;

            view = GameObject.Instantiate(Resources.Load<UnitView>("units/" + unitType), transformRoot);
            view.transform.localPosition = new Vector3(pos, -3, 0);
            view.SetPowerStar(powerPlus);
        }

        public void Attack(SetDamage newDamag)
        {
            damage = newDamag;
            State = UnitState.Attack;
            stateProgres = 0;
            view.ShowWeapon(true);
        }

        public void MoveTo(int moveTo)
        {
            State = UnitState.Move;

            if (moveTo < 0)
                moveTo = 0;
            if (moveTo >= Settings.MapSize)
                moveTo = Settings.MapSize - 1;

            newPos = moveTo;
            if (newPos == pos)
                stateProgres = 1;
            else
                stateProgres = 0;
        }

        public void Update()
        {
            switch (_state)
            {
                case UnitState.Attack:
                    stateProgres += Time.deltaTime;
                    if (stateProgres > 1)
                        stateProgres = 1;

                    if (stateProgres == 1)
                    {
                        view.ShowWeapon(false);
                        State = UnitState.Idle;
                    }
                    break;
                case UnitState.Move:
                    stateProgres += Time.deltaTime;
                    if (stateProgres > 1)
                        stateProgres = 1;
                    float offset = (newPos - pos) * stateProgres;
                    view.transform.localPosition = new Vector3(pos + offset, -3, 0);

                    if (stateProgres == 1)
                    {
                        pos = newPos;
                        State = UnitState.Idle;
                    }
                    break;
            }
            view.UpdateHp((float)hp / maxHp);
        }
    }

    public enum UnitState
    {
        Idle = 0,
        Move = 1,
        Attack = 2,
    }

    public struct SetDamage
    {
        public int value;
        public int dist;
        public CardDirection direction;
        public CardAttackType attackType;
    }
}