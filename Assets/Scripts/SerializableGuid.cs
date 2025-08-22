using System;
using UnityEngine;
using Unity.Collections;

namespace RobotBattle
{
    [Serializable]
    public class SerializableGuid
    {
        [ReadOnly]
        [SerializeField] private string _guid;

        public Guid Guid
        {
            get => string.IsNullOrEmpty(_guid) ? Guid.Empty : new Guid(_guid);
            set => _guid = value.ToString();
        }

        public SerializableGuid()
        {
            Guid = Guid.NewGuid();
        }

        public override string ToString() => Guid.ToString();
    }
}
