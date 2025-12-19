using System.Text;
using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenDebugPresenter : IPresenter
    {
        private readonly CitizenView _view;

        private readonly CitizenModel _model;

        public CitizenDebugPresenter(CitizenView view, CitizenModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.OnGui += OnGui;
        }

        public void Disable()
        {
            _view.CitizenMovementView.OnUpdate += OnGui;
        }

        private void OnGui()
        {
            if (_model == null)
                return;

            var sb = new StringBuilder(1024);

            sb.AppendLine($"Citizen #{_model.Id}");
            sb.AppendLine($"Position: {_model.Position}");
            sb.AppendLine();

            sb.AppendLine("FLAGS:");
            if (_model.Flags != null)
                foreach (var f in _model.Flags)
                    sb.AppendLine($"  {f.Key}: {f.Value}");

            sb.AppendLine("\nSTATS:");
            if (_model.Stats != null)
                foreach (var s in _model.Stats)
                    sb.AppendLine($"  {s.Key}: {s.Value:0.00}");

            sb.AppendLine("\nTIMERS:");
            if (_model.Timers != null)
                foreach (var t in _model.Timers)
                    sb.AppendLine($"  {t.Key}: {t.Value}");

            sb.AppendLine("\nPOINTS OF INTEREST:");
            if (_model.PointsOfInterest != null)
                foreach (var p in _model.PointsOfInterest)
                    sb.AppendLine($"  {p.Key}: {p.Value}");

/* ================= FSM ================= */

            sb.AppendLine("\nSTATE MACHINE:");
            sb.AppendLine($"Current State: {_model.StateMachine.CurrentStateName}");

            var state = _model.StateMachine.CurrentState;

            if (state != null)
            {
                // ACTIONS
                sb.AppendLine("  Actions:");
                if (state.Actions != null && state.Actions.Count > 0)
                {
                    foreach (var action in state.Actions)
                        sb.AppendLine($"    - {action.Type}");
                }
                else
                {
                    sb.AppendLine("    (none)");
                }

                // TRANSITIONS
                sb.AppendLine("  Transitions:");
                if (state.Transitions != null && state.Transitions.Count > 0)
                {
                    foreach (var transition in state.Transitions)
                        sb.AppendLine($"    -> {transition.ToState}");
                }
                else
                {
                    sb.AppendLine("    (none)");
                }
            }
            else
            {
                sb.AppendLine("  State data: NULL");
            }

            GUI.Box(
                new Rect(10, 10, 460, 700),
                sb.ToString()
            );

        }
    }
}