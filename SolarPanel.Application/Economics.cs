using System;

namespace SolarPanel.Application
{
    public class Economics
    {
        public Economics(
            float installationCost,
            float panelCost,
            float panelCount,
            float totalInitialOutlay,
            DateTime? breakEvenDate,
            TimeSpan? timeToBreakEven,
            float totalProfit,
            float savedOnBills,
            float generationProfit)
        {
            InstallationCost = installationCost;
            PanelCost = panelCost;
            PanelCount = panelCount;
            TotalInitialOutlay = totalInitialOutlay;
            BreakEvenDate = breakEvenDate;
            TimeToBreakEven = timeToBreakEven;
            TotalProfit = totalProfit;
            SavedOnBills = savedOnBills;
            GenerationProfit = generationProfit;
        }

        /// <summary>
        /// The installation costs (£)
        /// </summary>
        public float InstallationCost { get; }
        /// <summary>
        /// The purchase cost of the panels (£)
        /// </summary>
        public float PanelCost { get; }
        /// <summary>
        /// The number of panels installed
        /// </summary>
        public float PanelCount { get; }
        /// <summary>
        /// The total initial outlay (£)
        /// </summary>
        public float TotalInitialOutlay { get; }
        /// <summary>
        /// The break even date
        /// </summary>
        public DateTime? BreakEvenDate { get; }
        /// <summary>
        /// The time it takes to break even
        /// </summary>
        public TimeSpan? TimeToBreakEven { get; }
        /// <summary>
        /// The total profit from installing the panels (£)
        /// </summary>
        public float TotalProfit { get; }
        /// <summary>
        /// The amount saved on bills (£)
        /// </summary>
        public float SavedOnBills { get; }
        /// <summary>
        /// The amount made from selling electricity back to the grid (£)
        /// </summary>
        public float GenerationProfit { get; }
    }
}
