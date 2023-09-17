namespace Upgrades
{
    public class IndexedUpgrade
    {
        public UpgradeSeries upgradeSeries { get; private set; }
        public int index { get; private set; }

        public IndexedUpgrade(UpgradeSeries upgrade)
        {
            this.upgradeSeries = upgrade;
            index = 0;
        }

        public bool isMaxed()
        {
            return index == upgradeSeries.GetMaxIndex();
        }

        public void IncrementIndex()
        {
            index++;
        }

        public Upgrade GetLatestUpgrade()
        {
            return upgradeSeries.GetUpgrade(index);
        }

        public Upgrade GetFirstUpgrade()
        {
            return upgradeSeries.GetUpgrade(0);
        }
    }
}