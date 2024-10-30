namespace Cleanup
{
public class Program {
	private const double TargetChangeTime = 1;

        private double _previousTargetSetTime;
        private bool _isTargetSet;
        private dynamic _lockedCandidateTarget;
        private dynamic _lockedTarget;
        private dynamic _target;
        private dynamic _previousTarget;
        private dynamic _activeTarget;
        private dynamic _targetInRangeContainer;

        public void CleanupTest(TexturePacker_JsonArray.Frame frame)
        {
            try
            {
                ValidateLockedTargets();
                UpdateActiveTarget(frame);

                if (IsCurrentTargetValid())
                {
                    _previousTarget = _target;
                    return;
                }

                _previousTarget = _target;

                if (TrySetTarget(_lockedTarget))
				{
					return;
				}

				if (TrySetTarget(_activeTarget))
				{
					return;
				}

				if (TrySetTarget(_targetInRangeContainer.GetTarget()))
				{
					return;
				}

				_target = null; // No valid target found
            }
            finally
            {
                UpdateTargetSelection();
            }
        }

        private void ValidateLockedTargets()
        {
            if (_lockedCandidateTarget != null && !_lockedCandidateTarget.CanBeTarget)
            {
                _lockedCandidateTarget = null;
            }

            if (_lockedTarget != null && !_lockedTarget.CanBeTarget)
            {
                _lockedTarget = null;
            }
        }

        private void UpdateActiveTarget(TexturePacker_JsonArray.Frame frame)
        {
            // Updates _activeTarget based on the frame
            TrySetActiveTargetFromQuantum(frame);
        }

        private bool IsCurrentTargetValid()
        {
            if (_target != null && _target.CanBeTarget && (Time.time - _previousTargetSetTime) < TargetChangeTime)
            {
                return true;
            }
            return false;
        }

        private bool TrySetTarget(dynamic potentialTarget)
        {
            if (potentialTarget != null && potentialTarget.CanBeTarget)
            {
                if (_target != potentialTarget)
                {
                    _previousTargetSetTime = Time.time;
                }
                _target = potentialTarget;
                return true;
            }
            return false;
        }

        private void UpdateTargetSelection()
        {
            TargetableEntity.Selected = _target;
        }
		
        private void TrySetActiveTargetFromQuantum(TexturePacker_JsonArray.Frame frame)
        {
            // Implementation details...
        }

        // MORE CLASS CODE
    }
}