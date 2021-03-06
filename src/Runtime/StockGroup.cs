﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSimStockFlow
{
    class StockGroup : StockFlowType
    {
        private StockTypeLinkageCollection m_StockTypeLinkages = new StockTypeLinkageCollection();

        public StockGroup(int id, string name) : base(id, name)
        {
        }

        internal StockTypeLinkageCollection StockTypeLinkages
        {
            get
            {
                return this.m_StockTypeLinkages;
            }
        }
    }
}
