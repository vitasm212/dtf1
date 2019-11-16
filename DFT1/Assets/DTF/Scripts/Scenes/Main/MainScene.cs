using System;
using System.Collections.Generic;
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

        private Map _map;
        private Unit[] _units;
        private List<Unit> _agrUnit = new List<Unit>();

        private int _stateAi = 0;
        private bool _stopGame = false;

        void Start()
        {
            _scenesManager = GameObject.FindObjectOfType<ScenesManager>();

            var param = ScenesManager.GetSceneParams(SceneId.Main) as MainSceneParams;

            _board = GameObject.FindObjectOfType<BoardView>();
            _cameraControl = GameObject.FindObjectOfType<CameraControl>();

            _uIController = new UIController(Canvas);
            _uIController.NextTurnPanelView().Show();
            _uIController.NextTurnPanelView().onNextTurn = OnNextTurn;

            _uIController.TopPanelView().Show();
            _uIController.TopPanelView().onGoMenu = GoMenu;
            _uIController.TopPanelView().onSelectCard = OnSelectCard;

            GenerateSetCard(6);

            SetUIInteractable(true);

            _units = new Unit[5];
            _units[0] = new Unit(UnitType.player, 4, 0, _board.transform);

            for (int i = 1; i < _units.Length; i++)
            {
                _units[i] = new Unit((UnitType)UnityEngine.Random.Range(2, 4), 10, i * 2 + 1, _board.transform);
            }

            _map = new Map(Settings.MapSize);
            for (int x = 0; x < Map.Size; x++)
            {
                _map[x] = new Cell(1, x, _board.rootRoad.transform);
            }
        }

        private void GenerateSetCard(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CardType rType = (CardType)UnityEngine.Random.Range(0, 2);
                int valueCard = UnityEngine.Random.Range(-6, 6);
                if (valueCard == 0)
                    valueCard++;
                if (rType == CardType.Attack)
                    valueCard = Mathf.Abs(valueCard);

                _uIController.TopPanelView().AddCard(new Card(rType, valueCard));
            }
        }

        private void OnNextTurn()
        {
            SetUIInteractable(false);
            _uIController.TopPanelView().ClealCard();
            GenerateSetCard(6);

            _agrUnit.Clear();
            for (int i = 1; i < _units.Length; i++)
            {
                if (_units[i] != null)
                    _agrUnit.Add(_units[i]);
            }
            _stateAi = 0;
        }

        private void EndAiTurn()
        {
            if (_units[0].hp > 0)
                SetUIInteractable(true);
            else
            {
                _stopGame = true;
                _uIController.TopPanelView().Close();
                _uIController.NextTurnPanelView().Close();
            }
        }

        private void OnSelectCard(Card card)
        {
            switch (card.type)
            {
                case CardType.Move:
                    SetUIInteractable(false);
                    _units[0].MoveTo(_units[0].pos + card.value);
                    _units[0].onChangeState = OnEndMove;
                    break;
                case CardType.Attack:
                    SetUIInteractable(false);
                    _units[0].Attack(new SetDamage() { dist = card.value, value = 1 });
                    _units[0].onChangeState = OnEndAttack;
                    break;
            }
        }

        private void OnEndAttack(Unit unit, UnitState state)
        {
            if (state == UnitState.Idle)
            {
                unit.onChangeState = null;

                for (int i = 1; i < _units.Length; i++)
                {
                    Unit mob = _units[i];
                    if (mob != null && mob.pos < unit.pos + unit.damage.dist && mob.pos > unit.pos - unit.damage.dist)
                    {
                        mob.hp -= unit.damage.value;
                    }
                }

                SetUIInteractable(true);
            }
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

        private void GoMenu()
        {
            _uIController.TopPanelView().Close();
            _uIController.NextTurnPanelView().Close();
            _scenesManager.LoadScene(SceneId.Menu, new MenuSceneParams(SceneId.Main));
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

                unit.Update();
                if (unit.hp <= 0)
                {
                    GameObject.Destroy(unit.view.gameObject);
                    _units[i] = null;
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
                        _agrUnit[0].MoveTo(_agrUnit[0].pos - 1);
                    else
                        _agrUnit[0].MoveTo(_agrUnit[0].pos + 1);

                    _agrUnit[0].onChangeState = EndMoveAi;
                }
            }

            _cameraControl.UpdatePos(_units[0].view.transform.position.x);
        }

        private void EndAttackAi(Unit mob, UnitState state)
        {
            if (state == UnitState.Idle)
            {
                mob.onChangeState = null;

                _units[0].hp -= mob.damage.value;

                _agrUnit.Remove(mob);
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