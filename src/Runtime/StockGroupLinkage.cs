﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSimStockFlow
{
    class StockGroupLinkage
    {
        private StockGroup m_StockGroup;
        private double m_Value;

        public StockGroupLinkage(StockGroup stockGroup, double value)
        {
            this.m_StockGroup = stockGroup;
            this.m_Value = value;
        }

        internal StockGroup StockGroup
        {
            get
            {
                return m_StockGroup;
            }
        }

        public double Value
        {
            get
            {
                return m_Value;
            }
        }
    }
}
