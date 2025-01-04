using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace GM.Entities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Entity : MonoBehaviour
    {
        protected Dictionary<Type, IEntityComponent> _components;

        public NavMeshAgent NavAgent => _navAgent;
        protected NavMeshAgent _navAgent;

        public bool CanManualMove => _canManualMove;
        protected bool _canManualMove = false;

        protected virtual void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();

            _components = new Dictionary<Type, IEntityComponent>();
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInitialize();
        }

        #region Entity Component Structure

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IEntityComponent>(true)
                .ToList().ForEach(component => _components.Add(component.GetType(), component));
        }

        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInitable>()
                .ToList().ForEach(afterInitCompo => afterInitCompo.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponent component))
            {
                return (T)component;
            }

            if (isDerived == false) return default;

            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_components[findType];

            return default;
        }

        #endregion

        /// <summary>
        /// Set Movement Destination
        /// </summary>
        /// <param name="targetTrm">target Transform to move</param>
        public virtual void SetMovement(Transform targetTrm)
        {
            _navAgent.SetDestination(targetTrm.position);
        }

        /// <summary>
        /// Move Stop
        /// </summary>
        public void StopImmediately()
        {
            _navAgent.isStopped = true;
        }
    }
}
