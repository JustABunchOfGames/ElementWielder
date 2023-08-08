using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    public class IndexedUpgrade
    {
        public UpgradeSeries upgradeSeries { get; private set; }
        private int _index;

        public IndexedUpgrade(UpgradeSeries upgrade)
        {
            this.upgradeSeries = upgrade;
        }

        public bool isMaxed()
        {
            return _index == upgradeSeries.GetMaxIndex();
        }

        public void AugmentIndex()
        {
            _index++;
        }

        public Upgrade GetUpgrade()
        {
            return upgradeSeries.GetUpgrade(_index);
        }
    }
}