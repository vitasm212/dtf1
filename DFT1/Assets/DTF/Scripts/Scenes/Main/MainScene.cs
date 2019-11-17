using System;
using System.Collections.Generic;
using System.Linq;
using DTF.ui;
using UnityEngine;

namespace DTF.Scenes
{
    public class MainScene : MonoBehaviour
    {
        private static Canvas _canvas;
        public static Canvas Canvas { get { if (_canvas == null) _canvas = GameObject.Find("CanvasUI").GetComponent<Canvas>(); return _canvas; } }

        private ScenesManager _scenesManager;
        private UIController _uIController;

        private CameraControl _cameraControl;
        private BoardView _board;
        private Bulets _bulets;
        private Map _map;
        private Unit[] _units;
        private List<Unit> _agrUnit = new List<Unit>();

        private int _stateAi = 0;
        private bool _stopGame = false;
        private int nextLevel = 1;

        private int _boostAttack = 0;       

        private Packs1 _packs;
        private List<int> _additionalCard;
        private MobManager _mobManager;

        void Start()
        {
            _scenesManager = GameObject.FindObjectOfType<ScenesManager>();

            var param = ScenesManager.GetSceneParams(SceneId.Main) as MainSceneParams;
            nextLevel = param.Round + 1;
            _additionalCard = param.addCard;
            _board = GameObject.FindObjectOfType<BoardView>();
            _bulets = GameObject.FindObjectOfType<Bulets>();
            _cameraControl = GameObject.FindObjectOfType<CameraControl>();

            _packs = new Packs1();
            _packs.GeneratePack(20, _additionalCard);

            _uIController = new UIController(Canvas);
            _uIController.NextTurnPanelView().Show();
            _uIController.NextTurnPanelView().onNextTurn = OnNextTurn;

            _uIController.TopPanelView().Show();
            _uIController.TopPanelView().onSelectCard = OnSelectCard;
            _uIController.TopPanelView().onShowPack = () =>
            {
                _uIController.TopPanelView().Hide();
                _uIController.NextTurnPanelView().Hide();
                _uIController.PackPanelView().Show();
                _uIController.PackPanelView().Setup(_packs.ShowAll());
                _uIController.PackPanelView().onClose = () =>
                 {
                     _uIController.PackPanelView().Close();
                     _uIController.TopPanelView().Show();
                     _uIController.NextTurnPanelView().Show();
                 };
            };

            GenerateSetCard(6);

            _mobManager = new MobManager(param.Round, _board.transform);

            _units = _mobManager.GetUnits();           


            _map = new Map(Settings.MapSize);
            for (int x = 0; x < Map.Size; x++)
            {
                _map[x] = new Cell(1, x, _board.rootRoad.transform);
            }
        }

        private void GenerateSetCard(int count)
        {
            int countAdd = 0;
            for (int i = 0; i < count; i++)
            {
                if (_packs.Count > 0)
                {
                    _uIController.TopPanelView().AddCard(_packs.Get(0));
                    countAdd++;
                }
            }

            if (countAdd == 0)
            {
                _packs.Reset();
                GenerateSetCard(count);
            }
            else
                _uIController.TopPanelView().onEndAnimationNewCard = () => SetUIInteractable(true);
        }

        private void OnNextTurn()
        {
            _boostAttack = 0;
            _uIController.TopPanelView().SetCurentPower(_boostAttack);
            _uIController.TopPanelView().ClealCard();
            _agrUnit.Clear();
            for (int i = 1; i < _units.Length; i++)
            {
                if (_units[i] != null)
                {
                    _units[i].direction = Mathf.Abs(_units[i].pos - _units[0].pos);
                    _agrUnit.Add(_units[i]);
                }
            }
            _agrUnit = _agrUnit.OrderBy(u => u.direction).ToList();
            _stateAi = 0;
            SetUIInteractable(false);
            if (_agrUnit.Count == 0)
                EndAiTurn();
        }

        private void EndAiTurn()
        {
            if (_units[0].hp > 0)
            {
                GenerateSetCard(6);
            }
        }
        private void EndGame()
        {
            nextLevel = 1;
            _stopGame = true;
            _uIController.TopPanelView().Close();
            _uIController.NextTurnPanelView().Close();
            _uIController.LosePanelView().Show();
            _uIController.LosePanelView().onGoMenu = GoMenu;
        }

        private void OnSelectCard(Card card)
        {
            switch (card.type)
            {
                case CardType.AttackPlus:
                    _boostAttack += card.value;
                    _uIController.TopPanelView().SetCurentPower(_boostAttack);
                    break;
                case CardType.HpPlus:
                    _units[0].hp += card.value;
                    if (_units[0].hp > _units[0].maxHp)
                        _units[0].hp = _units[0].maxHp;
                    break;
                case CardType.Move:
                    SetUIInteractable(false);
                    var step = card.direction == CardDirection.Left ? -card.value : card.value;
                    step = _units[0].pos + step;
                    if (step < 0)
                        step = 0;
                    if (step >= Settings.MapSize)
                        step = Settings.MapSize - 1;

                    if (CanMove(step))
                    {
                        _units[0].view.SetDirection(card.direction);
                        _units[0].MoveTo(step);
                    }
                    else
                    {
                        _units[0].MoveTo(_units[0].pos);
                    }
                    _units[0].onChangeState = OnEndMove;
                    break;
                case CardType.Attack:
                    SetUIInteractable(false);
                    _units[0].view.SetDirection(card.direction);
                    _units[0].Attack(new SetDamage()
                    {
                        dist = card.value,
                        value = 1 + _boostAttack,
                        direction = card.direction,
                        attackType = card.attackType,
                    });
                    _units[0].onChangeState = OnEndAttack;
                    if (card.attackType == CardAttackType.Zone)
                    {
                        int dir = card.direction == CardDirection.Rigth ? 1 : -1;
                        for (int i = 1; i <= card.value; i++)
                        {
                            _bulets.SetBulet(i - 1, new Vector3(_units[0].pos + i * dir, 10, 0), new Vector3(_units[0].pos + i * dir, -3, 0));
                        }
                    }
                    else
                    {
                        var temp = GetUnits(_units[0]);
                        if (temp.Count > 0 && temp[0].direction <= card.value)
                        {
                            _bulets.SetBulet(0, new Vector3(_units[0].pos, -2, 0), new Vector3(temp[0].pos, -2, 0));
                        }
                        else
                        {
                            int dir = card.direction == CardDirection.Rigth ? 1 : -1;
                            _bulets.SetBulet(0, new Vector3(_units[0].pos, -2, 0), new Vector3(_units[0].pos + card.value * dir, -2, 0));
                        }
                    }
                    break;
            }
        }

        private void OnEndAttack(Unit unit, UnitState state)
        {
            if (state == UnitState.Idle)
            {
                unit.onChangeState = null;

                var temp = GetUnits(unit);

                for (int i = 0; i < temp.Count; i++)
                {
                    Unit mob = temp[i];
                    if (mob.direction <= unit.damage.dist)
                    {
                        mob.hp -= unit.damage.value;
                        if (unit.damage.attackType == CardAttackType.Point)
                            break;
                    }
                }
                _boostAttack = 0;
                _uIController.TopPanelView().SetCurentPower(_boostAttack);
                SetUIInteractable(true);
            }
        }

        private List<Unit> GetUnits(Unit unit)
        {
            List<Unit> temp = new List<Unit>();
            for (int i = 1; i < _units.Length; i++)
            {
                if (_units[i] != null)
                {
                    _units[i].direction = Mathf.Abs(_units[i].pos - _units[0].pos);
                    if (unit.damage.direction == CardDirection.Left && _units[0].pos > _units[i].pos)
                        temp.Add(_units[i]);
                    if (unit.damage.direction == CardDirection.Rigth && _units[0].pos < _units[i].pos)
                        temp.Add(_units[i]);
                }
            }
            temp = temp.OrderBy(u => u.direction).ToList();
            return temp;
        }

        private void OnEndMove(Unit unit, UnitState state)
        {
            if (state == UnitState.Idle)
            {
                unit.onChangeState = null;
                SetUIInteractable(true);
            }
        }

        private void SetUIInteractable(bool value)
        {
            _uIController.TopPanelView().SetInteractable(value);
            _uIController.NextTurnPanelView().SetInteractable(value);
        }

        private void GoMenu(int index)
        {
            _uIController.TopPanelView().Close();
            _uIController.LosePanelView().Close();
            _uIController.WinPanelView().Close();
            _uIController.PackPanelView().Close();
            _uIController.NextTurnPanelView().Close();
            if (index >= 0)
            {
                int newIndexCard = _packs.GetUpdateIndex(index);
                if (_additionalCard == null)
                    _additionalCard = new List<int>();
                _additionalCard.Add(newIndexCard);
            }
            else
                _additionalCard = null;

            _scenesManager.LoadScene(SceneId.Menu, new MenuSceneParams(SceneId.Main, nextLevel, _additionalCard));
        }

        private void Update()
        {
            if (_stopGame)
                return;

            for (int i = 0; i < _units.Length; i++)
            {
                Unit unit = _units[i];
                if (unit == null)
                    continue;
                if (i > 0)
                {
                    if (_units[0] != null)
                    {
                        CardDirection direction = unit.pos < _units[0].pos ? CardDirection.Left : CardDirection.Rigth;
                        unit.view.SetDirection(direction);
                    }
                }

                unit.Update();
                if (unit.hp <= 0)
                {
                    if (i == 0)
                    {
                        _stopGame = true;
                        EndGame();
                    }
                    GameObject.Destroy(unit.view.gameObject);
                    _units[i] = null;
                    if (TestWin())
                    {
                        _stopGame = true;
                        _uIController.TopPanelView().Close();
                        _uIController.NextTurnPanelView().Close();
                        _uIController.WinPanelView().Show();
                        _uIController.WinPanelView().Setup(_packs.GetUpdateCard());
                        _uIController.WinPanelView().onGoMenu = GoMenu;
                        return;
                    }
                }
            }

            if (_agrUnit.Count > 0 && _stateAi == 0)
            {
                _stateAi = 1;
                var playerDirection = _units[0].pos - _agrUnit[0].pos;

                if (Mathf.Abs(playerDirection) <= _agrUnit[0].damageDist)
                {
                    _agrUnit[0].Attack(new SetDamage() { dist = _agrUnit[0].damageDist, value = 1 });
                    _agrUnit[0].onChangeState = EndAttackAi;
                }
                else
                {
                    if (playerDirection < 0)
                    {
                        if (CanMove(_agrUnit[0].pos - 1))
                            _agrUnit[0].MoveTo(_agrUnit[0].pos - 1);
                        else
                            _agrUnit[0].MoveTo(_agrUnit[0].pos);
                    }
                    else
                    {
                        if (CanMove(_agrUnit[0].pos + 1))
                            _agrUnit[0].MoveTo(_agrUnit[0].pos + 1);
                        else
                            _agrUnit[0].MoveTo(_agrUnit[0].pos);
                    }

                    _agrUnit[0].onChangeState = EndMoveAi;
                }
            }

            if (_units[0] != null)
                _cameraControl.UpdatePos(_units[0].view.transform.position.x);
        }

        private bool TestWin()
        {
            for (int i = 1; i < _units.Length; i++)
            {
                Unit unit = _units[i];
                if (unit != null)
                    return false;
            }
            return true;
        }

        private bool CanMove(int pos)
        {
            for (int i = 0; i < _units.Length; i++)
            {
                Unit unit = _units[i];
                if (unit == null)
                    continue;
                if (unit.pos == pos)
                    return false;
            }
            return true;
        }

        private void EndAttackAi(Unit mob, UnitState state)
        {
            if (state == UnitState.Idle)
            {
                mob.onChangeState = null;

                _units[0].hp -= mob.damage.value + mob.powerPlus;

                _agrUnit.Remove(mob);
                if (_units[0].hp < 1)
                {
                    _agrUnit.Clear();
                }
                if (_agrUnit.Count == 0)
                    EndAiTurn();
                _stateAi = 0;
            }
        }

        private void EndMoveAi(Unit mob, UnitState state)
        {
            if (state == UnitState.Idle)
            {
                mob.onChangeState = null;
                _agrUnit.Remove(mob);
                if (_agrUnit.Count == 0)
                    EndAiTurn();
                _stateAi = 0;
            }
        }
    }
}