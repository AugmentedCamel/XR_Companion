/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction
{
    /// <summary>
    /// Sets the origin of the ray used by hand ray interactors.
    /// </summary>
    public class HandPointerPose : MonoBehaviour, IActiveState
    {
        /// <summary>
        /// A hand ray interactor.
        /// </summary>
        [Tooltip("A hand ray interactor.")]
        [SerializeField, Interface(typeof(IHand))]
        private UnityEngine.Object _hand;
        public IHand Hand { get; private set; }

        /// <summary>
        /// How much the ray origin is offset relative to the hand.
        /// </summary>
        [Tooltip("How much the ray origin is offset relative to the hand.")]
        [SerializeField]
        private Vector3 _offset;

        public bool Active => Hand.IsPointerPoseValid;

        protected bool _started = false;

        protected virtual void Awake()
        {
            Hand = _hand as IHand;
        }

        protected virtual void Start()
        {
            this.BeginStart(ref _started);
            this.AssertField(Hand, nameof(Hand));
            this.EndStart(ref _started);
        }

        protected virtual void OnEnable()
        {
            if (_started)
            {
                Hand.WhenHandUpdated += HandleHandUpdated;
            }
        }

        protected virtual void OnDisable()
        {
            if (_started)
            {
                Hand.WhenHandUpdated -= HandleHandUpdated;
            }
        }

        private void HandleHandUpdated()
        {
            if (Hand.GetPointerPose(out Pose pointerPose))
            {
                pointerPose.position += pointerPose.rotation * _offset;
                transform.SetPose(pointerPose);
            }
        }

        #region Inject

        public void InjectAllHandPointerPose(IHand hand,
            Vector3 offset)
        {
            InjectHand(hand);
            InjectOffset(offset);
        }

        public void InjectHand(IHand hand)
        {
            _hand = hand as UnityEngine.Object;
            Hand = hand;
        }

        public void InjectOffset(Vector3 offset)
        {
            _offset = offset;
        }

        #endregion
    }
}
