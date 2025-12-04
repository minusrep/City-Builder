using System.Collections.Generic;
using System.IO;
using fastJSON;
using Runtime.Colony.Citizens.StateMachine.Temp;
using Runtime.Descriptions.Citizens;
using UnityEngine;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenStateMachineTest : MonoBehaviour
    {
        [SerializeField] private CitizenStateMachineView _view;

        private TempCitizenModel _citizenModel;
        private CitizenStateMachinePresenter _presenter;
        private CitizenStateMachineModel _model;

        [SerializeField] private float hungry;
        [SerializeField] private bool hasJob;
        
        private GUIStyle _header;
        private GUIStyle _text;
        private GUIStyle _stateNormal;
        private GUIStyle _stateCurrent;
        private CitizenStateDescriptionCollection _citizenStateCollection;

        private void Start()
        {
            _citizenModel = new TempCitizenModel()
            {
                Counters = new Dictionary<string, float>()
                {
                    {"Hungry", 10f},
                },
                Flags = new Dictionary<string, bool>()
                {
                    {"HasJob", false},
                }
            };
            
            var json = File.ReadAllText("Assets/Resources/citizenStatesDescriptions.json");
            var descriptionsDictionary = JSON.ToObject<Dictionary<string, object>>(json);

            _citizenStateCollection = new CitizenStateDescriptionCollection();
            _citizenStateCollection.Deserialize(descriptionsDictionary);

            _model = new CitizenStateMachineModel(_citizenStateCollection.StateDescriptions);
            _presenter = new CitizenStateMachinePresenter(_model, _view, _citizenModel);
            _presenter.Enable();

            // --- GUI Styles ---
            _header = new GUIStyle()
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };

            _text = new GUIStyle()
            {
                fontSize = 18,
                normal = { textColor = Color.white }
            };

            _stateNormal = new GUIStyle()
            {
                fontSize = 18,
                normal = { textColor = Color.gray }
            };

            _stateCurrent = new GUIStyle()
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.green }
            };
        }

        
        private void OnGUI()
        {
            if (_citizenModel == null || _model == null)
                return;

            int y = 10;

            GUI.Label(new Rect(10, y, 500, 30), "STATE MACHINE DEBUG", _header);
            y += 40;


            // ───── STATES BLOCK ─────
            GUI.Label(new Rect(10, y, 500, 30), "ALL STATES", _header);
            y += 35;

            foreach (var state in _citizenStateCollection.StateDescriptions)
            {
                bool isCurrent = _model.CurrentStateDescription == state.Value;

                GUI.Label(
                    new Rect(10, y, 500, 28),
                    state.Value.Name,
                    isCurrent ? _stateCurrent : _stateNormal
                );

                y += isCurrent ? 30 : 26;
            }

            y += 20;


            // ───── CURRENT STATE ─────
            GUI.Label(new Rect(10, y, 500, 30), "CURRENT STATE", _header);
            y += 35;

            GUI.Label(new Rect(10, y, 500, 30),
                _model.CurrentStateDescription.Name,
                _stateCurrent);
            y += 40;


            // ───── COUNTERS ─────
            GUI.Label(new Rect(10, y, 500, 30), "COUNTERS", _header);
            y += 35;

            foreach (var kvp in _citizenModel.Counters)
            {
                GUI.Label(new Rect(10, y, 500, 26), $"{kvp.Key}: {kvp.Value}", _text);
                y += 26;
            }

            y += 20;


            // ───── FLAGS ─────
            GUI.Label(new Rect(10, y, 500, 30), "FLAGS", _header);
            y += 35;

            foreach (var kvp in _citizenModel.Flags)
            {
                GUI.Label(new Rect(10, y, 500, 26), $"{kvp.Key}: {kvp.Value}", _text);
                y += 26;
            }
        }
        
        
        private void Update()
        {
            _citizenModel.Counters["Hungry"] = hungry;
            _citizenModel.Flags["HasJob"] = hasJob;
        }
    }
}
