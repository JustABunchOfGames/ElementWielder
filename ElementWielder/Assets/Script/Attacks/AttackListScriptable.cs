using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    [CreateAssetMenu(menuName ="Attacks/List")]
    public class AttackListScriptable : ScriptableObject
    {
        [field: SerializeField] public List<AttackData> attackData { get; private set; }
    }
}