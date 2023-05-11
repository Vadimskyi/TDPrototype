/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using UnityEngine;

namespace VadimskyiLab.UiExtension
{
	internal sealed class TweenPositionStrategy : MonoStrategyBase<Vector2>
	{
		private Transform _target;
		private Vector2 _defaultValue;

		public TweenPositionStrategy(
			Transform target,
			ITweenSharedState sharedSharedState,
			IValueModifier<Vector2> modHandler,
			ITweenPlayStyleStrategy style) : base(target, sharedSharedState, modHandler, style)
		{
			_target = target;
			_defaultValue = target.localPosition;
		}

		public override void OnValueUpdated(Vector2 value)
		{
			if(!IsTargetValid()) return;
			_target.localPosition = value;
		}

		public override void ResetValueToDefault()
		{
			if (!IsTargetValid()) return;
			_target.localPosition = _defaultValue;
		}
	}
}